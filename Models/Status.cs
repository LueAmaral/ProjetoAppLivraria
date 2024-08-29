using System.ComponentModel.DataAnnotations;

namespace ProjetoAppLivraria.Models
{
    public class Status
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome do Status")]
        [Required(ErrorMessage = "O nome do status é obrigatório")]
        public string Nome { get; set; }
    }
}
