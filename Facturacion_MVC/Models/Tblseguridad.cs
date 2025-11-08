using System;
using System.Collections.Generic;

namespace Facturacion_MVC.Models;

public partial class Tblseguridad
{
    public int IdSeguridad { get; set; }

    public int IdEmpleado { get; set; }

    public string? StrUsuario { get; set; }

    public string? StrClave { get; set; }

    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifico { get; set; }

    public virtual Tblempleado IdEmpleadoNavigation { get; set; } = null!;
}
