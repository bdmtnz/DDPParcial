using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Ingrediente
    {
        public int ProductoId { get; set; }
        public double Cantidad { get; set; }
        public double Costo { get; set; }
    }
}
