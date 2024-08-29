using System.ComponentModel.DataAnnotations;

namespace ProjetoAppLivraria.Models
{
    public class Autor
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "O código do autor é obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        public string nomeAutor { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public string ObterStatusDescricao()
        {
            return Status != null ? Status.Nome : "Status desconhecido";
        }
    }
}
