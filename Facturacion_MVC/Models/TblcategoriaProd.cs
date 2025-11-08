using System;
using System.Collections.Generic;

namespace Facturacion_MVC.Models;

public partial class TblcategoriaProd
{
    public int IdCategoria { get; set; }

    public string? StrDescripcion { get; set; }

    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifico { get; set; }

    public virtual ICollection<Tblproducto> Tblproductos { get; set; } = new List<Tblproducto>();
}
