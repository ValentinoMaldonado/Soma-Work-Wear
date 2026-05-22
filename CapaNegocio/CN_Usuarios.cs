using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
namespace CapaNegocio
{
    public class CN_Usuarios
    {

        private CD_Usuarios objCapaDato = new CD_Usuarios();




        /*metodo para listar los usuarios*/
        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }


    }
}
