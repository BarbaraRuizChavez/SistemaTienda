using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaTienda.Backend;

namespace SistemaTienda.Frontend
{
    public partial class FrmPrincipal : Form
    {
		private clsProductosDAL productosDAL;
		public FrmPrincipal()
        {
            InitializeComponent();
            InicializarDataGridView();
			productosDAL = new clsProductosDAL();
		}


		private void InicializarDataGridView()
		{
			dgvProductos.Columns.Clear();

			// Configurar columnas
			dgvProductos.Columns.Add("Clave", "Clave");
			dgvProductos.Columns.Add("Nombre", "Nombre");
			dgvProductos.Columns.Add("Precio", "Precio");
			dgvProductos.Columns.Add("Stock", "Stock");
			dgvProductos.Columns.Add("Descripcion", "Descripción");
			dgvProductos.Columns.Add("Disponibilidad", "Disponible");

			// Mejorar apariencia
			dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvProductos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvProductos.ReadOnly = true;
			dgvProductos.AllowUserToAddRows = false;
			dgvProductos.RowHeadersVisible = false;

			// Formato de columnas
			dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "C2";
			dgvProductos.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvProductos.Columns["Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		}

		private void BuscarProductoPorCodigo(string codigo)
		{
			try
			{
				// Validación básica
				if (string.IsNullOrWhiteSpace(codigo))
				{
					MostrarMensaje("Por favor ingresa un código de barras.", TipoMensaje.Informacion);
					return;
				}

				// Verificar si ya está en el grid
				if (productosDAL.ProductoYaEnGrid(codigo, dgvProductos))
				{
					MostrarMensaje("Este producto ya fue agregado al listado.", TipoMensaje.Advertencia);
					return;
				}

				// Buscar en la base de datos
				var producto = productosDAL.BuscarPorClave(codigo);

				if (producto != null)
				{
					// Validar disponibilidad
					if (!producto.Disponibilidad)
					{
						MostrarMensaje("Este producto está descontinuado y no se puede agregar.", TipoMensaje.Advertencia);
						return;
					}

					// Agregar al grid
					AgregarProductoAlGrid(producto);

					// Limpiar y enfocar
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

			// Resaltar la nueva fila agregada
			if (rowIndex >= 0)
			{
				dgvProductos.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
				dgvProductos.FirstDisplayedScrollingRowIndex = rowIndex;
			}
		}

		private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProductoPorCodigo(txtClave.Text.Trim());
        }

        private void btnDescontinuar_Click(object sender, EventArgs e)
        {
			try
			{
				if (dgvProductos.CurrentRow == null)
				{
					MostrarMensaje("Por favor seleccione un producto del listado.", TipoMensaje.Informacion);
					return;
				}

				string clave = dgvProductos.CurrentRow.Cells["Clave"].Value?.ToString();
				string disponibilidad = dgvProductos.CurrentRow.Cells["Disponibilidad"].Value?.ToString();

				if (string.IsNullOrEmpty(clave))
				{
					MostrarMensaje("No se puede identificar el producto seleccionado.", TipoMensaje.Error);
					return;
				}

				// Validar si ya está descontinuado en el grid
				if (disponibilidad == "No")
				{
					MostrarMensaje("Este producto ya está descontinuado.", TipoMensaje.Advertencia);
					return;
				}

				// Confirmar acción
				DialogResult resultado = MessageBox.Show(
					$"¿Está seguro de descontinuar el producto '{dgvProductos.CurrentRow.Cells["Nombre"].Value}'?",
					"Confirmar Descontinuación",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (resultado == DialogResult.Yes)
				{
					// Ejecutar descontinuación con transacción
					if (productosDAL.DescontinuarProducto(clave))
					{
						// Actualizar grid
						dgvProductos.CurrentRow.Cells["Disponibilidad"].Value = "No";
						dgvProductos.CurrentRow.DefaultCellStyle.ForeColor = Color.Gray;
						dgvProductos.CurrentRow.DefaultCellStyle.BackColor = Color.LightGray;

						MostrarMensaje("Producto descontinuado correctamente.", TipoMensaje.Exito);
					}
				}
			}
			catch (Exception ex)
			{
				MostrarMensaje(ex.Message, TipoMensaje.Error);
			}
		}

		// Método auxiliar para mostrar mensajes consistentes
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

		// Enumeración para tipos de mensaje
		private enum TipoMensaje
		{
			Exito,
			Error,
			Advertencia,
			Informacion
		}
	}
}
    


