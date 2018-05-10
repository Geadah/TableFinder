using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFinder.Models
{
    public class Feedback
    {
       
        public int IdFeedback { get; set; }
        public Estabelecimento Estabelecimento { get; set; }
        public Cadastro Usuario { get; set; }
        public DateTime Data_Hora { get; set; }
        public string Opiniao { get; set; }
        public int Nota { get; set; }
        
    }
}
