using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vacunacion.Models;
using Vacunacion.Servicios;

namespace Vacunacion.Controllers
{
    public class VacunaController : Controller
    {
        // GET: Vacuna
        public ActionResult Index()
        {
            RepositorioVacunas repositorioVacunas = new RepositorioVacunas();
            var vacunas = repositorioVacunas.ObtenerVacunas();
            return View(vacunas);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Vacuna vacuna)
        {
            if (!ModelState.IsValid)
            {
                return View(vacuna);
            }

            RepositorioVacunas repositorioVacunas = new RepositorioVacunas();
            repositorioVacunas.Crear(vacuna);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            RepositorioVacunas repositorioVacunas = new RepositorioVacunas();
            var vacuna = repositorioVacunas.ObtenerPorId(id);

            if (vacuna is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(vacuna);
        }

        [HttpPost]
        public ActionResult Editar(Vacuna vacuna)
        {
            if (!ModelState.IsValid)
            {
                return View(vacuna);
            }

            RepositorioVacunas repositorioVacunas = new RepositorioVacunas();
            repositorioVacunas.Editar(vacuna);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Borrar(int id)
        {
            RepositorioVacunas repositorioVacunas = new RepositorioVacunas();
            var vacuna = repositorioVacunas.ObtenerPorId(id);

            if (vacuna is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            ViewBag.Error = "";
            return View(vacuna);
        }

        [HttpPost]
        public ActionResult Borrar(Vacuna vacu)
        {
            RepositorioVacunas repositorioVacunas = new RepositorioVacunas();
            var vacuna = repositorioVacunas.ObtenerPorId(vacu.Id);

            if (vacuna is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            bool mensaje = repositorioVacunas.Borrar(vacu.Id);
            if (mensaje)
            {
                ViewBag.Error = "1";
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}