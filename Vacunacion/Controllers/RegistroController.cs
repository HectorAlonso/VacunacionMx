using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vacunacion.Models;
using Vacunacion.Servicios;

namespace Vacunacion.Controllers
{
    public class RegistroController : Controller
    {
        // GET: Registro
        public ActionResult Index()
        {
            RepositorioPersona repositorioPersona = new RepositorioPersona();
            Persona persona = new Persona();
            persona = (Persona)Session["Usuario"];
            if (Session["Usuario"] is null)
            {
                return RedirectToAction("Crear", "Persona");
            }
            RepositorioRegistroVacunas RepoRegistro = new RepositorioRegistroVacunas();
            var Registros = RepoRegistro.ObtenerPorFolio(persona.Folio);
            return View(Registros);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            CargarDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Registro modelo)
        {
            RepositorioVacunas repoVacunas = new RepositorioVacunas();
            RepositorioMunicipios repoMunicipios = new RepositorioMunicipios();
            RepositorioRegistroVacunas repoRegistro = new RepositorioRegistroVacunas();
            RepositorioDosis repoDosis = new RepositorioDosis();

            Persona persona = new Persona();
            persona = (Persona)Session["Usuario"];

            if (!ModelState.IsValid)
            {
                CargarDropDownList();
                return View(modelo);
            }

            //Validamos que exista la vacuna
            var vacuna = repoVacunas.ObtenerPorId(modelo.VacunaId);
            if( vacuna is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            //Validamos que exista el municipio
            var municipio = repoMunicipios.ObtenerPorId(modelo.MunicipioId);
            if(municipio is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var dosis = repoDosis.ObtenerPorId(modelo.DosisId);
            if (dosis is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            modelo.FolioId = persona.Folio;
            repoRegistro.Crear(modelo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            RepositorioRegistroVacunas repoRegistro = new RepositorioRegistroVacunas();
            var registro = repoRegistro.ObtenerPorId(id);

            if(registro is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            CargarDropDownList();
            return View(registro);
        }

        [HttpPost]
        public ActionResult Editar(Registro reg)
        {
            RepositorioVacunas repoVacunas = new RepositorioVacunas();
            RepositorioMunicipios repoMunicipios = new RepositorioMunicipios();
            RepositorioRegistroVacunas repoRegistro = new RepositorioRegistroVacunas();
            RepositorioDosis repoDosis = new RepositorioDosis();

            if (!ModelState.IsValid)
            {
                CargarDropDownList();
                return View(reg);
            }

            //Validamos que exista la vacuna
            var vacuna = repoVacunas.ObtenerPorId(reg.VacunaId);
            if (vacuna is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            //Validamos que exista el municipio
            var municipio = repoMunicipios.ObtenerPorId(reg.MunicipioId);
            if (municipio is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var dosis = repoDosis.ObtenerPorId(reg.DosisId);
            if (dosis is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            repoRegistro.Editar(reg);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Borrar(int id)
        {
            RepositorioRegistroVacunas repositorioRegistro = new RepositorioRegistroVacunas();
            var registro = repositorioRegistro.ObtenerPorId(id);

            if (registro is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(registro);
        }

        [HttpPost]
        public ActionResult Borrar(Registro reg)
        {
            RepositorioRegistroVacunas repositorioRegistro = new RepositorioRegistroVacunas();
            var registro = repositorioRegistro.ObtenerPorId(reg.Id);

            if (registro is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            repositorioRegistro.Borrar(reg.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        private void ObtenerVacunas()
        {
            List<SelectListItem> ListaVacunas = new List<SelectListItem>();
            RepositorioVacunas repositorioVacunas = new RepositorioVacunas();
            var vacunas = repositorioVacunas.ObtenerVacunas();
            foreach (var vacuna in vacunas)
            {
                ListaVacunas.Add(new SelectListItem
                {
                    Text = vacuna.Nombre,
                    Value = vacuna.Id.ToString()
                });
            }
            ListaVacunas.Insert(0, new SelectListItem { Text = "---Seleccione---", Value = "" });
            ViewBag.ListaVacunas = ListaVacunas;
        }

        [HttpGet]
        private void ObtenerMunicipios()
        {
            List<SelectListItem> ListaMunicipios = new List<SelectListItem>();
            RepositorioMunicipios repositorioMunicipios = new RepositorioMunicipios();
            var municipios = repositorioMunicipios.ObtenerMunicipios();
            foreach (var municipio in municipios)
            {
                ListaMunicipios.Add(new SelectListItem
                {
                    Text = municipio.Nombre,
                    Value = municipio.Id.ToString()
                });
            }
            ListaMunicipios.Insert(0, new SelectListItem { Text = "---Seleccione---", Value = "" });
            ViewBag.ListaMunicipios = ListaMunicipios;
        }

        [HttpGet]
        private void ObtenerDosis()
        {
            List<SelectListItem> ListaDosis = new List<SelectListItem>();
            RepositorioDosis repositorioDosis = new RepositorioDosis();
            var Dosis = repositorioDosis.ObtenerDosis();
            foreach (var dosis  in Dosis)
            {
                ListaDosis.Add(new SelectListItem
                {
                    Text = dosis.Descripcion,
                    Value = dosis.Id.ToString()
                });
            }
            ListaDosis.Insert(0, new SelectListItem { Text = "---Seleccione---", Value = "" });
            ViewBag.ListaDosis = ListaDosis;
        }

        private void CargarDropDownList()
        {
            ObtenerMunicipios();
            ObtenerVacunas();
            ObtenerDosis();
        }
    }
}