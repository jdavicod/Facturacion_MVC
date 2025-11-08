using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facturacion_MVC.Models;

public partial class TblcategoriaProd
{

[Key]
    public int IdCategoria { get; set; }

    [Display(Name = "Descripción Categoria")]
    public string? StrDescripcion { get; set; }

    public DateTime? DtmFechaModifica { get; set; }

    public string? StrUsuarioModifico { get; set; }

    public virtual ICollection<Tblproducto> Tblproductos { get; set; } = new List<Tblproducto>();
}
