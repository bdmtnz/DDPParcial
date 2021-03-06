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
        public double Costo => Productos.Sum(x => x.Costo);
        public double Precio => Productos.Sum(x => x.Precio);
        public List<Producto> Productos { get; private set; }

        public Venta(int pId, List<Producto> pVentas)
        {
            Id = pId;
            Productos = pVentas;
        }

    }
}
