using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facturacion_MVC.Models;

public partial class Tblproducto
{
    public int IdProducto { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(30)]
    public string StrNombre { get; set; } = null!;

    [Display(Name = "Codigo")]
    [StringLength(30)]
    public string? StrCodigo { get; set; }

    [Display(Name = "Precio de Compra")]
    [Required(ErrorMessage = "El precio de compra es obligatorio")]
    public double? NumPrecioCompra { get; set; }

    [Display(Name = "Precio de Venta")]
    [Required(ErrorMessage = "El precio de venta es obligatorio")]
    public double NumPrecioVenta { get; set; }

    [Display(Name = "Categoria")]
    [Required(ErrorMessage = "Debe seleccionar una categoría")]
    public int IdCategoria { get; set; }

    [Display(Name = "Detalle del producto")]
    [StringLength(50)]
    public string? StrDetalle { get; set; }

    [Display(Name = "Imagen")]
    [StringLength(50)]
    public string? StrFoto { get; set; }

    [Display(Name = "Cantidad Stock")]
    [Required(ErrorMessage = "El stock es obligatorio")]
    public int? NumStock { get; set; }

    public DateTime DtmFechaModifica { get; set; }

    public string? StrUsuarioModifica { get; set; }

    public virtual TblcategoriaProd? IdCategoriaNavigation { get; set; }

    public virtual ICollection<TbldetalleFactura> TbldetalleFacturas { get; set; } = new List<TbldetalleFactura>();
}
