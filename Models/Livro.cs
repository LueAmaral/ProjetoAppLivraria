using System.ComponentModel.DataAnnotations;

namespace ProjetoAppLivraria.Models
{
    public class Livro
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        public int codLivro { get; set; }
        [Display(Name = "Livro")]
        [Required(ErrorMessage = "O nome do livro é obrigatório")]
        public string nomeLivro { get; set; }
        [Display(Name = "Autor")]
        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        public Autor RefAutor { get; set; }
        public List<Autor> ListaAutor { get; set; }
    }
}
