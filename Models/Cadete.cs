using System.Data.Common;
using espacioPedido;

namespace espacioCadete
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;

        public Cadete() { }

        public Cadete(int id, string nombre, string dir, string tel){
            this.Id = id;
            this.Nombre = nombre;
            this.Direccion = dir;
            this.Telefono = tel;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public override string ToString(){
            return $"ID: {this.id}, Nombre: {this.nombre}, Direccion: {this.direccion}, Telefono: {this.telefono}";
        }
    }

}