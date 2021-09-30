using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Venta
    {
        public int Id { get; private set; }
        public double Costo { get; private set; }
        public double Precio { get; private set; }

        public Venta(int pId, double pCosto, double pPrecio)
        {
            Id = pId;
            Costo = pCosto;
            Precio = pPrecio;
        }

    }
}
