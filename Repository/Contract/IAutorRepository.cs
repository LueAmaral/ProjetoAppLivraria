﻿using ProjetoAppLivraria.Models;

namespace ProjetoAppLivraria.Repository.Contract
{
    public interface IAutorRepository
    {
        IEnumerable<Autor> ObterTodosAutores();
        void CadastrarAutor(Autor autor);
        void EditarAutor(Autor autor);
        void ExcluirAutor(int autor);
        Autor ObterAutor(int Id);
    }
}
