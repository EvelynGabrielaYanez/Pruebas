using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente : Usuario
    {
        Dictionary<Producto, int> productosComprados; // El producto y la cantidad del mismo Este va a ser el listado de productos comprados cuando el producto exista se lo va a sumar cuando compren
        double dineroInvertido;
        Dictionary<Producto, int> canasto;
        public Cliente(int dni, string nombre, string apellido) : base(dni, nombre, apellido)
        {
            this.dineroInvertido = 0;
            productosComprados = new Dictionary<Producto, int>();
            canasto = new Dictionary<Producto, int>();
        }
        public double DineroInvertido
        {
            get { return this.dineroInvertido; }
        }

        public bool AgregarProducto(Producto producto)
        {
            return true;
        }

        public void RealizarCompra()
        {
            // Agrega canasto a productos comprados y lo vacia
            dineroInvertido++; // Esta va a sumar el importe de las ocmpras realizadas

        }
        public static bool operator == (Cliente cliente1, Cliente cliente2)
        {
            bool retorno = false;
            if (cliente1 is not null && cliente2 is not null)
                retorno = cliente1.Dni == cliente2.Dni;
            else if (cliente1 is null && cliente2 is null)
                retorno = true;
            return retorno;
        }
        public override bool Equals(Object cliente)
        {
            return this == (Cliente)cliente;
        }
        public static bool operator !=(Cliente cliente1, Cliente cliente2)
        {
            return !(cliente1 == cliente2);
        }

        public override bool ValidarUsuario(Usuario usuario)
        {
            bool respuesta = false;
            if (usuario is Empleado)
            {
                Cliente cliente = (Cliente)usuario;
                respuesta = this.Nombre == cliente.Nombre && this.Apellido == cliente.Apellido && cliente == this;
            }
            return respuesta;
        }
    }
}
