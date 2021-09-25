using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class PetShop
    {
        public static List<Usuario> usuarios; // Ver si se puede hacer un getter para esto

        static PetShop()
        {
            usuarios = new List<Usuario>();

            // Hardcodea (Pasar esto a un metodo asi queda mas prolijo)
            Empleado empleado1 = new Empleado(39429759, "FedeSco", "empleado");
            Empleado empleado2 = new Empleado(39429758, "CamiYa", "empleado");
            Administrador adminsitrador1 = new Administrador(39429757,"admin","admin");
            Administrador adminsitrador2 = new Administrador(39429756,"pro", "pro");
            Cliente cliente1 = new Cliente(39429755,"evelyn","yanez");
            Cliente cliente2 = new Cliente(39429754,"lucas","yanez");

            PetShop.usuarios.Add(empleado1);
            PetShop.usuarios.Add(empleado2);
            PetShop.usuarios.Add(adminsitrador1);
            PetShop.usuarios.Add(adminsitrador2);
            PetShop.usuarios.Add(cliente1);
            PetShop.usuarios.Add(cliente2);
        }
        public static bool BuscarYValidarUsuario(Usuario usuarioAValidar, out Usuario usuarioRespuesta)
        {
            bool retorno = false;
            usuarioRespuesta = null;
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Activo && usuarioAValidar.ValidarUsuario(usuario)) 
                {
                    usuarioRespuesta = usuario;
                    retorno = true;
                    break;
                }
            }
            return retorno;
        }
        public static bool ValidarExistencia(Usuario usuarioAValidar)
        {
            bool respuesta = false;
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Activo && usuario == usuarioAValidar)
                {
                    respuesta = true; 
                    break;
                }
            }
            return respuesta;
        }

        public static Usuario BuscarUsuario(Usuario usuarioAValidar)
        {
            Usuario respuesta = null;
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Activo && usuario == usuarioAValidar && usuario.Activo)
                {
                    respuesta = usuario;
                    break;
                }
            }
            return respuesta;
        }

        public static List<Usuario> FiltrarListado(Type tipoDeUsuario)
        {
            List<Usuario> listaRetorno = new List<Usuario>();
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Activo && usuario.GetType() == tipoDeUsuario)
                {
                    listaRetorno.Add(usuario);
                }
            }
            return listaRetorno;
        }
    }
}
