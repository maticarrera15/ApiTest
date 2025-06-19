using System;
using System.Collections.Generic;

namespace ApiTest.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? User { get; set; }
        public int? Documento { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
