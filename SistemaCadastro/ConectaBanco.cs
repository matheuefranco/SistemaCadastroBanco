using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SistemaCadastro
{
    class ConectaBanco
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=compServer;database=banco_siscadastro");
        public string mensagem;
        
        public bool insereBanda(Banda novaBanda)
        
        {
            try { 
             conexao.Open();
                MySqlCommand cmd = 
                    new MySqlCommand("sp_insereBanda", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", novaBanda.Nome);
                cmd.Parameters.AddWithValue("integrantes", novaBanda.Integrantes);
                cmd.Parameters.AddWithValue("ranking", novaBanda.Ranking);
                cmd.Parameters.AddWithValue("genero", novaBanda.Genero);
                cmd.ExecuteNonQuery();//executar no banco
             return true;
            }catch(MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }

        }// fim do insereBanda 

        public DataTable listaGeneros()
        {
            // comentario
            MySqlCommand cmd = new MySqlCommand("sp_listaGeneros", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }// fim try
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }

        }// fim lista_generos


    }// fim classe
}// fim namespace
