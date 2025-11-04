using Microsoft.AspNetCore.Mvc;

namespace Facturacion_MVC.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ayuda()
        {
            return View();
        }

        public IActionResult Acerca()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }
    }
}
