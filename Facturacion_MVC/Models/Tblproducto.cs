using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facturacion_MVC.Models;

public partial class Tblproducto
{
    public int IdProducto { get; set; }

    [Display(Name = "Nombre")]
    public string StrNombre { get; set; } = null!;

    // Puede ser NULL en la base de datos: marcar nullable
    public string? StrCodigo { get; set; }

    // Si la columna NumPrecioCompra puede ser NULL, usar nullable
    public double? NumPrecioCompra { get; set; }

    [Display(Name = "Precio de Venta")]
    public double NumPrecioVenta { get; set; }

    [Display(Name = "Categoria")]
    public int IdCategoria { get; set; }

    public string? StrDetalle { get; set; }

    public string? StrFoto { get; set; }

    [Display(Name = "Cantidad")]
    public int? NumStock { get; set; }

    public DateTime DtmFechaModifica { get; set; }

    // Si la columna puede venir NULL desde la BD, marcar nullable
    public string? StrUsuarioModifica { get; set; }

    // Propiedad de navegación puede ser NULL si no hay FK establecida
    public virtual TblcategoriaProd? IdCategoriaNavigation { get; set; }

    public virtual ICollection<TbldetalleFactura> TbldetalleFacturas { get; set; } = new List<TbldetalleFactura>();
}
