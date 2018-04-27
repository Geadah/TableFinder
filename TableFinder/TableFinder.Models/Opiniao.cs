using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFinder.Models
{
    public class Opiniao
    {
        public int Id_Usuario { get; set; }
        public int Id_post { get; set; }
        public Cadastro Usuario { get; set; }
        public DateTime Data_Hora { get; set; }
        public string Post { get; set; }
        public int Nota { get; set; }
        
    }
}
