using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class ArticuloDeCuidado : Producto
    {
        ETipoDeAnimal tipoDeAnimal;
        public ArticuloDeCuidado(string descripcion, double precio, string marca, string proveedor , ETipoDeAnimal tipoDeAnimal, int stock) : base(descripcion, precio, marca, proveedor,stock)
        {
            this.tipoDeAnimal = tipoDeAnimal;
        }


    }
}
