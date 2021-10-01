using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Compuesto : Producto
    {
        public List<Ingrediente> Ingredientes { get; protected set; }
        public override double Costo => Ingredientes.Sum(oRow =>  oRow.Producto.Costo * oRow.Cantidad);
        public override double Precio => Ingredientes.Sum(oRow =>  oRow.Producto.Precio * oRow.Cantidad);

        public Compuesto(ProductoDTO pDatos) : base(pDatos)
        {
            Ingredientes = pDatos.Ingredientes ?? new List<Ingrediente>();
        }

        public override string Entrada(double pCantidad)
        {
            return $"Error: los compuestos no tienen entradas solo salidas.";
        }

        public override string Salida(double pCantidad)
        {
            //base.Salida(pCantidad, Stock);
            
            //if (Stock == null)
            //    return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
            //else
            //{
                var Rollback = new List<Movimiento>();
                foreach (Ingrediente oRow in Ingredientes)
                {
                    var oCantidadoProducto = pCantidad * oRow.Cantidad;
                    //var oProducto = Stock.FirstOrDefault(oProd => oProd.Id == oRow.ProductoId);
                    var Respuesta = oRow.Producto.Salida(oCantidadoProducto);
                    if (Respuesta.Contains("Error:"))
                        return Respuesta;
                    //if (oProducto == null)
                    //    return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
                    // if (oProducto.Cantidad < oCantidadoProducto)
                    //    return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
                    // Peligro de errores anteriores que disminuyeron el stock y dañen la integridad
                    // Se maneja con un rollback desde DB
                    //var Respuesta = oProducto.Salida(oCantidadoProducto, Stock);
                    //if (Respuesta.Contains("Error:"))
                    //    return Respuesta;
                }

                //this.Cantidad = pCantidad;
                return $"Hecho: se ha generado {pCantidad} de {Nombre} (Costo: {Costo * pCantidad}, Precio: {Precio * pCantidad}, Utilidad: {Utilidad * pCantidad}).";
            //}
        }

    }
}
