using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Compuesto : Producto
    {
        public override double Costo => Ingredientes.Sum(oRow => oRow.Costo * oRow.Cantidad);

        public Compuesto(ProductoDTO pDatos) : base(pDatos)
        {

        }

        public override string Entrada(double pCantidad)
        {
            return $"Error: los compuestos no tienen entradas solo salidas.";
        }
    }
}
