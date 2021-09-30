using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Producto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public double Cantidad { get; private set; }
        public virtual double Costo { get; private set; }
        public double Precio { get; private set; }
        public double Utilidad => Precio - Costo;
        public List<Ingrediente> Ingredientes { get; protected set; }

        public Producto(ProductoDTO pDatos)
        {
            Id = pDatos.Id;
            Nombre = pDatos.Nombre;
            Cantidad = pDatos.Cantidad;
            Costo = pDatos.Costo;
            Precio = pDatos.Precio;
            Ingredientes = pDatos.Ingredientes;
        }

        public virtual string Entrada(double pCantidad)
        {
            Cantidad += pCantidad;
            return $"Hecho: su nueva cantidad es {Cantidad}.";
        }

        public virtual string Salida(double pCantidad, ReadOnlyCollection<Producto> Stock = null)
        {
            if(Ingredientes.Count <= 0)
            {
                if(ValidarStock(pCantidad))
                    return $"Error: no cuenta con las unidades suficientes de {Nombre}.";

                Cantidad -= pCantidad;
                return $"Hecho: su nueva cantidad es {Cantidad}.";
            }

            return $"Error: este producto es compuesto pero se está tratando como uno simple.";
            //else if(Stock == null)
            //    return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
            //else
            //{
            //    foreach (Ingrediente oRow in Ingredientes)
            //    {
            //        var oCantidadoProducto = pCantidad * oRow.Cantidad;
            //        var oProducto = Stock.FirstOrDefault(oProd => oProd.Id == oRow.ProductoId);
            //        if (oProducto == null)
            //            return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
            //        if (oProducto.Cantidad < oCantidadoProducto)
            //            return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
            //    }
            //    //Decreción
            //    foreach (Ingrediente oRow in Ingredientes)
            //    {
            //        var oCantidadoProducto = pCantidad * oRow.Cantidad;
            //        var oProducto = Stock.FirstOrDefault(oProd => oProd.Id == oRow.ProductoId);
            //        oProducto.Salida(oCantidadoProducto);
            //    }
            //    return $"Hecho: se ha generado {pCantidad} de {Nombre}.";
            //}
        }

        private bool ValidarStock(double pCantidad)
        {
            return Cantidad < pCantidad;
        }
    }

    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Cantidad { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public List<Ingrediente> Ingredientes { get; set; }
    }

}
