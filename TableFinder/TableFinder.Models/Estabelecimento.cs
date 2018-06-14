using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableFinder.Models
{
    public class Estabelecimento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public string CNPJ { get; set; }
        public string Localizacao { get; set; }
        public int Aprovado { get; set; }
        public Cadastro Usuario { get; set; }
        public List<Feedback> Opinioes { get; set; }
        public List<Cardapio> Cardapio { get; set; }

        public Estabelecimento()
        {
            this.Opinioes = new List<Feedback>();
            this.Cardapio = new List<Cardapio>();
        }
    }
}
