﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TableFinder.Models;

namespace TableFinder.DataAccess
{
    public class EstabelecimentoDAO
    {
        public void Inserir(Estabelecimento obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"INSERT INTO estabelecimento (nome, descricao, imagem, cnpj, localizacao, aprovado, id_usuario) 
                                  VALUES (@nome, @descricao, @imagem, @cnpj, @localizacao, @aprovado, @id_usuario);";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;
                    cmd.Parameters.Add("@imagem", SqlDbType.VarChar).Value = obj.Imagem;
                    cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = obj.CNPJ;
                    cmd.Parameters.Add("@localizacao", SqlDbType.VarChar).Value = obj.Localizacao;
                    cmd.Parameters.Add("@aprovado", SqlDbType.Bit).Value = obj.Aprovado;
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = obj.Usuario.Id;

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

        public void Atualizar(Estabelecimento obj)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"UPDATE estabelecimento SET 
                                    nome = @nome,
                                    descricao = @descricao,
                                    imagem = @imagem,
                                    cnpj = @cnpj,
                                    localizacao = @localizacao
                                  WHERE id_estabelecimento = @id_estabelecimento;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = obj.Nome;
                    cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = obj.Descricao;
                    cmd.Parameters.Add("@imagem", SqlDbType.VarChar).Value = obj.Imagem;
                    cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = obj.CNPJ;
                    cmd.Parameters.Add("@localizacao", SqlDbType.VarChar).Value = obj.Localizacao;
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = obj.Id;

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

        public void Excluir(int idEstabelecimento)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"DELETE from cardapio WHERE id_estabelecimento = @id_estabelecimento; 
                                  DELETE from estabelecimento WHERE id_estabelecimento = @id_estabelecimento";
                

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = idEstabelecimento;

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public void Aprovar(int idEstabelecimento)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"UPDATE estabelecimento SET aprovado = 1 WHERE id_estabelecimento = @id_estabelecimento;";

                //Criando um comando sql que será executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    cmd.Connection = conn;
                    //Preenchendo os parâmetros da instrução sql
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = idEstabelecimento;


                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

        public Estabelecimento BuscarPorId(int id)
        {
            var lst = new List<Estabelecimento>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM estabelecimento where id_estabelecimento = @id_estabelecimento;";

                //Criando um comando SQL para ser executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Parameters.Add("@id_estabelecimento", SqlDbType.Int).Value = id;
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
                    //Percorrendo todos os registros encontrados na base de dados e adicionando em uma lista
                    var estabelecimento = new Estabelecimento()
                    {
                        Id = Convert.ToInt32(row["id_estabelecimento"]),
                        Nome = row["nome"].ToString(),
                        Descricao = row["descricao"].ToString(),
                        Imagem = row["imagem"].ToString(),
                        CNPJ = row["cnpj"].ToString(),
                        Localizacao = row["localizacao"].ToString(),
                        Aprovado = Convert.ToInt32(row["aprovado"]),
                        Usuario = new Cadastro() { Id = Convert.ToInt32(row["id_usuario"]) }
                    };

                    return estabelecimento;
                }
            }
        }

        public List<Estabelecimento> BuscarTodos()
        {
            var lst = new List<Estabelecimento>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM estabelecimento;";

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
                        var estabelecimento = new Estabelecimento()
                        {
                            Id = Convert.ToInt32(row["id_estabelecimento"]),
                            Nome = row["nome"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Imagem = row["imagem"].ToString(),
                            CNPJ = row["cnpj"].ToString(),
                            Localizacao = row["localizacao"].ToString(),
                            Aprovado = Convert.ToInt32(row["aprovado"]),
                            Usuario = new Cadastro() { Id = Convert.ToInt32(row["id_usuario"]) }
                        };

                        lst.Add(estabelecimento);
                    }
                }
            }

            return lst;
        }

        public List<Estabelecimento> BuscarNaoAprovados()
        {
            var lst = new List<Estabelecimento>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM estabelecimento where aprovado = 0;";

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
                        var estabelecimento = new Estabelecimento()
                        {
                            Id = Convert.ToInt32(row["id_estabelecimento"]),
                            Nome = row["nome"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Imagem = row["imagem"].ToString(),
                            CNPJ = row["cnpj"].ToString(),
                            Localizacao = row["localizacao"].ToString(),
                            Aprovado = Convert.ToInt32(row["aprovado"]),
                            Usuario = new Cadastro() { Id = Convert.ToInt32(row["id_usuario"]) }
                        };

                        lst.Add(estabelecimento);
                    }
                }
            }

            return lst;
        }

        public List<Estabelecimento> BuscarAprovados()
        {
            var lst = new List<Estabelecimento>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM estabelecimento where aprovado = 1;";

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
                        var estabelecimento = new Estabelecimento()
                        {
                            Id = Convert.ToInt32(row["id_estabelecimento"]),
                            Nome = row["nome"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Imagem = row["imagem"].ToString(),
                            CNPJ = row["cnpj"].ToString(),
                            Localizacao = row["localizacao"].ToString(),
                            Aprovado = Convert.ToInt32(row["aprovado"]),
                            Usuario = new Cadastro() { Id = Convert.ToInt32(row["id_usuario"]) }
                        };

                        lst.Add(estabelecimento);
                    }
                }
            }

            return lst;
        }

        public List<Estabelecimento> BuscarPorDono(int Usuario)
        {
            var lst = new List<Estabelecimento>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM estabelecimento where id_usuario = @id_usuario;";

                //Criando um comando SQL para ser executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = Usuario;
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
                        var estabelecimento = new Estabelecimento()
                        {
                            Id = Convert.ToInt32(row["id_estabelecimento"]),
                            Nome = row["nome"].ToString(),
                            Descricao = row["descricao"].ToString(),
                            Imagem = row["imagem"].ToString(),
                            CNPJ = row["cnpj"].ToString(),
                            Localizacao = row["localizacao"].ToString(),
                            Aprovado = Convert.ToInt32(row["aprovado"]),
                            Usuario = new Cadastro() { Id = Convert.ToInt32(row["id_usuario"]) }
                        };

                        lst.Add(estabelecimento);
                    }
                }
            }

            return lst;
        }
    }
}
