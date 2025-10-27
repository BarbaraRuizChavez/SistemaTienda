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
        private readonly string cadenaConexion = "server=localhost; database=sistema_tienda; user=root; pwd=root";

		public MySqlConnection ObtenerConexion()
		{
			try
			{
				MySqlConnection cn = new MySqlConnection(cadenaConexion);
				cn.Open();
				return cn;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error al conectar con la base de datos: {ex.Message}");
			}
		}
	}
}
