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
    class EstabelecimentoDAO
    {
        public void Inserir(Estabelecimento obj)
        {
            using (SqlConnection conn = new SqlConnection(@"Initial Catalog=TableFinder; Data Source=localhost; Integrated Security=SSPI;"))
            {
                //Criando instrução sql para inserir na tabela de cidades
                string strSQL = @"INSERT INTO cadastro (nome, descricao, imagem, cnpj, localizacao) VALUES (@nome, @descricao, @imagem, @cnpj, @localizacao);";

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

                    //Abrindo conexão com o banco de dados
                    conn.Open();
                    //Executando instrução sql
                    cmd.ExecuteNonQuery();
                    //Fechando conexão com o banco de dados
                    conn.Close();
                }
            }
        }

    }
}
