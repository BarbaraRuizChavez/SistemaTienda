using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Backend
{
    public class clsConexion
    {
        private readonly string cadenaConexion = "server=localhost; database=sistema_tienda; user=root; pwd=b2r4c6h8";

        public MySqlConnection ObtenerConexion()
        {
            MySqlConnection cn = new MySqlConnection(cadenaConexion);
            cn.Open();
            return cn;
        }
    }
}
