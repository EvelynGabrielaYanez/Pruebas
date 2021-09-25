using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Juguete : Producto
    {
        bool haceSonido;
        bool contieneComida;
        public Juguete(string descripcion, double precio, string marca,bool haceSonido, bool contieneComida,string proveedor) : base(descripcion, precio,marca, proveedor)
        {
            this.haceSonido = haceSonido;
            this.contieneComida = contieneComida;
        }



    }
}
