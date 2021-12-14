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
    public class MedicoController : Controller
    {
        private readonly VeterinariaDbContext context;
        public MedicoController(VeterinariaDbContext context)
        {
            this.context = context;
        }
        public ActionResult Index()
        {
            List<Medico> medicos = (from med in context.Medicos
                                  select med).ToList();
            return View("Index", medicos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Medico m = new Medico();

            return View("Create", m);
        }
        private ActionResult CheckearDatos(Medico m)
        {
            bool MedicoApellidoExiste = context.Medicos.Any(x => x.Apellido == m.Apellido);
            bool MedicoNombreExiste = context.Medicos.Any(x => x.Nombre == m.Nombre);
            bool MatriculaRepetida = context.Medicos.Any(x => x.NroMatricula == m.NroMatricula);
            if (MedicoApellidoExiste && MedicoNombreExiste)
            {
                ModelState.AddModelError("Nombre", "El médico ya existe");
            }
            if (MatriculaRepetida)
            {
                ModelState.AddModelError("NroMatricula", "Matricula repetida");
            }
            return View(m);
        }
        [HttpPost]
        public ActionResult Create(Medico m)
        {
            CheckearDatos(m);
            if (ModelState.IsValid)
            {
                context.Medicos.Add(m);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", m);
        }

        public ActionResult Details(int id)
        {
            Medico medico = context.Medicos.Find(id);
            if (medico != null)
            {
                return View("Details", medico);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult Delete(int id)
        {
            Medico medico = context.Medicos.Find(id);
            if (medico != null)
            {
                return View("Delete", medico);
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
            Medico m = context.Medicos.Find(id);
            context.Medicos.Remove(m);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Medico medico = context.Medicos.Find(id);
            if (medico != null)
            {
                return View("Edit", medico);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(Medico m)
        {
            if (context.Medicos.Any(x => x.NroMatricula == m.NroMatricula))
            {
                ModelState.AddModelError("NroMatricula", "Matricula repetida");
            }
            if (ModelState.IsValid)
            {
                context.Entry(m).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", m);
        }
    }
}
