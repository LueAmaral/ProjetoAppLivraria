using System.ComponentModel.DataAnnotations;

namespace ProjetoAppLivraria.Models
{
    public class Livro
    {
        [Display(Name = "Código")]
        public int codLivro { get; set; }
        [Display(Name = "Livro")]
        public string nomeLivro { get; set; }
        public Autor RefAutor { get; set; }
        [Display(Name = "Autor")]
        public List<Autor> ListaAutor { get; set; }
    }
}
