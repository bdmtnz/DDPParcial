using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Inventario.Domain;
using System.Collections.Generic;

namespace ParcialUno
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void EntradaDeProducto()
        {
            var oProductos = new List<Producto>()
            {
                new Producto
                (
                    new ProductoDTO()
                    {
                        Id = 1,
                        Cantidad = 0,
                        Costo = 1000,
                        Nombre = "Sachicha",
                        Precio = 1500
                    }
                )
            };
            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );

            var Respuesta = oRestaurante.Entrada(1, 10);

            if (Respuesta.ToString().Contains("Error:"))
                throw new AssertFailedException(Respuesta.ToString());

            Console.WriteLine(Respuesta);
        }

        [TestMethod]
        public void SalidaDeProducto()
        {

            var oProductos = new List<Producto>()
            {
                new Producto
                (
                    new ProductoDTO()
                    {
                        Id = 1,
                        Cantidad = 0,
                        Costo = 1000,
                        Nombre = "Salchicha",
                        Precio = 1500,
                        Ingredientes = new List<Ingrediente>()
                    }
                ),
                new Compuesto
                (
                    new ProductoDTO()
                    {
                        Id = 2,
                        Cantidad = 0,
                        Costo = 1000,
                        Nombre = "Perro",
                        Precio = 1500,
                        Ingredientes = new List<Ingrediente>()
                        {
                            new Ingrediente()
                            {
                                ProductoId = 1,
                                Cantidad = 1,
                                Costo = 1000
                            }
                        }
                    }
                )
            };
            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );

            var Respuesta = oRestaurante.Salida(2, 10);

            if (Respuesta.ToString().Contains("Error:"))
                throw new AssertFailedException(Respuesta.ToString());

            Console.WriteLine(Respuesta);
        }
    }
}
