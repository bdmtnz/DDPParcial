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
        public virtual double Cantidad { get; protected set; }
        public virtual double Costo { get; private set; }
        public virtual double Precio { get; private set; }
        public double Utilidad => Precio - Costo;

        public Producto(ProductoDTO pDatos)
        {
            Id = pDatos.Id;
            Nombre = pDatos.Nombre;
            Cantidad = pDatos.Cantidad;
            Costo = pDatos.Costo;
            Precio = pDatos.Precio;
        }

        public virtual string Entrada(double pCantidad)
        {
            Cantidad += pCantidad;
            return $"Hecho: su nueva cantidad es {Cantidad}.";
        }

        public virtual string Salida(double pCantidad, ReadOnlyCollection<Producto> Stock = null)
        {
            if (ValidarStock(pCantidad))
                return $"Error: no cuenta con las unidades suficientes de {Nombre}.";

            Cantidad -= pCantidad;
            return $"Hecho: su nueva cantidad es {Cantidad}.";
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
