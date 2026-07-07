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
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();

            // CN_Usuarios is the class present in CapaNegocio (note plural and exact casing)
            oLista = new CN_Usuarios().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario usuario)
        {
            if (usuario == null)
                return Json(new { success = false, message = "Datos inválidos" });

            try
            {
                string mensaje = string.Empty;
                if (usuario.IdUsuario == 0)
                {
                    int id = new CN_Usuarios().Registrar(usuario, out mensaje);
                    if (id > 0)
                    {
                        usuario.IdUsuario = id;
                        return Json(new { success = true, message = string.IsNullOrWhiteSpace(mensaje) ? "Usuario registrado" : mensaje, id = id, data = usuario });
                    }
                    return Json(new { success = false, message = mensaje ?? "No se pudo registrar" });
                }
                else
                {
                    bool resultado = new CN_Usuarios().Editar(usuario, out mensaje);
                    if (resultado)
                    {
                        return Json(new { success = true, message = "Usuario actualizado", data = usuario });
                    }
                    return Json(new { success = false, message = mensaje ?? "No se pudo actualizar" });
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            try
            {
                string mensaje = string.Empty;
                bool ok = new CN_Usuarios().Eliminar(id, out mensaje);
                if (ok)
                    return Json(new { success = true, message = "Usuario eliminado" });
                return Json(new { success = false, message = mensaje ?? "No se pudo eliminar" });
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return Json(new { success = false, message = ex.Message });
            }
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
