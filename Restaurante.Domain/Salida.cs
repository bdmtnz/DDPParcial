using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Salida
    {
        public int ProductoId { get; private set; }
        public double Cantidad { get; private set; }

        public Salida(int pProductoId, double pCantidad)
        {
            ProductoId = pProductoId;
            Cantidad = pCantidad;
        }
    }
}
