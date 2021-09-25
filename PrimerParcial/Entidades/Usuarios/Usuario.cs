using System;
using System.Collections.Generic;

namespace Entidades
{
    public abstract class Usuario
    {
        protected string nombre;
        protected string apellido;
        protected int dni;
        protected bool activo;

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public string Apellido
        {
            get { return this.apellido; }
            set { this.apellido = value; }
        }
        public int Dni
        {
            get { return this.dni; }
        }
        public bool Activo
        {
            get { return this.activo; }
            set { this.activo = value; }
        }

        private Usuario()
        {
            this.nombre = "";
            this.apellido = "";
            this.activo = true;
        }
        protected Usuario(int dni) :this()
        {
            this.dni = dni;
        }
        protected Usuario(int dni,string nombre, string apellido) : this(dni)
        {
            this.nombre = nombre;
            this.apellido = apellido;
        }
        public static int ValidarDNI(int dni) // Arreglar Validar DNI
        {
            return Usuario.ValidarDNI(dni.ToString());
        }
        public static int ValidarDNI(string strDni)
        {
            if (!int.TryParse(strDni, out int dni))
                dni = 0;

            return dni;
        }
        public abstract bool ValidarUsuario(Usuario usuario);

        public static bool operator ==(Usuario usuario1, Usuario usuario2)
        {
            bool retorno = false;
            if (usuario1 is not null && usuario2 is not null)
                retorno = usuario1.Dni == usuario2.Dni;
            else if (usuario1 is null && usuario2 is null)
                retorno = true;
            return retorno;
        }

        public override bool Equals(Object usuario)
        {
            return this == (Usuario)usuario;
        }

        public static bool operator !=(Usuario usuario1, Usuario usuario2)
        {
            return !(usuario1 == usuario2);
        }
    }
}
