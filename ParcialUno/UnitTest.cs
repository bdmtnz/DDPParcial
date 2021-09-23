using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using Inventario.Domain;

namespace ParcialUno
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void EntradaDeProducto()
        {
            var oRestaurante = new Restaurante();

            var Respuesta = oRestaurante.Entrada("Salchicha", 1);

            if (Respuesta.ToString().Contains("Error:"))
                new AssertFailedException(Respuesta.ToString());

            Console.WriteLine(Respuesta);
        }
    }
}
