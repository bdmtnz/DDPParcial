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

        public string Entrada(List<Entrada> pEntradas)
        {
            if(Productos == null)
                return "Error: No hay productos registrados.";

            if (Productos.Count <= 0)
                return "Error: No hay productos registrados.";

            var Respuestas = "";
            for (int i = 0; i < pEntradas.Count; i++)
            {

                if (pEntradas[i].Cantidad <= 0)
                    return "Error: La cantidad debe ser mayor a cero.";

                var oProducto = Productos.FirstOrDefault(oRow => oRow.Id == pEntradas[i].ProductoId);

                if (oProducto == null)
                    return $"Error: No hay productos registrados que coincidan con '{pEntradas[i].ProductoId}'.";

                Respuestas += oProducto.Entrada(pEntradas[i].Cantidad) + ";";
            }

            Respuestas = Respuestas.Trim(new char[] { ';' });
            return Respuestas;
        }

        public string Salida(List<Salida> pSalidas)
        {
            var Resultado = "";
            List<Producto> Vendidos = new List<Producto>();

            for (int i = 0; i < pSalidas.Count; i++)
            {
                if (pSalidas[i].Cantidad <= 0)
                    return "Error: La cantidad debe ser mayor a cero.";

                var oProducto = Productos.FirstOrDefault(oRow => oRow.Id == pSalidas[i].ProductoId);

                if (oProducto == null)
                    return $"Error: No hay productos registrados que coincidan con '{pSalidas[i].ProductoId}'.";

                Resultado += oProducto.Salida(pSalidas[i].Cantidad, oProductos) + ";";
                Vendidos.Add(oProducto);
            }

            Resultado = Resultado.Trim(new char[] { ';' });
            Vender(1, Vendidos);

            return Resultado;
        }

        public void Vender(int pId, List<Producto> pVendidos)
        {
            Ventas.Add
            (
                new Venta(Ventas.Count, pVendidos)
            );
        }
    }
}
