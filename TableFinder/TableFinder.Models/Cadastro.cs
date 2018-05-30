using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Web.Script.Serialization;

namespace TableFinder.Models
{
    public class Cadastro : ICustomPrincipal
    {
        public int Id { get; set; }

        public string NomeCompleto { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public bool Administrador { get; set; }

        [ScriptIgnore]
        [IgnoreDataMember]
        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.Email, "Cadastro");
            }
            set { }
        }

        public bool IsInRole(string role)
        {
            return (role == "Admin");
        }

        public Cadastro()
        {

        }

        public Cadastro(string myEmail)
        {
            Identity = new GenericIdentity(myEmail, "Cadastro");
        }
    }
}
