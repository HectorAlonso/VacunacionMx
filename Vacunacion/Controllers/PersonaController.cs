using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Vacunacion.Models;
using Vacunacion.Servicios;

namespace Vacunacion.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            Persona persona = new Persona();
            persona = (Persona)Session["Usuario"];
            return View(persona);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            Session["Usuario"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Persona persona)
        {
            RepositorioPersona repositorioPersona = new RepositorioPersona();
            if (!ModelState.IsValid )
            {
                return View(persona);
            }
            using (TransactionScope ts = new TransactionScope())
            {
                repositorioPersona.Crear(persona);
                var Folio = repositorioPersona.ObtenerFolioPorRFC(persona.RFC);
                persona.Folio = Folio;
                Session["Usuario"] = persona;
                ts.Complete();
            }
            return RedirectToAction("Index");
        }
    }
}