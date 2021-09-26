using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Entidades
{
    public abstract class Producto
    {
        protected string descripcion;
        protected double precio;
        protected string marca;
        protected string proveedor;
        static int ultimoId;
        protected int id;
        static Producto()
        {
            Producto.ultimoId = 0;
        }
        public Producto(string descripcion,double precio, string marca, string proveedor)
        {
            this.descripcion = descripcion;
            this.marca = marca;
            this.precio = precio;
            this.proveedor = proveedor;
            this.id = Producto.ultimoId;
            Producto.ultimoId++;

        }
        public string Descripcion
        {
            get { return this.descripcion; }
        }
        public int Id
        {
            get { return this.id; }
        }
        public string Marca
        {
            get { return this.marca; }
        }


    }
}
