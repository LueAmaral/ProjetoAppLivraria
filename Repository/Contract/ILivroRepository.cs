using ProjetoAppLivraria.Models;

namespace ProjetoAppLivraria.Repository.Contract
{
    public interface ILivroRepository
    {
        IEnumerable<Livro> ObterTodosLivros();
        void CadastrarLivro(Livro livro);
        void AtualizarLivro(Livro livro);
        void ExcluirLivro(int Id);
        Livro ObterLivro(int Id);
    }
}
