using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empleado : Usuario
    {
        protected string nombreUsuario;
        protected string contrasenia;
        public Empleado(int dni, string nombreUsuario, string contrasenia) : base(dni)
        {
            this.nombreUsuario = nombreUsuario;
            this.contrasenia = contrasenia;
        }
        public Empleado(int dni, string nombre, string apellido, string nombreUsuario, string contrasenia) : base(dni, nombre, apellido)
        {
            this.nombreUsuario = nombreUsuario;
            this.contrasenia = contrasenia;
        }
        public string NombreUsuario
        {
            get { return this.nombreUsuario; }
            set { this.nombreUsuario = value; }
        }
        public string Contrasenia
        {
            get { return this.contrasenia; }
            set { this.contrasenia = value; } // Esto esta mal
        }
        public override bool Equals(Object empleado)
        {
            return this == (Empleado)empleado;
        }
        public static bool operator ==(Empleado empleado1, Empleado empleado2)
        {
            bool retorno = false;
            if (empleado1 is not null && empleado2 is not null)
                retorno = empleado1.nombreUsuario == empleado2.nombreUsuario && empleado1.Dni == empleado2.Dni;
            else if (empleado1 is null && empleado2 is null)
                retorno = true;
            return retorno;
        }
        public static bool operator !=(Empleado empleado1, Empleado empleado2)
        {
            return !(empleado1 == empleado2);
        }

        public override bool ValidarUsuario(Usuario usuario)
        {
            bool respuesta = false;
            if (usuario is Empleado)
            {
                Empleado empleado = (Empleado)usuario;
                respuesta =this.Contrasenia == empleado.contrasenia && empleado == this;
            }
            return respuesta;
        }

        public virtual bool EliminarUsuario(int dni)
        {
            bool respuesta = false;
            foreach (Usuario usuario in PetShop.usuarios)
            {
                if (usuario is Cliente && dni == usuario.Dni)
                {
                    usuario.Activo = false;
                    respuesta = true;
                    break;
                }
            }
            return respuesta;
        }
        public bool ActualizarCliente(string nombre, string apellido, int dni)
        {
            bool respuesta = false;
            Cliente usuarioEditado = new Cliente(dni, nombre, apellido);
            foreach (Usuario usuario in PetShop.usuarios)
            {
                if (usuario.GetType() == typeof(Cliente) && usuarioEditado.Dni == usuario.Dni)
                {
                    PetShop.usuarios.Remove(usuario);
                    PetShop.usuarios.Add(usuarioEditado);
                    respuesta = true;
                    break;
                }
            }
            return respuesta;
        }
        public bool AgregarCliente(string nombre, string apellido, int dni)
        {
            bool respuesta = false;
            Cliente nuevoUsuario = new Cliente(dni, nombre, apellido);
            if (!PetShop.ValidarExistencia(nuevoUsuario))
            {
                PetShop.usuarios.Add(nuevoUsuario);
                respuesta = true;
            }
            return respuesta;
        }
    }
}
