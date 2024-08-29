using ProjetoAppLivraria.Models;

namespace ProjetoAppLivraria.Repository.Contract
{
    public interface IStatusRepository
    {
        IEnumerable<Status> ObterTodosStatus();
        Status ObterStatusPorID(int id);
        void CadastrarStatus(Status status);
        void EditarStatus(Status status);
        void ExcluirStatus(int status);
    }
}
