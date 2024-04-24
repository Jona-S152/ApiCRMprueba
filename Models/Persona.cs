using System;
using System.Collections.Generic;

namespace ApiCRMprueba.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string? Identificacion { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
