using Facturacion_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion_MVC.Controllers
{
    public class TBLProductosController : Controller
    {

        public IActionResult Index()
        {
            FacturacionContext db = new FacturacionContext();

            var productos = db.Tblproductos;

            return View(productos.ToList());
        }
    }
}
