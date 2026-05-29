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

        // Insert a new usuario and return the generated identity IdUsuario
        public int Registrar(Usuario objeto, out string Mensaje)
        {
            Mensaje = string.Empty;
            int idGenerado = 0;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "INSERT INTO USUARIO (Nombres,Apellidos,Correo,Clave,Reestablecer,Activo) OUTPUT INSERTED.IdUsuario VALUES (@Nombres,@Apellidos,@Correo,@Clave,@Reestablecer,@Activo)";
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.AddWithValue("@Nombres", objeto.Nombre ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellidos", objeto.Apellido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Correo", objeto.Correo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Clave", objeto.Clave ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Reestablecer", objeto.Reestablecer);
                        cmd.Parameters.AddWithValue("@Activo", objeto.Activo);

                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            idGenerado = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                System.Diagnostics.Trace.TraceError(ex.ToString());
            }
            return idGenerado;
        }

        // Update existing usuario
        public bool Editar(Usuario objeto, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "UPDATE USUARIO SET Nombres=@Nombres,Apellidos=@Apellidos,Correo=@Correo,Clave=@Clave,Reestablecer=@Reestablecer,Activo=@Activo WHERE IdUsuario=@IdUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", objeto.IdUsuario);
                        cmd.Parameters.AddWithValue("@Nombres", objeto.Nombre ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellidos", objeto.Apellido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Correo", objeto.Correo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Clave", objeto.Clave ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Reestablecer", objeto.Reestablecer);
                        cmd.Parameters.AddWithValue("@Activo", objeto.Activo);

                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();
                        int filas = cmd.ExecuteNonQuery();
                        respuesta = filas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                System.Diagnostics.Trace.TraceError(ex.ToString());
            }
            return respuesta;
        }

        // Delete usuario by id
        public bool Eliminar(int id, out string Mensaje)
        {
            Mensaje = string.Empty;
            bool respuesta = false;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "DELETE FROM USUARIO WHERE IdUsuario=@IdUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", id);
                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();
                        int filas = cmd.ExecuteNonQuery();
                        respuesta = filas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                System.Diagnostics.Trace.TraceError(ex.ToString());
            }
            return respuesta;
        }
    }
}
