using Facturacion_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion_MVC.Controllers
{
    public class TBLEmpleadoController : Controller
    {
        // LISTAR EMPLEADOS
        public IActionResult Index()
        {
            using (FacturacionContext db = new FacturacionContext())
            {
                var empleados = db.Tblempleados.ToList();
                return View(empleados);
            }
        }

        // FORMULARIO NUEVO EMPLEADO
        public IActionResult Nuevo()
        {
            return View();
        }

        // GUARDAR NUEVO EMPLEADO
        [HttpPost]
        public IActionResult Nuevo(Tblempleado empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (FacturacionContext db = new FacturacionContext())
                    {
                        empleado.DtmFechaModifica = DateTime.Now;
                        empleado.StrUsuarioModifico = Environment.UserName;
                        empleado.Activo = true;

                        db.Tblempleados.Add(empleado);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                return View(empleado);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(empleado);
            }
        }

        // FORMULARIO EDITAR EMPLEADO
        public IActionResult Editar(int id)
        {
            using (FacturacionContext db = new FacturacionContext())
            {
                var empleado = db.Tblempleados.FirstOrDefault(e => e.IdEmpleado == id);
                if (empleado == null)
                    return NotFound();

                return View(empleado);
            }
        }

        // GUARDAR CAMBIOS EN EDITAR
        [HttpPost]
        public IActionResult Editar(Tblempleado empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (FacturacionContext db = new FacturacionContext())
                    {
                        var empDB = db.Tblempleados.FirstOrDefault(e => e.IdEmpleado == empleado.IdEmpleado);

                        if (empDB != null)
                        {
                            empDB.StrNombre = empleado.StrNombre;
                            empDB.NumDocumento = empleado.NumDocumento;
                            empDB.StrDireccion = empleado.StrDireccion;
                            empDB.StrTelefono = empleado.StrTelefono;
                            empDB.StrEmail = empleado.StrEmail;
                            empDB.DtmIngreso = empleado.DtmIngreso;
                            empDB.DtmRetiro = empleado.DtmRetiro;
                            empDB.DtmFechaModifica = DateTime.Now;
                            empDB.StrUsuarioModifico = Environment.UserName;

                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }

                return View(empleado);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(empleado);
            }
        }

        // ELIMINAR 

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            try
            {
                using (var db = new FacturacionContext())
                {
                    var empleado = db.Tblempleados.Find(id);
                    if (empleado != null)
                    {
                        db.Tblempleados.Remove(empleado);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el empleado: " + ex.Message);
            }
        }
    }
}