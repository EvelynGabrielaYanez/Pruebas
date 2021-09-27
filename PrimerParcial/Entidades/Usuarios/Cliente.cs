using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente : Usuario
    {
        // El producto y la cantidad del mismo Este va a ser el listado de productos comprados cuando el producto exista se lo va a sumar cuando compren
        Dictionary<Producto, int> productosComprados; 
        double saldo;
        Dictionary<Producto, int> carrito;
        public Cliente(int dni, string nombre, string apellido) : base(dni, nombre, apellido)
        {
            this.saldo = 0;
            productosComprados = new Dictionary<Producto, int>();
            carrito = new Dictionary<Producto, int>();
        }
        public double Saldo
        {
            get { return this.saldo; }
            set {
                this.saldo = value;
            }
        }
        public Dictionary<Producto, int> Carrito
        {
            get { 
                return this.carrito; 
            }
        }
        public Dictionary<Producto, int> ProductosComprados
        {
            get
            {
                return this.carrito;
            }
        }
        public Producto this[int id]
        {
            set 
            {
                KeyValuePair<Producto, int> parProductoCantidad = this.BuscarProductoEnCanasto(id);
                if (!parProductoCantidad.Equals(default(KeyValuePair<Producto, int>)))
                {
                    this.carrito.Remove(parProductoCantidad.Key);
                    this.carrito.Add(parProductoCantidad.Key, parProductoCantidad.Value + 1);
                }
                else
                {
                    this.carrito.Add(value,1);
                }
            }
        }
        public bool RealizarCompra()
        {
            bool retorno = false;
            double gastosTotales = 0;
            if (this.carrito.Count > 0)
            {
                foreach (KeyValuePair<Producto, int> producto in this.carrito)
                {
                    if (!this.productosComprados.TryAdd(producto.Key, producto.Value))
                    {
                        int cantidadActual = this.carrito.GetValueOrDefault(producto.Key);
                        // Reduce el stock del producto en el petship
                        producto.Key.Stock -= cantidadActual;
                        // Elimina el producto y vuelve  a cargar el nuevo modificado
                        this.productosComprados.Remove(producto.Key);
                        this.productosComprados.Add(producto.Key, cantidadActual + producto.Value);
                    }
                    gastosTotales += producto.Key.Precio;
                }
                this.carrito.Clear();
                this.Saldo -= gastosTotales;
                retorno = true;
            }
            return retorno;
        }
        public KeyValuePair<Producto, int> BuscarProductoEnCanasto(int id)
        {
            KeyValuePair<Producto, int> retorno = new KeyValuePair<Producto, int>();
            foreach (KeyValuePair<Producto,int> producto in carrito)
            {
                if (producto.Key.Id == id)
                {
                    retorno = producto;
                    break;
                }
            }
            return retorno;
        }

        public bool ValidarProductoEnCanasto(int id)
        {
            return !this.BuscarProductoEnCanasto(id).Equals(default(KeyValuePair<Producto, int>));
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
