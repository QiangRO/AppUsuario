﻿using System.ComponentModel.DataAnnotations;

namespace AppUsuario.Models
{
    public class RegistroVM
    {
        [Required(ErrorMessage ="El email es obligatorio")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage ="La contraseña es obligatoria")]
        [StringLength(50,ErrorMessage ="El {0} debe estar  entre al menos {2} caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "La confirmacion de contraseña es obligatoria")]
        [Compare("Password", ErrorMessage = "La contraseña y confirmacion de contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }
        public string? Url { get; set; }
        public int CodigoPais { get; set; }
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "El pais es obligatorio")]
        public string? Pais { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage ="El estado es obligatorio")]
        public bool Estado { get; set; }
    }
}
