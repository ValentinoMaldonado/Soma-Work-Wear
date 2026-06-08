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
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            obj.Nombre = obj.Nombre == null ? string.Empty : obj.Nombre.Trim();
            obj.Apellido = obj.Apellido == null ? string.Empty : obj.Apellido.Trim();
            obj.Correo = obj.Correo == null ? string.Empty : obj.Correo.Trim();
            obj.Clave = obj.Clave == null ? string.Empty : obj.Clave.Trim();

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El apellido del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Clave) || string.IsNullOrWhiteSpace(obj.Clave))
            {
                Mensaje = "La clave del usuario no puede ser vacía";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                bool correoExistente = objCapaDato.Listar().Any(u =>
                    string.Equals(u.Correo, obj.Correo, StringComparison.OrdinalIgnoreCase));

                if (correoExistente)
                {
                    Mensaje = "Ya existe un usuario registrado con ese correo";
                    return 0;
                }

                obj.Clave = CN_Recursos.ConvertirSha256(obj.Clave);
                obj.Reestablecer = false;

                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El apellido del usuario no puede ser vacío";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
