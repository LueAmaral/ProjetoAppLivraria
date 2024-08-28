using MySql.Data.MySqlClient;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;
using System.Data;
using System.Security.Cryptography;

namespace ProjetoAppLivraria.Repository
{
    public class AutorRepository(IConfiguration conf) : IAutorRepository
    {
        private readonly string _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");

        public void CadastrarAutor(Autor autor)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbAutor (nomeAutor, sta) values (@nomeAutor, @sta)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@nomeAutor", MySqlDbType.VarChar).Value = autor.nomeAutor;
                cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = autor.status;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void EditarAutor(Autor autor)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE tbAutor SET nomeAutor = @nomeAutor, sta = @sta WHERE codAutor = @codAutor", conexao);

                cmd.Parameters.Add("@nomeAutor", MySqlDbType.VarChar).Value = autor.nomeAutor;
                cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = autor.status;
                cmd.Parameters.Add("@codAutor", MySqlDbType.Int32).Value = autor.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void ExcluirAutor(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM tbAutor WHERE codAutor = @codAutor", conexao);
                cmd.Parameters.Add("@codAutor", MySqlDbType.Int32).Value = id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Autor ObterAutor(int id)
        {
            Autor autor = null;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbAutor WHERE codAutor = @codAutor", conexao);
                cmd.Parameters.Add("@codAutor", MySqlDbType.Int32).Value = id;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        autor = new Autor
                        {
                            Id = Convert.ToInt32(reader["codAutor"]),
                            nomeAutor = reader["nomeAutor"] == DBNull.Value ? null : reader["nomeAutor"].ToString(),
                            status = reader["sta"] == DBNull.Value ? null : reader["sta"].ToString(),
                        };
                    }
                }

                conexao.Close();
            }

            return autor;
        }


        public IEnumerable<Autor> ObterTodosAutores()
        {
            List<Autor> Autlist = new List<Autor>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbautor", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    Autlist.Add(
                        new Autor
                        {
                            Id = Convert.ToInt32(dr["codAutor"]),
                            nomeAutor = dr["nomeAutor"] == DBNull.Value ? null : (string)dr["nomeAutor"],
                            status = Convert.ToString(dr["sta"]),
                        }
                        );
                }
                return Autlist;
            }
        }
    }
}
