using Microsoft.AspNetCore.Mvc;

namespace Facturacion_MVC.Controllers
{
    public class TBLCategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    

        public IActionResult Nuevo()
        {
            return View();

        }
    }
    
}
