using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableFinder.Models
{
    public class Cardapio
    {
        public int Id { get; set; }
        public Estabelecimento Estabelecimento { get; set; }
        public TipoComida Tipo { get; set; }
        public string Produto { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
