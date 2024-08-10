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
                MySqlCommand cmd = new MySqlCommand("Update tblivro set nomeLivro=@nomeLivro, codAutor=@codAutor " +
                                                    " Where codLivro=@codLivro ", conexao);

                cmd.Parameters.Add("@codLivro", MySqlDbType.VarChar).Value = livro.codLivro;
                cmd.Parameters.Add("@nomeLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@codAutor", MySqlDbType.VarChar).Value = livro.RefAutor.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }

        public void CadastrarLivro(Livro livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tblivro (nomeLivro, codAutor) values (@nomeLivro, @codAutor)", conexao);

                cmd.Parameters.Add("@nomLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@codAutor", MySqlDbType.Int32).Value = livro.RefAutor.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void ExcluirLivro(int livro)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tblivro where codLivro=@codLivro", conexao);
                cmd.Parameters.AddWithValue("@codLivro", livro);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Livro ObterLivro(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tblivro as t1 " +
                " INNER JOIN tbautor as t2 ON t1.codAutor = t2.codAutor where codLivro=@codLivro", conexao);
                cmd.Parameters.AddWithValue("@codLivro", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Livro livro = new Livro();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    livro.codLivro = Convert.ToInt32(dr["codLivro"]);
                    livro.nomeLivro = (string)(dr["nomeLivro"]);
                    livro.RefAutor = new Autor()
                    {
                        Id = Convert.ToInt32(dr["codAutor"]),
                        nomeAutor = (string)(dr["nomeAutor"]),
                        status = (string)(dr["sta"])
                    };
                }
                return livro;
            }
        }

        public IEnumerable<Livro> ObterTodosLivros()
        {
            List<Livro> Livrolist = new List<Livro>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tblivro as t1" + " INNER JOIN tbautor as t2 ON t1.codAutor = t2.codAutor", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    Livrolist.Add(
                        new Livro
                        {
                            codLivro = Convert.ToInt32(dr["codLivro"]),
                            nomeLivro = (string)(dr["nomLivro"]),
                            RefAutor = new Autor
                            {
                                Id = Convert.ToInt32(dr["codAutor"]),
                                nomeAutor = (string)(dr["nomAutor"]),
                                status = (string)(dr["sta"])
                            }
                    });
                }
                return Livrolist;
            }
        }
    }
}
