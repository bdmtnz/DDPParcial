using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Compuesto : Producto
    {
        public new double Costo => Ingredientes.Sum(oRow => oRow.Costo * oRow.Cantidad);

        public Compuesto(ProductoDTO pDatos) : base(pDatos)
        {

        }

        public new string Entrada(double pCantidad)
        {
            return $"Error: los compuestos no tienen entradas solo salidas.";
        }
    }
}
