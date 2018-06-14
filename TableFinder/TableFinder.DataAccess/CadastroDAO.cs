using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TableFinder.Models;

namespace TableFinder.DataAccess
{
    public class CadastroDAO
    {
        public void Inserir(Cadastro obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"INSERT INTO cadastro (nome_completo, cpf, email, login, senha) VALUES (@nome_completo, @cpf, @email, @login, @senha);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@nome_completo", SqlDbType.VarChar).Value = obj.NomeCompleto;
                    cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = obj.CPF;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.Email;
                    cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = obj.Login;
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Cadastro Logar(Cadastro obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                string strSQL = @"SELECT TOP 1 * FROM cadastro where login = @login and senha = @senha;";

                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = obj.Login;
                    cmd.Parameters.Add("@senha", SqlDbType.VarChar).Value = obj.Senha;
                    cmd.CommandText = strSQL;

                    var DataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(DataReader);
                    conn.Close();

                    //se não encontrar ngm, retorna null
                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    //se encontrar alguem com este login e senha, retorna o cadastro completo do usuário
                    var row = dt.Rows[0];
                    var usuario = new Cadastro()
                    {
                        Id = Convert.ToInt32(row["id_usuario"]),
                        NomeCompleto = row["nome_completo"].ToString(),
                        Email = row["email"].ToString(),
                        CPF = row["cpf"].ToString(),
                        Login = row["login"].ToString(),
                        Senha = row["senha"].ToString(),
                        Administrador = Convert.ToBoolean(row["administrador"])
                    };

                    return usuario;
                }
            }
        }
    }
}
