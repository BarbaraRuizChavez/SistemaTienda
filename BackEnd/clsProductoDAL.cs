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

		public bool ProductoYaEnGrid(string clave, DataGridView grid)
		{
			foreach (DataGridViewRow fila in grid.Rows)
			{
				if (fila.Cells["Clave"].Value?.ToString() == clave)
					return true;
			}
			return false;
		}

		public bool DescontinuarProductos(List<string> claves)
		{
			if (claves == null || claves.Count == 0)
				return false;

			MySqlConnection cn = null;
			MySqlTransaction trans = null;

			try
			{
				cn = conexion.ObtenerConexion();
				trans = cn.BeginTransaction();

				foreach (string clave in claves)
				{
					string sqlVerificar = "SELECT Disponibilidad FROM productos WHERE Clave = @Clave";
					MySqlCommand cmdVerificar = new MySqlCommand(sqlVerificar, cn, trans);
					cmdVerificar.Parameters.AddWithValue("@Clave", clave);

					var resultado = cmdVerificar.ExecuteScalar();

					if (resultado == null)
					{
						throw new Exception($"Producto con clave {clave} no encontrado");
					}

					bool estaDisponible = Convert.ToBoolean(resultado);
					if (!estaDisponible)
					{
						throw new Exception($"El producto {clave} ya está descontinuado");
					}
				}

				string sqlActualizar = "UPDATE productos SET Disponibilidad = FALSE WHERE Clave IN (" +
									  string.Join(",", claves.ConvertAll(c => $"'{c}'").ToArray()) + ")";

				MySqlCommand cmdActualizar = new MySqlCommand(sqlActualizar, cn, trans);
				int filasAfectadas = cmdActualizar.ExecuteNonQuery();

				if (filasAfectadas != claves.Count)
				{
					throw new Exception($"No se pudieron actualizar todos los productos. Esperados: {claves.Count}, Actualizados: {filasAfectadas}");
				}

				trans.Commit();
				return true;
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw new Exception($"Error al descontinuar productos: {ex.Message}");
			}
			finally
			{
				trans.Dispose();
				cn.Close();
				cn.Dispose();
			}
		}

		public clsProducto ProcesarCodigoBarras(string codigo, DataGridView grid)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(codigo))
					return null;

				if (ProductoYaEnGrid(codigo, grid))
					return null;

				var producto = BuscarPorClave(codigo);
				return producto;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}

