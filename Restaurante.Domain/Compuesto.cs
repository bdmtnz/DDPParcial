﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain
{
    public class Compuesto : Producto
    {
        public override double Cantidad { get; protected set; }
        public override double Costo => Ingredientes.Sum(oRow => Cantidad * oRow.Costo * oRow.Cantidad);
        public override double Precio => Ingredientes.Sum(oRow => Cantidad * oRow.Precio * oRow.Cantidad);

        public Compuesto(ProductoDTO pDatos) : base(pDatos)
        {

        }

        public override string Entrada(double pCantidad)
        {
            return $"Error: los compuestos no tienen entradas solo salidas.";
        }

        public override string Salida(double pCantidad, ReadOnlyCollection<Producto> Stock = null)
        {
            //base.Salida(pCantidad, Stock);
            
            if (Stock == null)
                return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
            else
            {
                foreach (Ingrediente oRow in Ingredientes)
                {
                    var oCantidadoProducto = pCantidad * oRow.Cantidad;
                    var oProducto = Stock.FirstOrDefault(oProd => oProd.Id == oRow.ProductoId);
                    if (oProducto == null)
                        return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
                    if (oProducto.Cantidad < oCantidadoProducto)
                        return $"Error: no cuenta con los ingredientes suficientes para {Nombre}.";
                }
                //Decreción
                foreach (Ingrediente oRow in Ingredientes)
                {
                    var oCantidadoProducto = pCantidad * oRow.Cantidad;
                    var oProducto = Stock.FirstOrDefault(oProd => oProd.Id == oRow.ProductoId);
                    oProducto.Salida(oCantidadoProducto);
                }
                this.Cantidad = pCantidad;
                return $"Hecho: se ha generado {pCantidad} de {Nombre} (Costo: {Costo}, Precio: {Precio}, Utilidad: {Utilidad}).";
            }
        }

    }
}
