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
    public class TComidaDAO
    {
        public List<TComida> BuscarPorTipoComida(int TComida)
        {
            var lst = new List<TComida>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=TableFinder; Data Source=localhost; Integrated Security = SSPI;"))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM tipo_comida where tipoId = @tipoId;";

                //Criando um comando SQL para ser executado na base de dados
                using (SqlCommand cmd = new SqlCommand(strSQL))
                {
                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    cmd.Parameters.Add("@tipoId", SqlDbType.Int).Value = TComida;
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
                        var obj = new TComida()
                        {
                            TipoId = Convert.ToInt32(row["tipoId"]),
                            TipoNome = row["tipoNome"].ToString()
                        };

                        lst.Add(obj);
                    }
                }
            }
            return lst;
        }

        public List<TComida> BuscarTodos()
        {
            var lst = new List<TComida>();
            //Criando uma conexão com o banco de dados
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=TableFinder; Data Source=localhost; Integrated Security = SSPI;"))
            {
                //Criando instrução na tabela SQL para selecionar todos os registros na tabela de estados
                string strSQL = @"SELECT * FROM tipo_comida;";

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
                        var obj = new TComida()
                        {
                            TipoId = Convert.ToInt32(row["tipoId"]),
                            TipoNome = row["tipoNome"].ToString()

                        };

                        lst.Add(obj);
                    }
                }
            }
            return lst;
        }
    }
}