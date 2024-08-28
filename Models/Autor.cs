using System.ComponentModel.DataAnnotations;

namespace ProjetoAppLivraria.Models
{
    public class Autor
    {

        [Display(Name = "Situação")]
        internal object status;

        [Display(Name = "Código")]
        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        public int Id { get; set; }
        [Display(Name = "Autor")]
        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        public string nomeAutor { get; set; }
    }
}
