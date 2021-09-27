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
        public static Dictionary<Producto,string> productos; // Ver si se puede hacer un getter para esto

        static PetShop()
        {
            usuarios = new List<Usuario>();
            productos = new Dictionary<Producto, string>();
            // Se HardCodean los listados
            PetShop.HardcodearUsuarios();
            PetShop.HardcodearProductos();
        }

        #region Hardcodeo
        public static void HardcodearUsuarios()
        {
            // Se cargan los empleados
            Empleado empleado1 = new Empleado(39429759, "FedeSco", "empleado");
            Empleado empleado2 = new Empleado(39429758, "CamiYa", "empleado");
            PetShop.usuarios.Add(empleado1);
            PetShop.usuarios.Add(empleado2);

            // Se cargan los administradores
            Administrador adminsitrador1 = new Administrador(39429757, "admin", "admin");
            Administrador adminsitrador2 = new Administrador(39429756, "pro", "pro");
            PetShop.usuarios.Add(adminsitrador1);
            PetShop.usuarios.Add(adminsitrador2);

            // Se cargan los clientes
            Cliente cliente1 = new Cliente(39429755, "evelyn", "yanez");
            Cliente cliente2 = new Cliente(39429754, "lucas", "yanez");
            PetShop.usuarios.Add(cliente1);
            PetShop.usuarios.Add(cliente2);
        }
        public static void HardcodearProductos()
        { 
            // Se cargan alimentos
            Alimento alimento1 = new Alimento("Mix Perros Adultos",300,"Sabrositos",1.5,ETipoDeAnimal.Perro,"MONAMI",15);
            Alimento alimento2 = new Alimento("Regular Fit", 6184, "Sabrositos", 7.5, ETipoDeAnimal.Gato, "MONAMI",50);
            Alimento alimento3 = new Alimento("Mix Canarios Mezcla Semillas", 6184, "Nelsoni R.", 0.75, ETipoDeAnimal.Pajaro, "Pet & Fish",30);
            Alimento alimento4 = new Alimento("Tropimix", 6184, "Zootec", 1, ETipoDeAnimal.Hamster, "Puppis",34);
            PetShop.productos.Add(alimento1, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(alimento2, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(alimento3, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(alimento4, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");

            // Se cargan camas
            Cama cama1 = new Cama("Circular con estampado", 4000, "Coconing", "MascotasYa", "Ecológico hipoalergénico", "Polyester",5);
            Cama cama2 = new Cama("Rectangular y lisa", 5000, "Coconing", "MascotasYa", "Ecológico hipoalergénico", "Funda suave",2);
            Cama cama3= new Cama("Modelo Small", 5000, "Eggys S", "MascotasYa", "Fibra de poliéster", "Funda de peluche suave",1);
            PetShop.productos.Add(cama1, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(cama2, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(cama3, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");

            // Se cargan Juguetes
            Juguete juguete1 = new Juguete("Hueso Soga", 430, "CanCat", false , false, "Puppis",10);
            Juguete juguete2 = new Juguete("Hueso S De Goma Dispensador", 800, "Rascals", false, true, "Puppis",8);
            Juguete juguete3 = new Juguete("Porta Alimento Interactivo", 1540, "PetToys", true, true, "Es Divertido",3);
            PetShop.productos.Add(juguete1, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(juguete2, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(juguete3, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");

            // Se cargan los Articulos de Cuidado de la Mascota
            // Articulos de Farmacia
            ArticuloDeFarmacia articuloFarmacia1 = new ArticuloDeFarmacia("Perrolac - Sustituto Lacteo 500g", 1390, "Alimasc", "Foyel", ETipoDeAnimal.Perro,false,7);
            ArticuloDeFarmacia articuloFarmacia2 = new ArticuloDeFarmacia("Algel en comprimidos con tramadol 120 Comprimidos", 450, "Algen", "Foyel", ETipoDeAnimal.Gato,true,6);
            ArticuloDeFarmacia articuloFarmacia3 = new ArticuloDeFarmacia("Vitaminas para hamsters", 600, "Omega3 Pets", "Puppis", ETipoDeAnimal.Hamster, false,4);
            ArticuloDeFarmacia articuloFarmacia4 = new ArticuloDeFarmacia("Beepower - Promotor De Crecimiento 100ml", 1390, "Exzootix", "Puppis", ETipoDeAnimal.Pajaro, true,10);
            PetShop.productos.Add(articuloFarmacia1, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(articuloFarmacia2, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(articuloFarmacia3, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(articuloFarmacia4, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");

            // Articulos de Limpieza
            Limpieza limpieza1 = new Limpieza("Cepillo Removedor De Pelos", 1000 , "PAKEWAY", "Puppis", ETipoDeAnimal.Perro, ETipoDePelaje.Largo,9);
            Limpieza limpieza2 = new Limpieza("Sacapelos Y Masajeador", 1500 , "FuriMinator", "Puppis", ETipoDeAnimal.Perro, ETipoDePelaje.Rizado,11);
            Limpieza limpieza3 = new Limpieza("Guante Sacapelos", 2500 , "FuriMinator", "Puppis", ETipoDeAnimal.Gato, ETipoDePelaje.Corto,69);
            Limpieza limpieza4 = new Limpieza("Shapoo", 2500 , "PAKEWAY", "Alimasc", ETipoDeAnimal.Perro, ETipoDePelaje.Raso,10);
            PetShop.productos.Add(limpieza1, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(limpieza2, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(limpieza3, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
            PetShop.productos.Add(limpieza4, ".\\Resources\\Alimento_Sabrositos_Para_Perros.jpg");
        }
        #endregion

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

        public static Dictionary<Producto, string> FiltrarListadoProducto(Type tipoDeProducto)
        {
            Dictionary<Producto, string> retorno = new Dictionary<Producto, string>();
            foreach (KeyValuePair< Producto,string> producto in productos)
            {
                if (producto.Key.GetType() == tipoDeProducto)
                {
                    retorno.Add(producto.Key, producto.Value);
                }
            }
            return retorno;
        }

        public static Producto BuscarProducto(int id)
        {
            Producto productoRetorno = null;
            foreach (KeyValuePair<Producto, string> producto in PetShop.productos)
            {
                if (producto.Key.Id == id)
                {
                    productoRetorno = producto.Key;
                    break;
                }
            }
            return productoRetorno;
        }
    }
}
