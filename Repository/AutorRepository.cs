using MySql.Data.MySqlClient;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;
using System.Data;

namespace ProjetoAppLivraria.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly string _conexaoMySQL;

        public AutorRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void CadastrarAutor(Autor autor)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbAutor (nomeAutor, sta) VALUES (@nomeAutor, @sta)", conexao);

                cmd.Parameters.Add("@nomeAutor", MySqlDbType.VarChar).Value = autor.nomeAutor;
                cmd.Parameters.Add("@sta", MySqlDbType.Int32).Value = autor.StatusId;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void EditarAutor(Autor autor)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE tbAutor SET nomeAutor = @nomeAutor, sta = @StatusId " +
                                                    "WHERE codAutor = @codAutor", conexao);

                cmd.Parameters.Add("@nomeAutor", MySqlDbType.VarChar).Value = autor.nomeAutor;
                cmd.Parameters.Add("@StatusId", MySqlDbType.Int32).Value = autor.StatusId;
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

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbAutor a JOIN tbStatus s ON a.sta = s.codStatus WHERE codAutor = @codAutor", conexao);
                cmd.Parameters.Add("@codAutor", MySqlDbType.Int32).Value = id;

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        autor = new Autor
                        {
                            Id = Convert.ToInt32(reader["codAutor"]),
                            nomeAutor = reader["nomeAutor"] == DBNull.Value ? null : reader["nomeAutor"].ToString(),
                            StatusId = reader["sta"] == DBNull.Value ? 0 : Convert.ToInt32(reader["sta"]),
                            Status = new Status
                            {
                                Id = Convert.ToInt32(reader["codStatus"]),
                                Nome = reader["sta"].ToString()
                            }
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
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT a.codAutor, a.nomeAutor, s.codStatus, s.sta " +
                    "FROM tbAutor a " +
                    "JOIN tbStatus s ON a.sta = s.codStatus", conexao);
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
                            StatusId = Convert.ToInt32(dr["codStatus"]),
                            Status = new Status
                            {
                                Id = Convert.ToInt32(dr["codStatus"]),
                                Nome = dr["sta"].ToString()
                            }
                        }
                    );
                }
                return Autlist;
            }
        }

    }
}
