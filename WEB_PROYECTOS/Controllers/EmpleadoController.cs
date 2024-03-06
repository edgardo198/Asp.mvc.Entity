using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ENTIDAD;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_PROYECTOS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EmpleadoController : Controller
    {
        // GET: empleado
        public ActionResult Index()
        {
            var empleados = EmpleadoCN.ListarEmpleados();
            return View(empleados);
        }
        [HttpGet]
        public ActionResult Crear()
        {     
            return View();            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Empleado empleado)
        {
            try
            {
                

                System.Threading.Thread.Sleep(1000);
                EmpleadoCN.Agregar(empleado);
                return Json(new { ok = true, toRedirect = "Index" }, JsonRequestBehavior.AllowGet); 
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("", "Ocurrió un error al agregar un Proyecto");
                //return View();
            }
           
        }
        public ActionResult Detalles(int id)
        {
            var proyecto = EmpleadoCN.ObtenerEmpleado(id);
            return View(proyecto);
        }
        public ActionResult Editar(int id)
        {
            var proyecto = EmpleadoCN.ObtenerEmpleado(id);
            return View(proyecto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Empleado empleado)
        {
            try
            {
                EmpleadoCN.Editar(empleado);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        public ActionResult Eliminar(int identificador)
        {
            try
            {
                EmpleadoCN.Eliminar(identificador);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarEmpleados()
        {
            try
            {
                var lista = EmpleadoCN.ListarEmpleados();
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RptEmpleado()
        {
            return View();
        }

        public ActionResult DescargarReporteEmpleado(int id )
        {
            try
            {
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reportes/EmpleadoReporte1.rpt");
                rptH.Load();

                rptH.SetParameterValue("DptoId", id);
                //rptH.SetParameterValue("ParamAlgo", algo);mas de un parametro 

                // Report connection
                var connInfo = CrystalReportsCnn.GetConnectionInfo();
                TableLogOnInfo logonInfo = new TableLogOnInfo();
                Tables tables;
                tables = rptH.Database.Tables;
                foreach (Table table in tables)
                {
                    logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo = connInfo;
                    table.ApplyLogOnInfo(logonInfo);
                }

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                //En PDF
                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");

                //En Excel
                //Stream stream = rptH.ExportToStream(ExportFormatType.Excel);
                //stream.Seek(0, SeekOrigin.Begin);
                //return File(stream, "application/vnd.ms-excel", "empleadoRpt.xls");
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}