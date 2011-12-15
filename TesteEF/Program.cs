using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace TesteEF
{
    class Program
    {
        static void Main(string[] args)
        {

            Database.SetInitializer<CarroDados>(new DropCreateDatabaseIfModelChanges<CarroDados>());

            //cria uma instância do DbContext, que usamos para interagir com o banco de dados
            //o "using" garante que o objeto vai ser liberado após o uso
            using (CarroDados carroDados = new CarroDados())
            {

                var opcao = Menu();

                while (opcao != "5")
                {
                    switch (opcao)
                    {
                        case "1":
                            InsereCarro(carroDados);
                            break;

                        case "2":
                            ListaCarros(carroDados);
                            break;

                        case "3":
                            AtualizaCarro(carroDados);
                            break;

                        case "4":
                            ExcluiCarro(carroDados);
                            break;

                        default:
                            Console.WriteLine("Comando inválido :õ(");
                            break;
                    }

                    Console.WriteLine("Pressione qualquer tecla para voltar");
                    Console.ReadKey();

                    opcao = Menu();
                }
            }
        }

        private static void AtualizaCarro(CarroDados carroDados)
        {
            Console.Write("Informe o código do carro: ");
            var codigo = Int32.Parse(Console.ReadLine());

            //carrega o item
            var carro = carroDados.Carros.Find(codigo);

            //testa se a query acima retornou algum resultado
            if (carro != null)
            {
                Console.Write("Modelo [{0}]: ", carro.Modelo);
                var novoModelo = Console.ReadLine();
                //se digitou alguma coisa, atualiza o valor
                carro.Modelo = (String.IsNullOrWhiteSpace(novoModelo) ? carro.Modelo : novoModelo);

                //salva alterações
                carroDados.SaveChanges();
            }
            else
            {
                Console.WriteLine("Carro \"{0}\" não encontrado", codigo);
            }
        }

        private static void ExcluiCarro(CarroDados carroDados)
        {
            Console.Write("Informe o código do carro: ");
            var codigo = Int32.Parse(Console.ReadLine());

            //digite o código do item que deseja atualizar:
            var carro = carroDados.Carros.Find(codigo);

            //testa se a query acima retornou algum resultado
            if (carro != null)
            {
                Console.Write("Deseja excluir o carro \"{0}\"?: ", carro.Modelo);
                var podeExcluir = new[] { "s", "sim", "1", "true", "t", "yes", "yep", "y", "pode cre" }.Contains(Console.ReadLine().ToLower());

                if (podeExcluir)
                {
                    carroDados.Carros.Remove(carro);
                }

                //salva alterações
                carroDados.SaveChanges();
            }
            else
            {
                Console.WriteLine("Carro \"{0}\" não encontrado", codigo);
            }
        }

        private static void ListaCarros(CarroDados carroDados)
        {
            //lista todos carros do banco de dados
            foreach (var carro in carroDados.Carros)
            {
                Console.WriteLine("{0} - {1} - {2}", carro.Id, carro.Modelo, carro.Marca.Nome);
            }
        }

        private static void InsereCarro(CarroDados carroDados)
        {

            Carro carro = new Carro();

            Console.Write("Modelo: ");
            carro.Modelo = Console.ReadLine();

            Console.Write("Ano: ");
            carro.Ano = Int32.Parse(Console.ReadLine());

            Console.Write("Portas: ");
            carro.QuantidadePortas = Int32.Parse(Console.ReadLine());

            Console.Write("Tem Ar-Condicionado: ");
            carro.TemArCondicionado = new[] { "s", "sim", "1", "true", "t", "yes", "yep", "y" }.Contains(Console.ReadLine().ToLower());

            Console.Write("Marca: ");
            var marca = Console.ReadLine();

            //verifica se a marca ja existe
            var marcaDb = carroDados.Marcas.FirstOrDefault(m => m.Nome.ToLower() == marca.ToLower());

            //se a marca existe, associa ela ao novo carro
            if (marcaDb != null)
                carro.Marca = marcaDb;
            else
                //senao cria uma nova marca com o nome informado
                carro.Marca = new Marca() { Nome = marca };

            //adiciona apenas o carro, caso a marca ainda nao exista
            //ela vai ser salva tambem automaticamente (yay!)
            carroDados.Carros.Add(carro);

            //salva alterações no banco de dados
            carroDados.SaveChanges();

            Console.WriteLine("Carro ID \"{0}\" gravado.", carro.Id);
        }

        static string Menu()
        {
            Console.Clear();
            Console.WriteLine("Digite uma opção:");
            Console.WriteLine("1. Inserir Carro");
            Console.WriteLine("2. Listar Carros");
            Console.WriteLine("3. Atualizar Carro");
            Console.WriteLine("4. Excluir Carro");
            Console.WriteLine("5. Sair");
            var opcao = Console.ReadLine();
            Console.Clear();
            return opcao;
        }



















    }
}


