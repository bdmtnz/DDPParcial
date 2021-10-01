using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Ingrediente
    {
        public readonly Producto Producto;
        public double Cantidad { get; set; }

        public Ingrediente(Producto pProducto, double pCantidad)
        {
            Producto = pProducto;
            Cantidad = pCantidad;
        }

    }
}
