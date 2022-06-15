using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vacunacion.Models;
using Vacunacion.Servicios;

namespace Vacunacion.Controllers
{
    public class MunicipioController : Controller
    {
        public ActionResult Index()
        {
            RepositorioMunicipios repositorioMunicipios = new RepositorioMunicipios();
            var municipios = repositorioMunicipios.ObtenerMunicipios();
            return View(municipios);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Municipio municipio)
        {
            if (!ModelState.IsValid)
            {
                return View(municipio);
            }

            RepositorioMunicipios repositorioMunicipios = new RepositorioMunicipios();
            repositorioMunicipios.Crear(municipio);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            RepositorioMunicipios repositorioMunicipios = new RepositorioMunicipios();
            var município = repositorioMunicipios.ObtenerPorId(id);

            if (município is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(município);
        }

        [HttpPost]
        public ActionResult Editar(Municipio municipio)
        {
            if (!ModelState.IsValid)
            {
                return View(municipio);
            }

            RepositorioMunicipios repositorioMunicipios = new RepositorioMunicipios();
            repositorioMunicipios.Editar(municipio);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Borrar(int id)
        {
            RepositorioMunicipios repositorioMunicipios = new RepositorioMunicipios();
            var município = repositorioMunicipios.ObtenerPorId(id);

            if (município is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            ViewBag.Error = "";
            return View(município);
        }

        [HttpPost]
        public ActionResult Borrar(Municipio mun)
        {
            RepositorioMunicipios repositorioMunicipios = new RepositorioMunicipios();
            var município = repositorioMunicipios.ObtenerPorId(mun.Id);

            if (município is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            bool mensaje = repositorioMunicipios.Borrar(mun.Id);
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