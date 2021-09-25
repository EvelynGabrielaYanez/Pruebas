using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ArticuloDeFarmacia : ArticuloDeCuidado
    {
        bool requierePrescripcion;
        public ArticuloDeFarmacia(string descripcion, double precio, string marca, string proveedor, ETipoDeAnimal tipoDeAnimal, bool requierePrescripcion) : base(descripcion, precio, marca, proveedor, tipoDeAnimal)
        {
            this.requierePrescripcion = requierePrescripcion;
        }
    }
}
