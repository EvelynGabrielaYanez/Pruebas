using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Administrador : Empleado
    {
        public Administrador(int dni, string nombreUsuario, string contrasenia) : base(dni, nombreUsuario, contrasenia)
        {
        }
        public Administrador(int dni, string nombre, string apellido, string nombreUsuario, string contrasenia) : base(dni, nombre, apellido, nombreUsuario, contrasenia)
        { 
        }
        public override bool EliminarUsuario(int dni)
        {
            bool respuesta = false;
            foreach (Usuario usuario in PetShop.usuarios)
            {
                if (dni == usuario.Dni)
                {
                    usuario.Activo = false;
                    respuesta = true;
                    break;
                }
            }
            return respuesta;
        }
        public bool EditarEmpleado(Empleado usuarioAEditar, Empleado usuarioDatos)
        {
            bool respuesta = false;

            if (usuarioAEditar.Dni == usuarioDatos.Dni)
            {
                usuarioAEditar.Nombre = usuarioDatos.Nombre;
                usuarioAEditar.Apellido = usuarioDatos.Apellido;
                usuarioAEditar.NombreUsuario = usuarioDatos.NombreUsuario;
                usuarioAEditar.Contrasenia = usuarioDatos.Contrasenia;
                respuesta = true;
            }
            return respuesta;
        }

        public bool ActualizarEmpleado(string nombre, string apellido, int dni,string nombreUsuario,string contrasenia, ETipoUsuario tipoEmpleado)
        {
            bool respuesta = false;
            Empleado empleadoEditado;
            if (tipoEmpleado == ETipoUsuario.Administador)
                empleadoEditado = new Administrador(dni, nombre, apellido, nombreUsuario, contrasenia);
            else
                empleadoEditado = new Empleado(dni, nombre, apellido, nombreUsuario, contrasenia);

            foreach (Usuario usuario in PetShop.usuarios)
            {
                if (usuario is Empleado && empleadoEditado.Dni == usuario.Dni)
                {
                    PetShop.usuarios.Remove(usuario);
                    PetShop.usuarios.Add(empleadoEditado);
                    respuesta = true;
                    break;
                }
            }
            return respuesta;
        }
        public bool AgregarEmpleado(string nombre, string apellido, int dni, string nombreUsuario, string contrasenia,ETipoUsuario tipoEmpleado)
        {
            bool response = false;
            Empleado nuevoEmpleado;
            if (tipoEmpleado == ETipoUsuario.Administador)
                nuevoEmpleado = new Administrador(dni, nombre, apellido, nombreUsuario, contrasenia);
            else
                nuevoEmpleado = new Empleado(dni, nombre, apellido, nombreUsuario, contrasenia);
            if (!PetShop.BuscarYValidarUsuario(nuevoEmpleado, out _))
            {
                PetShop.usuarios.Add(nuevoEmpleado);
                response = true;
            }
            return response;
        }
    }
}
