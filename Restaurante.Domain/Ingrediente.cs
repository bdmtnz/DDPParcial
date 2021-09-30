using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Ingrediente
    {
        public int ProductoId { get; private set; }
        public double Cantidad { get; private set; }
        public double Costo { get; private set; }
        public double Precio { get; private set; }
        public double Utilidad => Precio - Costo;

        public Ingrediente(Producto pProducto, double pCantidad)
        {
            ProductoId = pProducto.Id;
            Cantidad = pCantidad;
            Costo = pProducto.Costo;
            Precio = pProducto.Precio;
        }

    }
}
