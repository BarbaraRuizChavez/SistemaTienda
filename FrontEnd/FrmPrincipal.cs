using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaTienda.Backend;

namespace SistemaTienda.Frontend
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            InicializarDataGridView();
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
        }

        private void BuscarProductoPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor ingresa un código de barras.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                if (fila.Cells["Clave"].Value != null && fila.Cells["Clave"].Value.ToString() == codigo)
                {
                    MessageBox.Show("Este producto ya fue agregado al listado.", "Producto duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            clsProductosDAL dal = new clsProductosDAL();
            var producto = dal.BuscarPorClave(codigo);

            if (producto != null)
            {
                dgvProductos.Rows.Add(
                    producto.Clave,
                    producto.Nombre,
                    producto.Precio,
                    producto.Stock,
                    producto.Descripcion,
                    producto.Disponibilidad ? "Sí" : "No"
                );

                txtClave.Clear();
                txtClave.Focus();
            }
            else
            {
                MessageBox.Show("Producto no encontrado en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProductoPorCodigo(txtClave.Text.Trim());
        }

        private void btnDescontinuar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow != null)
            {
                string clave = dgvProductos.CurrentRow.Cells["Clave"].Value.ToString();
                string disponibilidad = dgvProductos.CurrentRow.Cells["Disponibilidad"].Value.ToString();

                if (disponibilidad == "No")
                {
                    MessageBox.Show("Este producto ya está descontinuado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                clsProductosDAL dal = new clsProductosDAL();
                if (dal.DescontinuarProducto(clave))
                {
                    MessageBox.Show("Producto descontinuado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvProductos.CurrentRow.Cells["Disponibilidad"].Value = "No";
                    dgvProductos.CurrentRow.DefaultCellStyle.ForeColor = Color.Gray;
                }
                else
                {
                    MessageBox.Show("Error al descontinuar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}



