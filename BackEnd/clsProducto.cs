using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Backend
{
    public class clsProducto
    {
        public int ProductoID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public bool Disponibilidad { get; set; }
    }
}

