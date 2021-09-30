using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Inventario.Domain;
using System.Collections.Generic;

namespace ParcialUno
{
    [TestClass]
    public class RefactoringTest
    {
        /*
         Entrada producto -1
        H1: COMO USUARIO QUIERO REGISTRAR LA ENTRADA PRODUCTOS
        Criterio de aceptación:
        1. La cantidad de la entrada de debe ser mayor a 0
        Dado	En el restaurante hay producto nombre: “Gaseosa”, cantidad: 10, costo: 1000, precio: 2000, utilidad:1000
        Cuando	Se registre entrada de -1
        Entonces	El sistema arrojara un mensaje “La entrada del producto es incorrecta”
         */
        [TestMethod]
        public void EntradaProductoDeMenosUno()
        {
            var oProductos = new List<Producto>()
            {
                new Producto
                (
                    new ProductoDTO()
                    {
                        Id = 1,
                        Cantidad = 10,
                        Costo = 1000,
                        Nombre = "Gasimba",
                        Precio = 2000
                    }
                )
            };

            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );

            var Entrada = new Entrada(1, -1);
            var Respuesta = oRestaurante.Entrada(new List<Entrada>() { Entrada });

            Assert.AreEqual("Error: La cantidad debe ser mayor a cero.", Respuesta);
        }
        /*
         Aumentar entrada En producto
        H1: COMO USUARIO QUIERO REGISTRAR LA ENTRADA PRODUCTOS
        Criterio de aceptación:
        2. La cantidad de la entrada se le aumentará a la cantidad existente del producto
        Dado	En el restaurante hay producto nombre: “Gaseosa”, cantidad: 10, costo: 1000, precio: 2000, utilidad:1000
        Cuando	Se registre la entrada de 10
        Entonces	El sistema arrojara un mensaje “La cantidad del Gaseosa aumento y es de 20”
         */
        [TestMethod]
        public void AumentarCantidadEntradaEnProducto()
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
                        Nombre = "Gasimba",
                        Precio = 2000
                    }
                )
            };

            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );

            var Entrada = new Entrada(1, 10);
            var Respuesta = oRestaurante.Entrada(new List<Entrada>() { Entrada });

            Assert.AreEqual("Hecho: su nueva cantidad es 10.", Respuesta);
        }

        /*
         Salida de producto -1
        H2: COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
        Criterio de aceptación:
        2.1. La cantidad de la salida de debe ser mayor a 0
        Dado	En el restaurante hay producto nombre: “Gaseosa”, cantidad: 10, costo: 1000, precio: 2000, utilidad:1000
        Cuando	Se retira producto de -1
        Entonces	El sistema arrojara un mensaje “La cantidad de salida es incorrecta”
         */
        [TestMethod]
        public void SalidaDeProductoDeMenosUno()
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
                        Nombre = "Gasimba",
                        Precio = 2000
                    }
                )
            };

            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );

            var Salida = new Salida(1, -1);
            string Respuesta = oRestaurante.Salida(new List<Salida>() { Salida });

            Assert.AreEqual("Error: La cantidad debe ser mayor a cero.", Respuesta);
        }
        /*
         Disminuir cantidad de productos salientes
        H2: COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
        Criterio de aceptación:
        2.2. En caso de un producto sencillo la cantidad de la salida se le disminuirá a la cantidad existente del producto.
        Dado	En el restaurante hay producto nombre: “Gaseosa”, cantidad: 10, costo: 1000, precio: 2000, utilidad:1000
        Cuando	Se retira producto de 2
        Entonces	El sistema arrojara un mensaje “La cantidad de Gaseosa restante es de 8”
         */
        [TestMethod]
        public void DisminuirCantidadDeProductosSalientes()
        {
            var oProductos = new List<Producto>()
            {
                new Producto
                (
                    new ProductoDTO()
                    {
                        Id = 1,
                        Cantidad = 10,
                        Costo = 1000,
                        Nombre = "Gasimba",
                        Precio = 2000
                    }
                )
            };

            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );

            var Salida = new Salida(1, 2);
            string Respuesta = oRestaurante.Salida(new List<Salida>() { Salida });

            Assert.AreEqual("Hecho: su nueva cantidad es 8.", Respuesta);
        }
        /*
            Disminuir cantidad de productos compuestos salientes y guardar la venta
            H2: COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
            Criterio de aceptación:
            2.3. En caso de un producto compuesto la cantidad de la salida se le disminuirá a la cantidad existente de cada uno de su ingrediente
            2. 4. Cada salida debe registrar el costo del producto y el precio de la venta
            Dado	En el restaurante quiere hacer un combo perro doble y tiene productos: nombre: “Gaseosa”, cantidad: 10, costo: 1000, precio: 2000, utilidad:1000
            “Salchicha”, cantidad: 40, costo: 1000, precio: 2000, utilidad:1000
            “Laminas queso Mozarela”, cantidad: 100, costo: 700, precio: 1500, utilidad: 800
            “Pan de perro”, cantidad: 60, costo: 1000, precio:1500, utilidad:500
            Cuando	Se retira una gaseosa, unas dos salchichas, dos láminas de queso y un pan de perro
            Entonces	El sistema arrojara un mensaje “Combo perro doble: El costo de la venta $5400 y un precio de $10500 y la utilidad $5100”
         */
        [TestMethod]
        public void DisminuirCantidadDeProductosCompuestosSalientesGuardarLaVenta()
        {
            var Gaseosa = new Producto
            (
                new ProductoDTO()
                {
                    Id = 1,
                    Cantidad = 10,
                    Costo = 1000,
                    Nombre = "Gasimba",
                    Precio = 2000
                }
            );

            var Salchicha = new Producto
            (
                new ProductoDTO()
                {
                    Id = 2,
                    Cantidad = 40,
                    Costo = 1000,
                    Nombre = "Salchicha",
                    Precio = 2000
                }
            );

            var Lamina = new Producto
            (
                new ProductoDTO()
                {
                    Id = 3,
                    Cantidad = 100,
                    Costo = 700,
                    Nombre = "Lámina de queso",
                    Precio = 1500
                }
            );

            var Pan = new Producto
            (
                new ProductoDTO()
                {
                    Id = 4,
                    Cantidad = 60,
                    Costo = 1000,
                    Nombre = "Pan de perro",
                    Precio = 1500
                }
            );

            var oProductos = new List<Producto>()
            {
                Gaseosa,
                Salchicha,
                Lamina,
                Pan
            };

            var oProductoVendido = new Compuesto
            (
                new ProductoDTO()
                {
                    Id = 5,
                    Nombre = "Combo perro",
                    Precio = 1500,
                    Ingredientes = new List<Ingrediente>()
                    {
                        new Ingrediente(Salchicha, 2),
                        new Ingrediente(Gaseosa, 1),
                        new Ingrediente(Lamina, 2),
                        new Ingrediente(Pan, 1)
                    }
                }
            );

            oProductos.Add
            (
                oProductoVendido
            );

            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );

            var Salida = new Salida(5, 2);
            string Respuesta = oRestaurante.Salida(new List<Salida>() { Salida });

            Assert.AreEqual("Hecho: se ha generado 2 de Combo perro (Costo: 10800, Precio: 21000, Utilidad: 10200).", Respuesta);

            //Valida ventas
            Assert.AreEqual(1, oRestaurante.Ventas.Count);
        }

        /*
            Disminuir cantidad de productos compuestos salientes y guardar la venta
            H2: COMO USUARIO QUIERO REGISTRAR LA SALIDA PRODUCTOS
            Criterio de aceptación:
            2.3. En caso de un producto compuesto la cantidad de la salida se le disminuirá a la cantidad existente de cada uno de su ingrediente
            2. 4. Cada salida debe registrar el costo del producto y el precio de la venta
            Dado	En el restaurante quiere hacer un combo perro doble y tiene productos: nombre: “Gaseosa”, cantidad: 10, costo: 1000, precio: 2000, utilidad:1000
            “Salchicha”, cantidad: 40, costo: 1000, precio: 2000, utilidad:1000
            “Laminas queso Mozarela”, cantidad: 100, costo: 700, precio: 1500, utilidad: 800
            “Pan de perro”, cantidad: 60, costo: 1000, precio:1500, utilidad:500
            Cuando	Se retira una gaseosa, unas dos salchichas, dos láminas de queso y un pan de perro
            Entonces	El sistema arrojara un mensaje “Combo perro doble: El costo de la venta $5400 y un precio de $10500 y la utilidad $5100”
         */
        [TestMethod]
        public void DisminuirCantidadDeProductosCompuestosSalientesGuardarLaVentaGasimba()
        {
            var Gaseosa = new Producto
            (
                new ProductoDTO()
                {
                    Id = 1,
                    Cantidad = 10,
                    Costo = 1000,
                    Nombre = "Gasimba",
                    Precio = 2000
                }
            );

            var Salchicha = new Producto
            (
                new ProductoDTO()
                {
                    Id = 2,
                    Cantidad = 40,
                    Costo = 1000,
                    Nombre = "Salchicha",
                    Precio = 2000
                }
            );

            var Lamina = new Producto
            (
                new ProductoDTO()
                {
                    Id = 3,
                    Cantidad = 100,
                    Costo = 700,
                    Nombre = "Lámina de queso",
                    Precio = 1500
                }
            );

            var Pan = new Producto
            (
                new ProductoDTO()
                {
                    Id = 4,
                    Cantidad = 60,
                    Costo = 1000,
                    Nombre = "Pan de perro",
                    Precio = 1500
                }
            );

            var oProductos = new List<Producto>()
            {
                Gaseosa,
                Salchicha,
                Lamina,
                Pan
            };

            var oProductoVendido = new Compuesto
            (
                new ProductoDTO()
                {
                    Id = 5,
                    Nombre = "Combo perro",
                    Precio = 1500,
                    Ingredientes = new List<Ingrediente>()
                    {
                        new Ingrediente(Salchicha, 2),
                        new Ingrediente(Gaseosa, 2),
                        new Ingrediente(Lamina, 2),
                        new Ingrediente(Pan, 1)
                    }
                }
            );

            oProductos.Add
            (
                oProductoVendido
            );

            var oRestaurante = new Restaurante
            (
                1,
                "Doña chepita",
                oProductos
            );

            var Salida = new Salida(5, 2);
            string Respuesta = oRestaurante.Salida(new List<Salida>() { Salida });

            Assert.AreEqual("Hecho: se ha generado 2 de Combo perro (Costo: 12800, Precio: 25000, Utilidad: 12200).", Respuesta);

            //Valida ventas
            Assert.AreEqual(1, oRestaurante.Ventas.Count);
        }

    }
}
