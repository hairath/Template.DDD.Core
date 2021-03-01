using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é campo obrigatório para login")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [StringLength(200, ErrorMessage = "Email deve ter no máximo 200 caracteres")]
        public string Email { get; set; }
    }
}
