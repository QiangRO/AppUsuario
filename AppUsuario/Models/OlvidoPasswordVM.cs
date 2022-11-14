using System.ComponentModel.DataAnnotations;
namespace AppUsuario.Models
{
    public class OlvidoPasswordVM
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
