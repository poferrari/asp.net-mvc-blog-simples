using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Web.Models.Acesso
{
    public class Login
    {
        [Display(Name = "E-mail")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "O e-mail informado é inválido.")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required]
        [StringLength(55, MinimumLength = 3)]
        [DataType(DataType.Password)]        
        public string Senha { get; set; }
    }
}