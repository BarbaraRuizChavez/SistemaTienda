namespace SistemaTienda.Frontend
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.btnDescontinuar = new System.Windows.Forms.Button();
			this.txtClave = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dgvProductos = new System.Windows.Forms.DataGridView();
			this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Disponibilidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnBuscar = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
			this.SuspendLayout();
			// 
			// btnDescontinuar
			// 
			this.btnDescontinuar.AutoSize = true;
			this.btnDescontinuar.BackColor = System.Drawing.Color.CornflowerBlue;
			this.btnDescontinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDescontinuar.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDescontinuar.ForeColor = System.Drawing.SystemColors.Control;
			this.btnDescontinuar.Location = new System.Drawing.Point(679, 174);
			this.btnDescontinuar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnDescontinuar.Name = "btnDescontinuar";
			this.btnDescontinuar.Size = new System.Drawing.Size(219, 38);
			this.btnDescontinuar.TabIndex = 0;
			this.btnDescontinuar.Text = "Descontinuar producto";
			this.btnDescontinuar.UseVisualStyleBackColor = false;
			this.btnDescontinuar.Click += new System.EventHandler(this.btnDescontinuar_Click);
			// 
			// txtClave
			// 
			this.txtClave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.txtClave.Location = new System.Drawing.Point(679, 39);
			this.txtClave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtClave.Name = "txtClave";
			this.txtClave.Size = new System.Drawing.Size(277, 22);
			this.txtClave.TabIndex = 2;
			this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(448, 39);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(189, 27);
			this.label1.TabIndex = 3;
			this.label1.Text = "Código de barras :";
			// 
			// dgvProductos
			// 
			this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Clave,
            this.Nombre,
            this.Precio,
            this.Stock,
            this.Descripcion,
            this.Disponibilidad});
			this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dgvProductos.Location = new System.Drawing.Point(0, 369);
			this.dgvProductos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dgvProductos.Name = "dgvProductos";
			this.dgvProductos.RowHeadersWidth = 51;
			this.dgvProductos.Size = new System.Drawing.Size(1064, 185);
			this.dgvProductos.TabIndex = 4;
			// 
			// Clave
			// 
			this.Clave.HeaderText = "Clave";
			this.Clave.MinimumWidth = 6;
			this.Clave.Name = "Clave";
			// 
			// Nombre
			// 
			this.Nombre.HeaderText = "Nombre";
			this.Nombre.MinimumWidth = 6;
			this.Nombre.Name = "Nombre";
			// 
			// Precio
			// 
			this.Precio.HeaderText = "Precio";
			this.Precio.MinimumWidth = 6;
			this.Precio.Name = "Precio";
			// 
			// Stock
			// 
			this.Stock.HeaderText = "Stock";
			this.Stock.MinimumWidth = 6;
			this.Stock.Name = "Stock";
			// 
			// Descripcion
			// 
			this.Descripcion.HeaderText = "Descripcion";
			this.Descripcion.MinimumWidth = 6;
			this.Descripcion.Name = "Descripcion";
			// 
			// Disponibilidad
			// 
			this.Disponibilidad.HeaderText = "Disponibilidad";
			this.Disponibilidad.MinimumWidth = 6;
			this.Disponibilidad.Name = "Disponibilidad";
			// 
			// btnBuscar
			// 
			this.btnBuscar.BackColor = System.Drawing.Color.CornflowerBlue;
			this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBuscar.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBuscar.ForeColor = System.Drawing.SystemColors.Control;
			this.btnBuscar.Location = new System.Drawing.Point(748, 100);
			this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnBuscar.Name = "btnBuscar";
			this.btnBuscar.Size = new System.Drawing.Size(100, 33);
			this.btnBuscar.TabIndex = 5;
			this.btnBuscar.Text = "Buscar";
			this.btnBuscar.UseVisualStyleBackColor = false;
			this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Navy;
			this.label2.Location = new System.Drawing.Point(16, 11);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(241, 27);
			this.label2.TabIndex = 6;
			this.label2.Text = "Descontinuar productos";
			// 
			// FrmPrincipal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ClientSize = new System.Drawing.Size(1064, 554);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnBuscar);
			this.Controls.Add(this.dgvProductos);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtClave);
			this.Controls.Add(this.btnDescontinuar);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "FrmPrincipal";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDescontinuar;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Disponibilidad;
        private System.Windows.Forms.Label label2;
    }
}

