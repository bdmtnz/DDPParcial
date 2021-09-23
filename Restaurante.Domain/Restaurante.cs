using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventario.Domain
{
    public class Restaurante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Producto> Productos { get; set; }

        public Restaurante(int pId, string pNombre, List<Producto> pProductos)
        {
            Id = pId;
            Nombre = pNombre;
            Productos = pProductos;
        }

        public string Entrada(int pProducto, double pCantidad)
        {
            if(Productos == null)
                return "Error: No hay productos registrados.";

            if (Productos.Count <= 0)
                return "Error: No hay productos registrados.";

            var oProducto = Productos.FirstOrDefault(oRow => oRow.Id == pProducto);
            if(oProducto == null)
                return $"Error: No hay productos registrados que coincidan con '{pProducto}'.";

            return oProducto.Entrada(pCantidad);
        }
    }
}
