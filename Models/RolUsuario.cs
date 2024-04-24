using System;
using System.Collections.Generic;

namespace ApiCRMprueba.Models;

public partial class RolUsuario
{
    public int? RolIdRol { get; set; }

    public int? UsuariosIdUsuario { get; set; }

    public virtual Rol? RolIdRolNavigation { get; set; }

    public virtual Usuario? UsuariosIdUsuarioNavigation { get; set; }
}
