using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TableFinder.Models;

namespace TableFinder.DataAccess
{
    public class CardapioDAO
    {
        public void Inserir(Cardapio obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"INSERT INTO cardapio (id_estabelecimento, id_tipo, produto, descricao, preco) VALUES (@id_estabelecimento, @id_tipo, @produto, @descricao, @preco);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = obj.Estabelecimento.Id;
                    cmd.Parameters.Add("@id_tipo", SqlDbType.Int).Value = obj.Tipo.TipoId;
                    cmd.Parameters.Add("@produto", SqlDbType.VarChar).Value = obj.Produto;
                    cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;
                    cmd.Parameters.Add("@preco", SqlDbType.Decimal).Value = obj.Preco;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Atualizar(Cardapio obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"UPDATE cardapio SET id_estabelecimento = @id_estabelecimento, id_tipo = @id_tipo, produto = @produto, descricao = @descricao, preco = @preco WHERE id = @id;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = obj.Estabelecimento.Id;
                    cmd.Parameters.Add("@id_tipo", SqlDbType.Int).Value = obj.Tipo.TipoId;
                    cmd.Parameters.Add("@produto", SqlDbType.VarChar).Value = obj.Produto;
                    cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;
                    cmd.Parameters.Add("@preco", SqlDbType.Decimal).Value = obj.Preco;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = obj.Id;

                    foreach (SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Excluir(Cardapio obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"DELETE FROM cardapio WHERE id_cardapio = @id_cardapio;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_cardapio", SqlDbType.Int).Value = obj.Id;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Cardapio BuscarPorId(int id)
        {
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT 
                                      c.*,
                                      tc.tipoNome
                                  FROM cardapio c
                                  INNER JOIN tipo_comida tc on (tc.tipoId = c.id_tipo)
                                  WHERE c.id_cardapio = @id_cardapio;";

                //Criando um comando SQL para ser executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Parameters.Add("@id_cardapio", SqlDbType.Int).Value = id;
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    //Executando instrução SQL
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    if (!(dt != null && dt.Rows.Count > 0))
                        return null;

                    var row = dt.Rows[0];
                    var cardapio = new Cardapio()
                    {
                        Id = Convert.ToInt32(row["id_cardapio"]),
                        Estabelecimento = new Estabelecimento()
                        {
                            Id = Convert.ToInt32(row["id_estabelecimento"])
                        },
                        Tipo = new TipoComida()
                        {
                            TipoId = Convert.ToInt32(row["id_tipo"]),
                            TipoNome = row["tipoNome"].ToString()
                        },
                        Produto = row["produto"].ToString(),
                        Descricao = row["descricao"].ToString(),
                        Preco = Convert.ToDecimal(row["preco"])
                    };

                    return cardapio;
                }
            }
        }

        public List<Cardapio> BuscarTodos()
        {
            var lst = new List<Cardapio>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM cardapio;";

                //Criando um comando SQL para ser executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    //Executando instrução SQL
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    foreach (DataRow row in dt.Rows)
                    {
                        var cardapio = new Cardapio()
                        {
                            Id = Convert.ToInt32(row["id_cardapio"]),
                            Estabelecimento = new Estabelecimento()
                            {
                                Id = Convert.ToInt32(row["id_estabelecimento"])
                            },
                            Tipo = new TipoComida()
                            {
                                TipoId = Convert.ToInt32(row["id_tipo"]),
                                TipoNome = row["tipoNome"].ToString()
                            },
                            Produto = row["produto"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Preco = Convert.ToDecimal(row["preco"])
                        };

                        lst.Add(cardapio);
                    }
                }
            }
            return lst;
        }

        public List<Cardapio> BuscarPorEstab(int estabelecimento)
        {
            var lst = new List<Cardapio>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT 
                                      c.*,
                                      tc.tipoNome
                                  FROM cardapio c
                                  INNER JOIN tipo_comida tc on (tc.tipoId = c.id_tipo)
                                  WHERE c.id_estabelecimento = @id_estabelecimento;";

                //Criando um comando SQL para ser executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = estabelecimento;
                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;

                    //Executando instrução SQL
                    var dataReader = cmd.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(dataReader);
                    //Fechando conexão com o banco de dados
                    conn.Close();

                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    foreach (DataRow row in dt.Rows)
                    {
                        var cardapio = new Cardapio()
                        {
                            Id = Convert.ToInt32(row["id_cardapio"]),
                            Estabelecimento = new Estabelecimento()
                            {
                                Id = Convert.ToInt32(row["id_estabelecimento"])
                            },
                            Tipo = new TipoComida()
                            {
                                TipoId = Convert.ToInt32(row["id_tipo"]),
                                TipoNome = row["tipoNome"].ToString()
                            },
                            Produto = row["produto"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Preco = Convert.ToDecimal(row["preco"])
                        };

                        lst.Add(cardapio);
                    }
                }
            }

            return lst;
        }
    }
}
