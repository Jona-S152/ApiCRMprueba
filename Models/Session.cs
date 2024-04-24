using System;
using System.Collections.Generic;

namespace ApiCRMprueba.Models;

public partial class Session
{
    public DateTime? FechaIngreso { get; set; }

    public DateTime? FechaCierre { get; set; }

    public int? UsuariosIdUsuario { get; set; }

    public virtual Usuario? UsuariosIdUsuarioNavigation { get; set; }
}
