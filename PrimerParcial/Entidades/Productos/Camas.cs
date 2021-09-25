using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cama : Producto
    {
        string tipoDeReyeno;
        string tipoDeTela;
        public Cama(string descripcion, double precio, string marca, string proveedor,string tipoDeReyeno, string tipoDeTela) : base(descripcion, precio, marca,proveedor)
        {
            this.tipoDeReyeno = tipoDeReyeno;
            this.tipoDeTela = tipoDeTela;
        }
    }
}
