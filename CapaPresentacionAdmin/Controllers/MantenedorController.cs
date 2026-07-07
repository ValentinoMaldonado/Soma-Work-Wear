using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> oLista = new CN_Marca().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca marca)
        {
            if (marca == null)
                return Json(new { success = false, message = "Datos invalidos" });

            try
            {
                string mensaje = string.Empty;

                if (marca.IdMarca == 0)
                {
                    int id = new CN_Marca().Registrar(marca, out mensaje);
                    if (id > 0)
                    {
                        marca.IdMarca = id;
                        return Json(new { success = true, message = "Marca registrada", id = id, data = marca });
                    }

                    return Json(new { success = false, message = mensaje ?? "No se pudo registrar" });
                }

                bool resultado = new CN_Marca().Editar(marca, out mensaje);
                if (resultado)
                {
                    return Json(new { success = true, message = "Marca actualizada", data = marca });
                }

                return Json(new { success = false, message = mensaje ?? "No se pudo actualizar" });
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            try
            {
                string mensaje = string.Empty;
                bool ok = new CN_Marca().Eliminar(id, out mensaje);

                if (ok)
                    return Json(new { success = true, message = "Marca eliminada" });

                return Json(new { success = false, message = mensaje ?? "No se pudo eliminar" });
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.ToString());
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult Producto()
        {
            return View();
        }
    }
}
