using System;
using System.Collections.Generic;

namespace ApiCRMprueba.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Mail { get; set; }

    public string? SessionActive { get; set; }

    public int? PersonaIdPersona { get; set; }

    public string? Status { get; set; }

    public virtual Persona? oPersona { get; set; }
}
