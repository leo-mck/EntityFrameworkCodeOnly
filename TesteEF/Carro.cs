using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TesteEF
{
    [Table("Carro")]
    public class Carro
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public int QuantidadePortas { get; set; }
        public bool TemArCondicionado { get; set; }
        public int Ano { get; set; }
        public virtual Marca Marca { get; set; }
    }
}
