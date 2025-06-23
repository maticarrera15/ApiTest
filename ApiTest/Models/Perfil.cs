using System;
using System.Collections.Generic;

namespace ApiTest.Models
{
    public partial class Perfil
    {
        public int IdPerfil { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Usuario> Users { get; set; }
    }
}
