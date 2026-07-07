using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Marca
    {
        private CD_Marca objCapaDato = new CD_Marca();

        public List<Marca> Listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            obj.Descripcion = obj.Descripcion == null ? string.Empty : obj.Descripcion.Trim();

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la marca no puede ser vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }

            return 0;
        }

        public bool Editar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            obj.Descripcion = obj.Descripcion == null ? string.Empty : obj.Descripcion.Trim();

            if (obj.IdMarca <= 0)
            {
                Mensaje = "Debe seleccionar una marca valida";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion de la marca no puede ser vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }

            return false;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            if (id <= 0)
            {
                Mensaje = "Debe seleccionar una marca valida";
                return false;
            }

            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
