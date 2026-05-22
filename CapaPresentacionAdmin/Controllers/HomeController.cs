using System.Collections.Generic;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using System.Configuration;
using System.Data.SqlClient;


namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        public JsonResult ListarUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();

            // CN_Usuarios is the class present in CapaNegocio (note plural and exact casing)
            oLista = new CN_Usuarios().Listar();

            return Json(oLista, JsonRequestBehavior.AllowGet);
        }

        // Diagnostic endpoint to verify DB connection from the running web app
        public JsonResult ProbarConexion()
        {
            try
            {
                var connStr = ConfigurationManager.ConnectionStrings["cadena"]?.ConnectionString;
                if (string.IsNullOrWhiteSpace(connStr))
                    return Json(new { ok = false, message = "Connection string 'cadena' no encontrada." }, JsonRequestBehavior.AllowGet);

                // quick connectivity test
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT 1", conn))
                    {
                        var v = cmd.ExecuteScalar();
                    }
                }

                // fetch usuarios using business layer
                List<Usuario> usuarios = new CN_Usuarios().Listar();

                return Json(new { ok = true, message = "Conexión OK", count = usuarios?.Count ?? 0, data = usuarios }, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return Json(new { ok = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
