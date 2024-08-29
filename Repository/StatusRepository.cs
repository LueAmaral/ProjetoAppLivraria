using MySql.Data.MySqlClient;
using ProjetoAppLivraria.Models;
using ProjetoAppLivraria.Repository.Contract;
using System.Data;

namespace ProjetoAppLivraria.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly string _conexaoMySQL;

        public StatusRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("conexaoMySQL");
        }

        public void AtualizarStatus(Status status)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tbStatus SET sta = @sta WHERE codStatus = @codStatus", conexao);

                cmd.Parameters.Add("@codStatus", MySqlDbType.Int32).Value = status.Id;
                cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = status.Nome;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void CadastrarStatus(Status status)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbStatus (sta) VALUES (@sta)", conexao);

                cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = status.Nome;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void EditarStatus(Status status)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tbStatus SET sta = @sta WHERE codStatus = @codStatus", conexao);

                cmd.Parameters.Add("@codStatus", MySqlDbType.Int32).Value = status.Id;
                cmd.Parameters.Add("@sta", MySqlDbType.VarChar).Value = status.Nome;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void ExcluirStatus(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM tbStatus WHERE codStatus = @codStatus", conexao);
                cmd.Parameters.AddWithValue("@codStatus", id);

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Status ObterStatusPorID(int id)
        {
            Status status = null;

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT codStatus, sta FROM tbStatus WHERE codStatus = @codStatus", conexao);
                cmd.Parameters.AddWithValue("@codStatus", id);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        status = new Status
                        {
                            Id = Convert.ToInt32(dr["codStatus"]),
                            Nome = dr["sta"].ToString()
                        };
                    }
                }
                conexao.Close();
            }

            return status;
        }

        public IEnumerable<Status> ObterTodosStatus()
        {
            List<Status> statusList = new List<Status>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT codStatus, sta FROM tbStatus", conexao);

                using (var da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        statusList.Add(new Status
                        {
                            Id = Convert.ToInt32(dr["codStatus"]),
                            Nome = dr["sta"].ToString()
                        });
                    }
                }
                conexao.Close();
            }

            return statusList;
        }
    }
}
