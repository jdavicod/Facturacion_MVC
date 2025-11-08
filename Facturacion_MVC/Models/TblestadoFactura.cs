using System;
using System.Collections.Generic;

namespace Facturacion_MVC.Models;

public partial class TblestadoFactura
{
    public int IdEstadoFactura { get; set; }

    public string? StrDescripcion { get; set; }

    public virtual ICollection<Tblfactura> Tblfacturas { get; set; } = new List<Tblfactura>();
}
