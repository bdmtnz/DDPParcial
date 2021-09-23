using System;
using System.Collections.Generic;
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
        public double Costo { get; private set; }
        public double Precio { get; private set; }
        public double Utilidad => Precio - Costo;

        public Producto(ProductoDTO pDatos)
        {
            Id = pDatos.Id;
            Nombre = pDatos.Nombre;
            Cantidad = pDatos.Cantidad;
            Costo = pDatos.Costo;
            Precio = pDatos.Precio;
        }

        public string Entrada(double pCantidad)
        {
            Cantidad += pCantidad;
            return $"Hecho: su nueva cantidad es {Cantidad}.";
        }

        public string Salida(double pCantidad)
        {
            return "";
        }

    }

    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Cantidad { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
    }

}
