using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Atividade1.Model;
using System.Windows;
using Npgsql;
using MySql.Data.MySqlClient;

namespace Atividade1
{
    public class Conexao
    {
        private static string host = "localhost";
        private static string port = "5432";
        private static string username = "postgres";
        private static string password = "1234";
        public static string dbname = "masseffect";

        private static NpgsqlConnection conexaoPostgres;
        //private static MySqlConnection conexaoSql;

        private NpgsqlCommand comandoPostgres;
        //private MySqlCommand comandoSql;

        private NpgsqlDataReader lerDadosPostgres;

        private List<Ser> listaSeres;
        private Ser ser { get; set; }

        public Conexao()
        {
            try
            {
                conexaoPostgres = new NpgsqlConnection($"Host={host};Database={dbname};Username={username};Password={password}");
                comandoPostgres = new NpgsqlCommand();
                //conexaoSql = new MySqlConnection($"Server={host};Database={dbname};Port={port};Username={username};Password={password}");
                //comandoSql = new MySqlConnection();
                comandoPostgres.Connection = conexaoPostgres;
                //comandoSql.Connection = conexaoSql;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public void Open()
        {
            try
            {
                conexaoPostgres.Open();
                //conexaoSql.Open();
            }
            catch
            {
                MessageBox.Show("Conexão com o banco de dados mal sucedida.");
                throw;
            }
        }

        public void Close()
        {
            //conexaoPostgres.Dispose();
            conexaoPostgres.Close();
            //conexaoSql.Close();
        }

        public List<Ser> Select()
        {
            try
            {
                Open();
                comandoPostgres.CommandText = "SELECT * FROM ser";
                //comandoSql.CommandText = "SELECT * FROM ser";
                lerDadosPostgres = comandoPostgres.ExecuteReader();
                //MySqlDataReader lerDadosSql = comandoSql.ExecuteReader();

                listaSeres = new List<Ser>();
                while (lerDadosPostgres.Read())
                {
                    ser = new Ser();
                    ser.Nome = (string)lerDadosPostgres[0];
                    ser.Nascimento = (DateTime)lerDadosPostgres[1];
                    ser.Sexo = (Sexo)Enum.Parse(typeof(Sexo), (string)lerDadosPostgres[2]);
                    ser.Raça = (Raça)Enum.Parse(typeof(Raça), (string)lerDadosPostgres[3]);

                    listaSeres.Add(ser);
                }
                return listaSeres;
            }
            catch (Exception ex)
            {
                //"Problema encontrado na seleção."
                MessageBox.Show(ex.ToString());
                throw;
            }
            finally
            {
                Close();
            }
        }

        public void Insert(Ser ser)
        {
            try
            {
                Open();
                comandoPostgres.CommandText = "INSERT INTO ser (nome, nascimento, sexo, raça) VALUES " + $"({ser.Nome}, {ser.Nascimento}, {ser.Sexo}, {ser.Raça})";
                comandoPostgres.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Problema encontrado na inserção.");
                throw;
            }
            finally
            {
                Close();
            }

            /*comandoPostgres.Parameters.AddWithValue("nome", ser.Nome);
            comandoPostgres.Parameters.AddWithValue("nascimento", ser.Nascimento);
            comandoPostgres.Parameters.AddWithValue("sexo", ser.Sexo);
            comandoPostgres.Parameters.AddWithValue("raça", ser.Raça);*/
        }
        public void Update(Ser ser)
        {
            try
            {
                Open();
                comandoPostgres.CommandText = "UPDATE ser SET " + $"nome = {ser.Nome} " + $"nascimento = {ser.Nascimento} " + $"sexo = {ser.Sexo} " + $"raça = {ser.Raça}";
                comandoPostgres.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Problema encontrado na edição.");
                throw;
            }
            finally
            {
                Close();
            }
            
            /*comandoPostgres.Parameters.AddWithValue("nome", ser.Nome);
            comandoPostgres.Parameters.AddWithValue("nascimento", ser.Nascimento);
            comandoPostgres.Parameters.AddWithValue("sexo", ser.Sexo);
            comandoPostgres.Parameters.AddWithValue("raça", ser.Raça);
            comandoPostgres.Parameters.AddWithValue("id", ser.Id);*/
        }

        public void Delete(Ser ser)
        {
            try
            {
                Open();
                comandoPostgres.CommandText = "DELETE FROM Ser WHERE " + $"nome = {ser.Nome}";
                comandoPostgres.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Problema encontrado na remoção.");
                throw;
            }
            finally
            {
                Close();
            }
        }
    }
}