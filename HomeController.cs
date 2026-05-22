CapaPresentacionAdmin\Controllers\HomeController.cs
using System.Collections.Generic;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;  

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

            // match the actual method name in CapaNegocio (listar)
            oLista = new CN_Usuarios().listar();

            return Json(oLista, JsonRequestBehavior.AllowGet);
        }
    }
}