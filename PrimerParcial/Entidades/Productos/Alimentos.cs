using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Alimento : Producto
    {
        double Kilos;
        ETipoDeAnimal animalQueLoConsume;
        public Alimento(string descripcion, double precio, string marca, double Kilos, ETipoDeAnimal animalQueLoConsume, string proveedor,int stock) : base(descripcion, precio, marca,proveedor, stock)
        {
            this.Kilos = Kilos;
            this.animalQueLoConsume = animalQueLoConsume;
        }
    }
}
