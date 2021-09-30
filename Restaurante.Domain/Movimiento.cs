using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Movimiento
    {
        public Producto Producto { get; private set; }
        public double Cantidad { get; private set; }

        public Movimiento(Producto pProducto, double pCantidad)
        {
            Producto = pProducto;
            Cantidad = pCantidad;
        }

        public Movimiento(int pProductoId, double pCantidad)
        {
            Producto = new Producto(new ProductoDTO() { Id = pProductoId });
            Cantidad = pCantidad;
        }
    }
}
