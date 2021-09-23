using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Inventario.Domain;
using System.Collections.Generic;

namespace ParcialUno
{
    [TestClass]
    public class UnitTest
    {
        /*
            HU1. ENTRADA DE PRODUCTO (1.5)
            COMO USUARIO QUIERO REGISTRAR LA ENTRADA PRODUCTOS 
            CRITERIOS DE ACEPTACIÓN
             1. La cantidad de la entrada de debe ser mayor a 0
             2. La cantidad de la entrada se le aumentará a la cantidad existente del producto
        */
        [TestMethod("HU1: Entrada de producto")]
        [DataRow(2, 0, 0, "Salchicha", 0, 0, true, DisplayName = "(Rojo) Caso de prueba entrada en cero")]
        [DataRow(2, 1, 1000, "Salchicha", 1500, 1, true, true, DisplayName = "(Rojo) Caso de prueba entrada de compuesto")]
        [DataRow(2, 2, 1000, "Salchicha", 1500, 1, false, DisplayName = "(Verde) Caso de prueba escenario correcto")]
        public void EntradaDeProducto
        (
            int pId,
            double pCantidad,
            double pCosto,
            string pNombre,
            double pPrecio,
            double pEntrada,
            bool pRojo = false,
            bool pEsCompuesto = false,
            string pNombreCompuesto = "Compuesto"
        )
        {
            #region Preparacion
            var oProductos = new List<Producto>()
            {
                new Producto
                (
                    new ProductoDTO()
                    {
                        Id = pEsCompuesto ? pId + 1 : pId,
                        Cantidad = pCantidad,
                        Costo = pCosto,
                        Nombre = pNombre,
                        Precio = pPrecio,
                        Ingredientes = new List<Ingrediente>()
                    }
                )
            };

            if (pEsCompuesto)
            {
                oProductos.Add
                (
                    new Compuesto
                    (
                        new ProductoDTO()
                        {
                            Id = pId,
                            Nombre = pNombreCompuesto,
                            Precio = 1500,
                            Ingredientes = new List<Ingrediente>()
                            {
                                new Ingrediente()
                                {
                                    ProductoId = pId + 1,
                                    Cantidad = 1,
                                    Costo = pCosto
                                }
                            }
                        }
                    )
                );
            }

            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );
            #endregion

            var Respuesta = oRestaurante.Entrada(pId, pEntrada);

            if (Respuesta.ToString().Contains("Error:") && !pRojo)
                throw new AssertFailedException(Respuesta.ToString());

            Console.WriteLine(Respuesta);
        }

        /*
            HU1. SALIDA DE PRODUCTO (3.5)
            COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS 
            CRITERIOS DE ACEPTACIÓN
            1. La cantidad de la salida de debe ser mayor a 0
            2. En caso de un producto sencillo la cantidad de la salida se le disminuirá a la cantidad 
            existente del producto.
            3. En caso de un producto compuesto la cantidad de la salida se le disminuirá a la cantidad 
            existente de cada uno de su ingrediente
            4. Cada salida debe registrar el costo del producto y el precio de la venta
            5. El costo de los productos compuestos corresponden al costo de sus ingredientes por la 
            cantidad de estos.
        */
        [TestMethod("HU2: Salida de producto")]
        [DataRow(2, 0, 0, "Salchicha", 0, 0, true, DisplayName = "(Rojo) Caso de prueba salida en cero")]
        [DataRow(2, 0, 1000, "Salchicha", 1500, 1, true, true, DisplayName = "(Rojo) Caso de prueba salida de compuesto sin stock de ingredientes")]
        [DataRow(2, 2, 1000, "Salchicha", 1500, 1, false, DisplayName = "(Verde) Caso de prueba escenario correcto simple")]
        [DataRow(2, 2, 1000, "Salchicha", 1500, 1, false, true, "Perro", DisplayName = "(Verde) Caso de prueba escenario correcto compuesto")]
        public void SalidaDeProducto
        (
            int pId,
            double pCantidad,
            double pCosto,
            string pNombre,
            double pPrecio,
            double pSalida,
            bool pRojo = false,
            bool pEsCompuesto = false,
            string pNombreCompuesto = "Compuesto"
        )
        {
            #region Preparacion
            var oProductos = new List<Producto>()
            {
                new Producto
                (
                    new ProductoDTO()
                    {
                        Id = pEsCompuesto ? pId + 1 : pId,
                        Cantidad = pCantidad,
                        Costo = pCosto,
                        Nombre = pNombre,
                        Precio = pEsCompuesto ? pPrecio - 100 : pPrecio,
                        Ingredientes = new List<Ingrediente>()
                    }
                )
            };
            if (pEsCompuesto)
            {
                oProductos.Add
                (
                    new Compuesto
                    (
                        new ProductoDTO()
                        {
                            Id = pId,
                            Nombre = pNombreCompuesto,
                            Precio = pPrecio,
                            Ingredientes = new List<Ingrediente>()
                            {
                                new Ingrediente()
                                {
                                    ProductoId = pId + 1,
                                    Cantidad = 1,
                                    Costo = pCosto
                                }
                            }
                        }
                    )
                );
            }
            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );
            #endregion

            var Respuesta = oRestaurante.Salida(pId, pSalida);

            if (Respuesta.ToString().Contains("Error:") && !pRojo)
                throw new AssertFailedException(Respuesta.ToString());

            //Validación de la venta
            if (!Respuesta.ToString().Contains("Error:"))
            {
                var Indice = oRestaurante.Ventas.Count;
                if(Indice <= 0)
                    throw new AssertFailedException($"Error: no se registro la venta");
                var UltimaVenta = oRestaurante.Ventas[Indice - 1];
                if (ValidarVenta(pCosto, pPrecio, UltimaVenta))
                    throw new AssertFailedException($"Error: inconsistencia en los registros de la venta esperados: ({1000},{2500}) vs obtenidos: ({UltimaVenta.Costo},{UltimaVenta.Precio})");
            }

            Console.WriteLine(Respuesta);
        }

        private bool ValidarVenta(double pCosto, double pPrecio, Venta pVenta)
        {
            if (pVenta.Costo != pCosto)
                return true;
            if (pVenta.Precio != pPrecio)
                return true;
            else
                return false;
        }
    }
}
