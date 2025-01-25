using Domain;
using Microsoft.AspNetCore.Mvc;
using MVCCapas2025.Models;
using Services;

namespace MVCCapas2025.Controllers
{
    public class ProfesoresController : Controller
    {
        public IActionResult Index()
        {
            ProfesorServices service = new ProfesorServices();

            var profesors = service.Get();


            var model = profesors.Select(x => new ProfesorModel
            {
                ProfesorID = x.ProfesorID,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Especialidad = x.Especialidad,
                CorreoElectronico = x.CorreoElectronico
            }).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nombre, Apellido, Especialidad, CorreoElectronico")] ProfesorModel model)
        {

            if (ModelState.IsValid)
            {
                ProfesorServices service = new ProfesorServices();
                var dominio = new Profesor
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Especialidad = model.Especialidad,
                    CorreoElectronico = model.CorreoElectronico,
                    Estado = true
                };
                service.Insert(dominio);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProfesorServices service = new ProfesorServices();
            var profesor = service.BuscarPorID(id);
            var dominio = new ProfesorModel
            {
                ProfesorID = profesor.ProfesorID,
                Nombre = profesor.Nombre,
                Apellido = profesor.Apellido,
                Especialidad = profesor.Especialidad,
                CorreoElectronico = profesor.CorreoElectronico
            };
            return View(dominio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ProfesorServices service = new ProfesorServices();
            service.DeleteLogic(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
