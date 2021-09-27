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
        protected int stock;
        static Producto()
        {
            Producto.ultimoId = 0;
        }
        public Producto(string descripcion,double precio, string marca, string proveedor, int stock)
        {
            this.descripcion = descripcion;
            this.marca = marca;
            this.precio = precio;
            this.proveedor = proveedor;
            this.stock = stock;
            this.id = Producto.ultimoId;
            Producto.ultimoId++;

        }
        public int Id
        {
            get { return this.id; }
        }
        public string Descripcion
        {
            get { return this.descripcion; }
        }
        public string Marca
        {
            get { return this.marca; }
        }
        public double Precio
        {
            get { return this.precio; }
        }
        public string Proveedor
        {
            get { return this.proveedor; }
        }
        public int Stock
        {
            set { this.stock = value; }
            get { return this.stock; }
        }

        public string ObtenerTipoDeProducto()
        {
            string retorno;

            if (this.GetType() == typeof(Alimento))
                retorno = ETipoDeProducto.Alimento.ToString();
            else if(this.GetType() == typeof(Cama))
                retorno = ETipoDeProducto.Cama.ToString();
            else if (this.GetType() == typeof(Juguete))
                retorno = ETipoDeProducto.Juguete.ToString();
            else
                retorno = ETipoDeProducto.ArticuloDeCuidado.ToString();

            return retorno;
        }

    }
}
