using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableFinder.Models;

namespace TableFinders.Controllers
{
    public class CadastroController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Salvar(Cadastro obj)
        {
            using (SqlConnection conn =
                new SqlConnection(@"Initial Catalog=TableFinder;
                        Data Source=localhost;
                        Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"INSERT INTO cadastro (nome_usuario, senha, nome_completo, cpf, email) VALUES (@nome_usuario, @senha, @nome_completo, @cpf, @email)";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@nome_usuario", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.Parameters.Add("@nome_completo", SqlDbType.VarChar).Value = obj.NomeCompleto;
                    cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = obj.CPF;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}