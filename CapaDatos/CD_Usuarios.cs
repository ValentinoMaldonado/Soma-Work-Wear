using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data; 
namespace CapaDatos
{
    public class CD_Usuarios
    {

        public List<Usuario> Listar() {
           
            List<Usuario> lista = new List<Usuario>();
                try
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                    {
                        // Use singular table name 'USUARIO' as in the database (was 'USUARIOS')
                        string query = "select IdUsuario,Nombres,Apellidos,Correo,Clave,Reestablecer,Activo from USUARIO";
                        SqlCommand cmd = new SqlCommand(query, oconexion);
                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {  
                                lista.Add(new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Nombre = dr["Nombres"].ToString(),
                                    Apellido = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                    Activo = Convert.ToBoolean(dr["Activo"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the full exception for diagnostics and rethrow so callers (like ProbarConexion) can report the error
                    System.Diagnostics.Trace.TraceError(ex.ToString());
                    throw;
                }
                return lista;





        }

    }
}
