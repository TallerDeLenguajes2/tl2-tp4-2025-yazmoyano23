namespace espacioCliente
{
    public class Cliente
    {
        private string nombre;
        private string direccion;
        private string telefono;
        private string datosReferenciaDireccion;

        public Cliente() { }

        public Cliente(string nombre, string dir, string tel, string datosDir)
        {
            this.nombre = nombre;
            this.Direccion = dir;
            this.telefono = tel;
            this.datosReferenciaDireccion = datosDir;
        }


        public string Direccion { get => direccion; set => direccion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }

        public override string ToString()
        {
            return $"Nombre: {this.nombre}, Direccion: {this.Direccion}, Telefono: {this.telefono},Referencia Direccion: {this.datosReferenciaDireccion}";
        }
    }   
}
