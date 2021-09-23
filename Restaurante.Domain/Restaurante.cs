using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Inventario.Domain
{
    public class Restaurante
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        private List<Producto> Productos { get; set; }
        public ReadOnlyCollection<Producto> oProductos => Productos.AsReadOnly();
        public List<Venta> Ventas { get; private set; }

        public Restaurante(int pId, string pNombre, List<Producto> pProductos)
        {
            Id = pId;
            Nombre = pNombre;
            Productos = pProductos;
            Ventas = new List<Venta>();
        }

        public string Entrada(int pProducto, double pCantidad)
        {
            if(Productos == null)
                return "Error: No hay productos registrados.";

            if (Productos.Count <= 0)
                return "Error: No hay productos registrados.";

            if (pCantidad <= 0)
                return "Error: La cantidad debe ser mayor a cero.";

            var oProducto = Productos.FirstOrDefault(oRow => oRow.Id == pProducto);
            if(oProducto == null)
                return $"Error: No hay productos registrados que coincidan con '{pProducto}'.";

            return oProducto.Entrada(pCantidad);
        }

        public string Salida(int pProducto, double pCantidad)
        {
            if (pCantidad <= 0)
                return "Error: La cantidad debe ser mayor a cero.";

            var oProducto = Productos.FirstOrDefault(oRow => oRow.Id == pProducto);
            if (oProducto == null)
                return $"Error: No hay productos registrados que coincidan con '{pProducto}'.";

            var Resultado = oProducto.Salida(pCantidad, oProductos);

            if (!Resultado.Contains("Error:"))
                Ventas.Add(new Venta(0, oProducto.Costo, oProducto.Precio));

            return Resultado;
        }
    }
}
