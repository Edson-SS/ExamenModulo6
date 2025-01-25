using Domain;
using Infraestructure;
using Microsoft.AspNetCore.Mvc;
using MVCCapas2025.Models;
using Services;

namespace MVCCapas2025.Controllers
{
    public class AlumnoController : Controller
    {
        public IActionResult Index()
        {
            AlumnoServices service = new AlumnoServices();

            var alumnos = service.Get();


            var model = alumnos.Select(x => new AlumnoModel
            {
                AlumnoID = x.AlumnoID,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                FechaNacimiento = x.FechaNacimiento,
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
        public IActionResult Create([Bind("Nombre, Apellido, FechaNacimiento, CorreoElectronico")] AlumnoModel model)
        {

            if (ModelState.IsValid)
            {
                AlumnoServices service = new AlumnoServices();
                var dominio = new Alumno
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    FechaNacimiento = model.FechaNacimiento,
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
            AlumnoServices service = new AlumnoServices();
            var alumno = service.BuscarPorID(id);
            var dominio = new AlumnoModel
            {
                AlumnoID = alumno.AlumnoID,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                FechaNacimiento = alumno.FechaNacimiento,
                CorreoElectronico = alumno.CorreoElectronico
            };
            return View(dominio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            AlumnoServices service = new AlumnoServices();
            service.DeleteLogic(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
