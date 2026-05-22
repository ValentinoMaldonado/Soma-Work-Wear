using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace CapaDatos
{
    public class Conexion
    {
        // Use ConnectionString and null-safe access to avoid exceptions when key is missing
        public static string cn = ConfigurationManager.ConnectionStrings["cadena"]?.ConnectionString ?? string.Empty;

    }
}
