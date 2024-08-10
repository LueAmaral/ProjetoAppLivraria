using MySql.Data.MySqlClient;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;
using System.Data;
using System.Security.Cryptography;

namespace ProjetoAppLivraria.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly string _conexaoMySQL;
        public AutorRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void AtualizarAutor(Autor autor)
        {
            throw new NotImplementedException();
        }

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

        public void ExcluirAutor(int autor)
        {
            throw new NotImplementedException();
        }

        public Autor ObterAutor(int Id)
        {
            throw new NotImplementedException();
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
                            nomeAutor = (string)(dr["nomeAutor"]),
                            status = Convert.ToString(dr["sta"]),
                        }
                        );
                }
                return Autlist;
            }
        }
    }
}
