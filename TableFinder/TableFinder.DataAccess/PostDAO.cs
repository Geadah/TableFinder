using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableFinder.Models;

namespace TableFinder.DataAccess
{
    public class PostDAO
    {
        public void Inserir(Opiniao obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"INSERT INTO post (id_usuario, data_hora, mensagem) VALUES (@id_usuario, @id_voto, @data_hora, @opiniao,@nota);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Id_Usuario;
                    cmd.Parameters.Add("@id_voto", SqlDbType.DateTime).Value = obj.Id_post;
                    cmd.Parameters.Add("@data_hora", SqlDbType.VarChar).Value = obj.Data_Hora;
                    cmd.Parameters.Add("@opiniao", SqlDbType.VarChar).Value = obj.Post;
                    cmd.Parameters.Add("@nota", SqlDbType.VarChar).Value = obj.Nota;


                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Atualizar(Opiniao obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"UPDATE post set 
                                    id_usuario = @id_usuario,
                                    id_voto = @id_voto,
                                    data_hora = @data_hora,
                                    opiniao = @opiniao,
                                    nota = @nota
                                WHERE id = @id;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Id_Usuario;
                    cmd.Parameters.Add("@id_voto", SqlDbType.DateTime).Value = obj.Id_post;
                    cmd.Parameters.Add("@data_hora", SqlDbType.VarChar).Value = obj.Data_Hora;
                    cmd.Parameters.Add("@opiniao", SqlDbType.VarChar).Value = obj.Post;
                    cmd.Parameters.Add("@nota", SqlDbType.VarChar).Value = obj.Nota;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Deletar(Opiniao obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"DELETE FROM post where id = @id;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_voto", SqlDbType.VarChar).Value = obj.Id_post;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Opiniao BuscarPorId(int id)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT * FROM post where id = @id;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var post = new Opiniao()
                    {
                        Id_Usuario = Convert.ToInt32(row["id_usuario"]),
                        Id_post = Convert.ToInt32(row["id_voto"]),
                        Usuario = new Cadastro() { Id = Convert.ToInt32(row["login"]) },
                        Data_Hora = Convert.ToDateTime(row["data_hora"]),
                        Post = row["opiniao"].ToString(),
                        Nota = Convert.ToInt32(row["nota"])
                    };

                    return post;
                }
            }
        }

        public List<Opiniao> BuscarTodos()
        {
            var lst = new List<Opiniao>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=Blog; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT 
                                    p.*, 
                                    u.nome_completo as usuario 
                                FROM feedback p 
                                INNER JOIN cadastro u ON (u.id_usuario = p.id_usuario);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    //Executando instrução sql
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    foreach (DataRow row in dt.Rows)
                    {
                        var Opiniao = new Opiniao()
                        {
                            Id_post = Convert.ToInt32(row["id"]),
                            Usuario = new Cadastro()
                            {
                                Id = Convert.ToInt32(row["id_usuario"]),
                                NomeCompleto= row["usuario"].ToString()
                            },
                            Data_Hora = Convert.ToDateTime(row["data_hora"]),
                            Post = row["opiniao"].ToString(),
                            Nota = Convert.ToInt32(row["nota"])
                        };

                        lst.Add(Opiniao);
                    }
                }
            }

            return lst;
        }
    }
}
