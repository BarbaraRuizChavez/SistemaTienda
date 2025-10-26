using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SistemaTienda.Backend
{
    public class clsProductosDAL
    {
        private readonly clsConexion conexion = new clsConexion();

        // Buscar producto por clave
        public clsProducto BuscarPorClave(string clave)
        {
            using (var cn = conexion.ObtenerConexion())
            {
                string sql = "SELECT * FROM productos WHERE Clave = @Clave";
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Clave", clave);
                MySqlDataReader dr = cmd.ExecuteReader();

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
                return null;
            }
        }

        // Descontinuar producto
        public bool DescontinuarProducto(string clave)
        {
            using (var cn = conexion.ObtenerConexion())
            {
                MySqlTransaction trans = cn.BeginTransaction();
                try
                {
                    string sql = "UPDATE productos SET Disponibilidad = FALSE WHERE Clave = @Clave";
                    MySqlCommand cmd = new MySqlCommand(sql, cn, trans);
                    cmd.Parameters.AddWithValue("@Clave", clave);
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
                finally
                {
                    /// FINALIZAMOS LA CONEXION CERRAMOS TODO
                    trans.Dispose();
                    cn.Close();
                    cn.Dispose();
                }
            }
        }
    }
}

