using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.Models;

namespace TableFinder.WebUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logar(Cadastro obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=TableFinder; Data Source=localhost; Integrated Security=SSPI;"))
            {
                string strSQL = @"SELECT top 1 * FROM cadastro where email = '" + obj.Email + "' and senha = '" + obj.Senha + "';";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    var DataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(DataReader);
                    conn.Close();

                    //se nao encontrar ngm, redireciona para o login novamente
                    if (!(dt != null && dt.Rows.Count > 0))
                        return RedirectToAction("Index", "Login");

                    //se encontrar alguem com este login e senha, redireciona para a pagina de navegacao
                    var row = dt.Rows[0];
                    var usuario = new Cadastro()
                    {
                        Nome = row["nome_usuario"].ToString(),
                        Senha = row["senha"].ToString(),
                        NomeCompleto = row["nome_completo"].ToString(),
                        Email = row["email"].ToString(),
                        CPF = row["cpf"].ToString()
                    };
                }
            }

            return RedirectToAction("Index", "Navegacao");
        }
    }
}