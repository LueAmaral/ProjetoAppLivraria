using MySql.Data.MySqlClient;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;
using System.Data;

namespace ProjetoAppLivraria.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly string _conexaoMySQL;

        public LivroRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("conexaoMySQL");
        }

        public void AtualizarLivro(Livro livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tbLivro SET nomeLivro = @nomeLivro, codAutor = @codAutor " +
                                                    "WHERE codLivro = @codLivro", conexao);

                cmd.Parameters.Add("@codLivro", MySqlDbType.Int32).Value = livro.codLivro;
                cmd.Parameters.Add("@nomeLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@codAutor", MySqlDbType.Int32).Value = livro.RefAutor.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void CadastrarLivro(Livro livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbLivro (nomeLivro, codAutor) VALUES (@nomeLivro, @codAutor)", conexao);

                cmd.Parameters.Add("@nomeLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@codAutor", MySqlDbType.Int32).Value = livro.RefAutor.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void ExcluirLivro(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM tbLivro WHERE codLivro = @codLivro", conexao);
                cmd.Parameters.AddWithValue("@codLivro", id);

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Livro ObterLivro(int id)
        {
            Livro livro = null;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT t1.codLivro, t1.nomeLivro, t2.codAutor, t2.nomeAutor, t3.codStatus, t3.sta " +
                                                    "FROM tbLivro t1 " +
                                                    "INNER JOIN tbAutor t2 ON t1.codAutor = t2.codAutor " +
                                                    "INNER JOIN tbStatus t3 ON t2.sta = t3.codStatus " +
                                                    "WHERE t1.codLivro = @codLivro", conexao);
                cmd.Parameters.AddWithValue("@codLivro", id);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        livro = new Livro
                        {
                            codLivro = Convert.ToInt32(dr["codLivro"]),
                            nomeLivro = dr["nomeLivro"].ToString(),
                            RefAutor = new Autor
                            {
                                Id = Convert.ToInt32(dr["codAutor"]),
                                nomeAutor = dr["nomeAutor"].ToString(),
                                Status = new Status
                                {
                                    Id = Convert.ToInt32(dr["codStatus"]),
                                    Nome = dr["sta"].ToString()
                                }
                            }
                        };
                    }
                }
                conexao.Close();
            }

            return livro;
        }

        public IEnumerable<Livro> ObterTodosLivros()
        {
            List<Livro> Livrolist = new List<Livro>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT t1.codLivro, t1.nomeLivro, t2.codAutor, t2.nomeAutor, t3.codStatus, t3.sta " +
                                                    "FROM tbLivro t1 " +
                                                    "INNER JOIN tbAutor t2 ON t1.codAutor = t2.codAutor " +
                                                    "INNER JOIN tbStatus t3 ON t2.sta = t3.codStatus", conexao);

                using (var da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Livrolist.Add(new Livro
                        {
                            codLivro = Convert.ToInt32(dr["codLivro"]),
                            nomeLivro = dr["nomeLivro"].ToString(),
                            RefAutor = new Autor
                            {
                                Id = Convert.ToInt32(dr["codAutor"]),
                                nomeAutor = dr["nomeAutor"].ToString(),
                                Status = new Status
                                {
                                    Id = Convert.ToInt32(dr["codStatus"]),
                                    Nome = dr["sta"].ToString()
                                }
                            }
                        });
                    }
                }
                conexao.Close();
            }

            return Livrolist;
        }
    }
}
