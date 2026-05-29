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

        // Registrar devuelve id generado
        public int Registrar(Usuario objeto, out string Mensaje)
        {
            return objCapaDato.Registrar(objeto, out Mensaje);
        }

        public bool Editar(Usuario objeto, out string Mensaje)
        {
            return objCapaDato.Editar(objeto, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
