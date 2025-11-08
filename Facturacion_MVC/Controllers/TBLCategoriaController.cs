using Facturacion_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion_MVC.Controllers
{
    public class TBLCategoriaController : Controller
    {
        public IActionResult Index()
        {
            FacturacionContext db = new FacturacionContext();

            var categorias = db.TblcategoriaProds;

            return View(categorias.ToList());
        }


        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(TblcategoriaProd categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FacturacionContext db = new FacturacionContext();

                    // Asignar valores automáticos
                    categoria.DtmFechaModifica = DateTime.Now;
                    categoria.StrUsuarioModifico = Environment.UserName;

                    // Guardar en la base de datos
                    db.TblcategoriaProds.Add(categoria);
                    db.SaveChanges();

                    return Redirect("/TBLCategoria");
                }

                return View(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
