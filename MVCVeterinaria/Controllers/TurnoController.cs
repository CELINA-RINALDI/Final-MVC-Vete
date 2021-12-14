using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCVeterinaria.Context;
using MVCVeterinaria.Models;

namespace MVCVeterinaria.Controllers
{
    public class TurnoController : Controller
    {
        private readonly VeterinariaDbContext context;
        public TurnoController(VeterinariaDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var turnos = from m in context.Turnos
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                turnos = turnos.Where(s => s.NombreMascota!.Contains(searchString));
            }

            return View(await turnos.ToListAsync());
        }

        public ActionResult TurnosDeHoy()
        {
            List<Turno> turnos = (from t in context.Turnos
                                  where t.Fecha == DateTime.Today
                                  select t).ToList();
            return View("Index", turnos);
        }

        private ActionResult CheckearDatos(Turno t)
        {
            bool FechaYaExiste = context.Turnos.Any(x => x.Fecha == t.Fecha);
            bool HoraYaExiste = context.Turnos.Any(x => x.Hora == t.Hora);
           
            if (FechaYaExiste && HoraYaExiste)
            {
                ModelState.AddModelError("Fecha", "Ya existe un turno con esta fecha y hora");
            }
            return View(t);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Turno turno = new Turno();

            return View("Create", turno);
        }

        [HttpPost]
        public ActionResult Create(Turno t)
        {
            CheckearDatos(t);
           asignarMedico(t, t.Medico.Apellido);
            if (ModelState.IsValid)
            {
                context.Turnos.Add(t);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", t);
        }

        private Medico asignarMedico(Turno t, string apellido)
        {
            var medico = context.Medicos.Where(x => x.Apellido == apellido).FirstOrDefault();
            if (medico == null)
            {
                ModelState.AddModelError("Medico.Apellido", "No se encontro un medico con este apellido");
            }
            else if (t.Medico.Nombre != medico.Nombre)
            {
                ModelState.AddModelError("Medico.Nombre", "No se encontro un medico con este nombre");
            }
            else
            {
                t.MedicoId = medico.MedicoId;
                t.Medico = medico; 
            }
            return medico; 

        }

        public ActionResult Details(int id)
        {
            Turno turno = context.Turnos.Find(id);
            turno.Medico = context.Medicos.Find(turno.MedicoId);
            if (turno != null)
            {
                return View("Detail", turno);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult Delete(int id)
        {
            Turno turno = context.Turnos.Find(id);
            turno.Medico = context.Medicos.Find(turno.MedicoId);
            if (turno != null)
            {
                return View("Delete", turno);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Turno turno = context.Turnos.Find(id);
            context.Turnos.Remove(turno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Turno turno = context.Turnos.Find(id);
            turno.Medico = context.Medicos.Find(turno.MedicoId);
            if (turno != null)
            {
                return View("Edit", turno);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(Turno t)
        {
            asignarMedico(t, t.Medico.Apellido);
            if (ModelState.IsValid)
            {
                context.Entry(t).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", t);
        }

        public ActionResult BuscarPorNombre(string nombre)
        {
            if (nombre == "")
            {
                return RedirectToAction("Index");
            }
            List<Turno> turnos = (from t in context.Turnos
                                  where t.NombreMascota == nombre 
                                  select t).ToList();
            return View("Index", turnos);
        }
     

    }

}
