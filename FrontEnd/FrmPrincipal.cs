using SistemaTienda.Backend;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaTienda.Frontend
{
    public partial class FrmPrincipal : Form
    {
		private clsProductosDAL productosDAL;
		private bool procesamientoAutomatico = true;
		public FrmPrincipal()
        {
            InitializeComponent();
            InicializarDataGridView();
			productosDAL = new clsProductosDAL();
		}


		private void InicializarDataGridView()
		{
			dgvProductos.Columns.Clear();

			dgvProductos.Columns.Add("Clave", "Clave");
			dgvProductos.Columns.Add("Nombre", "Nombre");
			dgvProductos.Columns.Add("Precio", "Precio");
			dgvProductos.Columns.Add("Stock", "Stock");
			dgvProductos.Columns.Add("Descripcion", "Descripción");
			dgvProductos.Columns.Add("Disponibilidad", "Disponible");

			dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvProductos.ReadOnly = true;
			dgvProductos.AllowUserToAddRows = false;
			dgvProductos.RowHeadersVisible = false;

			dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "C2";
			dgvProductos.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvProductos.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		}

		private void BuscarProductoPorCodigo(string codigo)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(codigo))
				{
					MostrarMensaje("Por favor ingresa un código de barras.", TipoMensaje.Informacion);
					return;
				}

				if (productosDAL.ProductoYaEnGrid(codigo, dgvProductos))
				{
					MostrarMensaje("Este producto ya fue agregado al listado.", TipoMensaje.Advertencia);
					return;
				}

				var producto = productosDAL.BuscarPorClave(codigo);

				if (producto != null)
				{
					if (!producto.Disponibilidad)
					{
						MostrarMensaje("Este producto está descontinuado y no se puede agregar.", TipoMensaje.Advertencia);
						return;
					}

					AgregarProductoAlGrid(producto);

					txtClave.Clear();
					txtClave.Focus();

					MostrarMensaje("Producto agregado correctamente.", TipoMensaje.Exito);
				}
				else
				{
					MostrarMensaje("Producto no encontrado en la base de datos.", TipoMensaje.Error);
				}
			}
			catch (Exception ex)
			{
				MostrarMensaje($"Error: {ex.Message}", TipoMensaje.Error);
			}
		}

		private void AgregarProductoAlGrid(clsProducto producto)
		{
			int rowIndex = dgvProductos.Rows.Add(
				producto.Clave,
				producto.Nombre,
				producto.Precio,
				producto.Stock,
				producto.Descripcion,
				producto.Disponibilidad ? "Sí" : "No"
			);

			if (rowIndex >= 0)
			{
				dgvProductos.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
				dgvProductos.FirstDisplayedScrollingRowIndex = rowIndex;
			}
		}

		private void btnBuscar_Click(object sender, EventArgs e)
        {
			ProcesarCodigoBarras();
		}

        private void btnDescontinuar_Click(object sender, EventArgs e)
        {
			try
			{
				if (dgvProductos.Rows.Count == 0)
				{
					MostrarMensaje("No hay productos en el listado para descontinuar.", TipoMensaje.Informacion);
					return;
				}

				List<string> clavesDescontinuar = new List<string>();
				foreach (DataGridViewRow fila in dgvProductos.Rows)
				{
					string disponibilidad = fila.Cells["Disponibilidad"].Value?.ToString();
					if (disponibilidad == "Sí")
					{
						string clave = fila.Cells["Clave"].Value?.ToString();
						if (!string.IsNullOrEmpty(clave))
						{
							clavesDescontinuar.Add(clave);
						}
					}
				}

				if (clavesDescontinuar.Count == 0)
				{
					MostrarMensaje("No hay productos disponibles para descontinuar en el listado.", TipoMensaje.Advertencia);
					return;
				}

				string mensajeConfirmacion = $"¿Está seguro de descontinuar {clavesDescontinuar.Count} productos?\n\n" +
										   "Esta acción no se puede deshacer y afectará a todos los productos seleccionados.";

				DialogResult resultado = MessageBox.Show(
					mensajeConfirmacion,
					"Confirmar Descontinuación Masiva",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning);

				if (resultado == DialogResult.Yes)
				{
					if (productosDAL.DescontinuarProductos(clavesDescontinuar))
					{
						foreach (DataGridViewRow fila in dgvProductos.Rows)
						{
							if (fila.Cells["Disponibilidad"].Value?.ToString() == "Sí")
							{
								fila.Cells["Disponibilidad"].Value = "No";
								fila.DefaultCellStyle.ForeColor = Color.Gray;
								fila.DefaultCellStyle.BackColor = Color.LightGray;
							}
						}

						MostrarMensaje($"{clavesDescontinuar.Count} productos descontinuados correctamente.", TipoMensaje.Exito);
					}
				}
			}
			catch (Exception ex)
			{
				MostrarMensaje(ex.Message, TipoMensaje.Error);
			}
		}

		private void MostrarMensaje(string mensaje, TipoMensaje tipo)
		{
			MessageBoxIcon icono = MessageBoxIcon.Information;
			string titulo = "Información";

			switch (tipo)
			{
				case TipoMensaje.Exito:
					icono = MessageBoxIcon.Information;
					titulo = "Éxito";
					break;
				case TipoMensaje.Error:
					icono = MessageBoxIcon.Error;
					titulo = "Error";
					break;
				case TipoMensaje.Advertencia:
					icono = MessageBoxIcon.Warning;
					titulo = "Advertencia";
					break;
				case TipoMensaje.Informacion:
					icono = MessageBoxIcon.Information;
					titulo = "Información";
					break;
			}

			MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
		}

		private enum TipoMensaje
		{
			Exito,
			Error,
			Advertencia,
			Informacion
		}

		private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (procesamientoAutomatico && e.KeyChar == (char)Keys.Enter)
			{
				e.Handled = true;
				ProcesarCodigoBarras();
			}
		}

		private void ProcesarCodigoBarras()
		{
			string codigo = txtClave.Text.Trim();

			if (string.IsNullOrWhiteSpace(codigo))
				return;

			try
			{
				var producto = productosDAL.ProcesarCodigoBarras(codigo, dgvProductos);

				if (producto != null)
				{
					if (!producto.Disponibilidad)
					{
						MostrarMensaje("Este producto está descontinuado y no se puede agregar.", TipoMensaje.Advertencia);
						return;
					}

					AgregarProductoAlGrid(producto);
					txtClave.Clear();
					txtClave.Focus();

					MostrarMensajeRapido($"✓ {producto.Nombre} agregado");
				}
				else
				{
					if (productosDAL.ProductoYaEnGrid(codigo, dgvProductos))
					{
						MostrarMensajeRapido("⚠ Producto ya en lista");
					}
					else
					{
						MostrarMensaje("Producto no encontrado en la base de datos.", TipoMensaje.Error);
					}
				}
			}
			catch (Exception ex)
			{
				MostrarMensaje($"Error al procesar código: {ex.Message}", TipoMensaje.Error);
			}
		}

		private void MostrarMensajeRapido(string mensaje)
		{
			MessageBox.Show(mensaje, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
    


