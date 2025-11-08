using System;
using System.Collections.Generic;

namespace Facturacion_MVC.Models;

public partial class Tblcliente
{
    public int IdCliente { get; set; }

    public string? StrNombre { get; set; }

    public long? NumDocumento { get; set; }

    public string? StrDireccion { get; set; }

    public string? StrTelefono { get; set; }

    public string? StrEmail { get; set; }

    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifica { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Tblfactura> Tblfacturas { get; set; } = new List<Tblfactura>();
}
