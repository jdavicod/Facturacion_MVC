using System;
using System.Collections.Generic;

namespace Facturacion_MVC.Models;

public partial class Tblrole
{
    public int IdRolEmpleado { get; set; }

    public string? NombreRol { get; set; }

    public string DescripcionRol { get; set; } = null!;

    public virtual ICollection<Tblempleado> Tblempleados { get; set; } = new List<Tblempleado>();
}
