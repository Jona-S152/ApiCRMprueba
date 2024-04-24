using System;
using System.Collections.Generic;

namespace ApiCRMprueba.Models;

public partial class RolRolOpcione
{
    public int? RolIdRol { get; set; }

    public int? RolOpcionesIdOpciones { get; set; }

    public virtual Rol? RolIdRolNavigation { get; set; }

    public virtual RolOpcione? RolOpcionesIdOpcionesNavigation { get; set; }
}
