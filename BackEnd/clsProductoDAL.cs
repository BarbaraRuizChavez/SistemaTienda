using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTienda.Backend
{
    public class clsProductosDAL
    {
        private readonly clsConexion conexion = new clsConexion();

		// Buscar producto por clave con validación de disponibilidad
		public clsProducto BuscarPorClave(string clave)
		{
			if (string.IsNullOrWhiteSpace(clave))
				return null;

			using (var cn = conexion.ObtenerConexion())
			{
				string sql = "SELECT * FROM productos WHERE Clave = @Clave";
				MySqlCommand cmd = new MySqlCommand(sql, cn);
				cmd.Parameters.AddWithValue("@Clave", clave.Trim());

				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						return new clsProducto
						{
							ProductoID = Convert.ToInt32(dr["ProductoID"]),
							Clave = dr["Clave"].ToString(),
							Nombre = dr["Nombre"].ToString(),
							Precio = Convert.ToDecimal(dr["Precio"]),
							Stock = Convert.ToInt32(dr["Stock"]),
							Descripcion = dr["Descripcion"].ToString(),
							Imagen = dr["Imagen"] == DBNull.Value ? null : (byte[])dr["Imagen"],
							Disponibilidad = Convert.ToBoolean(dr["Disponibilidad"])
						};
					}
				}
				return null;
			}
		}

		// Verificar si producto ya existe en grid
		public bool ProductoYaEnGrid(string clave, DataGridView grid)
		{
			foreach (DataGridViewRow fila in grid.Rows)
			{
				if (fila.Cells["Clave"].Value?.ToString() == clave)
					return true;
			}
			return false;
		}

		// Descontinuar producto
		public bool DescontinuarProducto(string clave)
		{
			if (string.IsNullOrWhiteSpace(clave))
				return false;

			MySqlConnection cn = null;
			MySqlTransaction trans = null;

			try
			{
				cn = conexion.ObtenerConexion();
				trans = cn.BeginTransaction();

				// Verificar si el producto existe y está disponible
				string sqlVerificar = "SELECT Disponibilidad FROM productos WHERE Clave = @Clave";
				MySqlCommand cmdVerificar = new MySqlCommand(sqlVerificar, cn, trans);
				cmdVerificar.Parameters.AddWithValue("@Clave", clave);

				var resultado = cmdVerificar.ExecuteScalar();

				if (resultado == null)
				{
					throw new Exception("Producto no encontrado");
				}

				bool estaDisponible = Convert.ToBoolean(resultado);
				if (!estaDisponible)
				{
					throw new Exception("El producto ya está descontinuado");
				}

				// Actualizar disponibilidad
				string sqlActualizar = "UPDATE productos SET Disponibilidad = FALSE WHERE Clave = @Clave";
				MySqlCommand cmdActualizar = new MySqlCommand(sqlActualizar, cn, trans);
				cmdActualizar.Parameters.AddWithValue("@Clave", clave);

				int filasAfectadas = cmdActualizar.ExecuteNonQuery();

				if (filasAfectadas == 0)
				{
					throw new Exception("No se pudo actualizar el producto");
				}

				trans.Commit();
				return true;
			}
			catch (Exception ex)
			{
				trans?.Rollback();
				throw new Exception($"Error al descontinuar producto: {ex.Message}");
			}
			finally
			{
				trans?.Dispose();
				cn?.Close();
				cn?.Dispose();
			}
		}
	}
}

