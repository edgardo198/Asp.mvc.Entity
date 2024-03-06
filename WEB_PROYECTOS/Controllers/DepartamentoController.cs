using ENTIDAD;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WEB_PROYECTOS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        public ActionResult Index()
        {
            var dptos = DepartamentoCN.ListaDepartamento();
            return View(dptos);
        }

        [HttpGet]       
        public ActionResult crear()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult crear(Departamento dpto)
        {
            if (dpto.NombreDepartamento == null)
            {
                ModelState.AddModelError("", "Debe ingresar un Nombre departamento");
                return View(dpto);
            }
            try
            {
                DepartamentoCN.Agregar(dpto);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrio un error al agregar");
                return View(dpto);
            }
        }
        public ActionResult GetDepartamento(int id)
        {
            var dpto = DepartamentoCN.GetDepartamento(id);
            return View(dpto);
        }

        public JsonResult GetDepartamentos()
        {
            var lista = DepartamentoCN.ListaDepartamento();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(int id) 
        {
            var dpto = DepartamentoCN.GetDepartamento(id);
            return View(dpto);
        }
        [HttpPost]
        public ActionResult Editar(Departamento dpto)
        {
            try
            {
                if (dpto.NombreDepartamento == null)
                {
                    ModelState.AddModelError("", "Debe ingresar un Nombre departamento");
                    return View(dpto); 
                }
                DepartamentoCN.Editar(dpto);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Ocurrio un error al agregar cambio");
                return View(dpto);
            }
        }

        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var dpto = DepartamentoCN.GetDepartamento(id.Value);
            return View(dpto);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                DepartamentoCN.Eliminar(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}