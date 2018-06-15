﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TableFinder.Models;

namespace TableFinder.DataAccess
{
    public class CardapioDAO
    {
        public void Inserir(Cardapio obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=TableFinder; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"INSERT INTO estabelecimento (id_tipo, produto, descricao, preco, imagem) VALUES (@id_tipo, @produto, @descricao, @preco, @imagem);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_tipo", SqlDbType.Int).Value = obj.Tipo.TipoId;
                    cmd.Parameters.Add("@produto", SqlDbType.VarChar).Value = obj.Produto;
                    cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;
                    cmd.Parameters.Add("@preco", SqlDbType.VarChar).Value = obj.Preco;
                    cmd.Parameters.Add("@imagem", SqlDbType.VarChar).Value = obj.Imagem;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public List<Cardapio> BuscarTodos()
        {
            var lst = new List<Cardapio>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=TableFinder; Data Source=localhost; Integrated Security = SSPI;"))
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
                            Tipo = new TComida()
                            {
                                TipoId = Convert.ToInt32(row["id_tipo"]),
                                TipoNome = row["tipoNome"].ToString()
                            },
                            Produto = row["produto"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Preco = row["preco"].ToString(),
                            Imagem = row["imagem"].ToString()
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
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=TableFinder; Data Source=localhost; Integrated Security = SSPI;"))
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
                            Tipo = new TComida()
                            {
                                TipoId = Convert.ToInt32(row["id_tipo"]),
                                TipoNome = row["tipoNome"].ToString()
                            },
                            Produto = row["produto"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Preco = row["preco"].ToString(),
                            Imagem = row["imagem"].ToString()
                        };

                        lst.Add(cardapio);
                    }
                }
            }
            return lst;
        }
    }
}
