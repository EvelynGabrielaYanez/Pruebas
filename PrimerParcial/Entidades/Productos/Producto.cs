using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Producto
    {
        protected string descripcion;
        protected double precio;
        protected string marca;
        protected string proveedor;
        public Producto(string descripcion,double precio, string marca, string proveedor)
        {
            this.descripcion = descripcion;
            this.marca = marca;
            this.precio = precio;
            this.proveedor = "";

        }


    }
}
