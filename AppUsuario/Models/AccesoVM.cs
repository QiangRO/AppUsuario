using System.ComponentModel.DataAnnotations;

namespace AppUsuario.Models
{
    public class AccesoVM
    {
        [Required(ErrorMessage ="El email es obligatorio")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage ="La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Password{ get; set; }
        [Display(Name ="Recordar datos?")]
        public bool RememberMe { get; set; }//boton los datos de acceso
    }
}
