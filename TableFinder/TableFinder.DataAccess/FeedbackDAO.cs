using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TableFinder.Models;

namespace TableFinder.DataAccess
{
    public class FeedbackDAO
    {
        public void Inserir(Feedback obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"INSERT INTO feedback (id_usuario, data_hora, opiniao, id_estabelecimento, nota) VALUES (@id_usuario, @data_hora, @opiniao , @id_estabelecimento, @nota);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Usuario.Id;
                    cmd.Parameters.Add("@data_hora", SqlDbType.DateTime).Value = obj.Data_Hora;
                    cmd.Parameters.Add("@opiniao", SqlDbType.VarChar).Value = obj.Opiniao;
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = obj.Estabelecimento.Id;
                    cmd.Parameters.Add("@nota", SqlDbType.Int).Value = obj.Nota;


                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Atualizar(Feedback obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"UPDATE feedback set 
                                    id_usuario = @id_usuario,
                                   data_hora = @data_hora,
                                    opiniao = @opiniao,
                                    id_estabelecimento = @id_estabelecimento,
                                    nota = @nota
                                WHERE id_feedback = @id_feedback;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Usuario.Id;
                    cmd.Parameters.Add("@data_hora", SqlDbType.VarChar).Value = obj.Data_Hora;
                    cmd.Parameters.Add("@opiniao", SqlDbType.VarChar).Value = obj.Opiniao;
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = obj.Estabelecimento.Id;
                    cmd.Parameters.Add("@nota", SqlDbType.Int).Value = obj.Nota;
                    cmd.Parameters.Add("@id_feedback", SqlDbType.Int).Value = obj.IdFeedback;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Deletar(Feedback obj)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de posts
                string strSQL = @"DELETE FROM feedback where id_feedback = @id_feedback;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_feedback", SqlDbType.Int).Value = obj.IdFeedback;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Feedback BuscarPorId(int id)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT * FROM feedback where id_feedback = @id_feedback;";

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
                    var feedback = new Feedback()
                    {
                        IdFeedback = Convert.ToInt32(row["id_feedback"]),
                        Usuario = new Cadastro() { Id = Convert.ToInt32(row["id_usuario"]) },
                        Estabelecimento = new Estabelecimento() { Id = Convert.ToInt32(row["id_estabelecimento"]) },
                        Data_Hora = Convert.ToDateTime(row["data_hora"]),
                        Opiniao = row["opiniao"].ToString(),
                        Nota = Convert.ToInt32(row["nota"])
                    };

                    return feedback;
                }
            }
        }

        public List<Feedback> BuscarTodos()
        {
            var lst = new List<Feedback>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT 
                                    f.*, 
                                    c.nome_completo as usuario 
                                FROM feedback f 
                                INNER JOIN cadastro c ON (c.id_usuario = f.id_usuario);";

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
                        var feedback = new Feedback()
                        {
                            IdFeedback = Convert.ToInt32(row["id_feedback"]),
                            Usuario = new Cadastro()
                            {
                                Id = Convert.ToInt32(row["id_usuario"]),
                                NomeCompleto = row["usuario"].ToString()
                            },
                            Estabelecimento = new Estabelecimento() { Id = Convert.ToInt32(row["id_estabelecimento"]) },
                            Data_Hora = Convert.ToDateTime(row["data_hora"]),
                            Opiniao = row["opiniao"].ToString(),
                            Nota = Convert.ToInt32(row["nota"])
                        };

                        lst.Add(feedback);
                    }
                }
            }

            return lst;
        }

        public List<Feedback> BuscarPorEstabelecimento(int estabelecimento)
        {
            var lst = new List<Feedback>();

            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para selecionar todos os registros na tabela de posts
                string strSQL = @"SELECT 
                                    f.*, 
                                    c.nome_completo as usuario 
                                FROM feedback f 
                                INNER JOIN cadastro c ON (c.id_usuario = f.id_usuario)
                                WHERE f.id_estabelecimento = @id_estabelecimento;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = estabelecimento;
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
                        var feedback = new Feedback()
                        {
                            IdFeedback = Convert.ToInt32(row["id_feedback"]),
                            Usuario = new Cadastro()
                            {
                                Id = Convert.ToInt32(row["id_usuario"]),
                                NomeCompleto = row["usuario"].ToString()
                            },
                            Estabelecimento = new Estabelecimento() { Id = Convert.ToInt32(row["id_estabelecimento"]) },
                            Data_Hora = Convert.ToDateTime(row["data_hora"]),
                            Opiniao = row["opiniao"].ToString(),
                            Nota = Convert.ToInt32(row["nota"])
                        };

                        lst.Add(feedback);
                    }
                }
            }

            return lst;
        }
    }
}
