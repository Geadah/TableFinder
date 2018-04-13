using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFinder.Models
{
    public class Cardapio
    {
        public int Id { get; set; }
        public int Idc { get; set; }
        public string Produto { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set;}
        public string Preco { get; set; }
        public string Imagem { get; set; }
    }
}
