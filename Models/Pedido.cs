using espacioCliente;
using espacioCadete;

namespace espacioPedido
{
    public enum Estado
    {
        SinAsignar,
        Asignado,
        Entregado
    }
    public class Pedido
    {
        private int id;
        private string obs;
        private Cliente? cliente;
        private Estado estado;
        private Cadete? refCadete;

        public int Id { get => id; set => id = value; }
        public Estado EstadoPedido { get => estado; set => estado = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public Cadete RefCadete { get => refCadete; set => refCadete = value; }

        public Pedido() { }

        public Pedido(int id, string obs, string nombre, string dir, string tel, string datosDir)
        {
            this.id = id;
            this.obs = obs;
            this.cliente = new Cliente(nombre, dir, tel, datosDir);
            this.refCadete = null;
            this.estado = Estado.SinAsignar;
        }

        public override string ToString()
        {
            return $"Id:{this.id}, Observacion Pedido: {this.obs}, Estado: {this.estado}, Cliente: {this.cliente.Nombre}";
        }

        public void SetCadete(Cadete cadeteAsignado)
        {
            this.RefCadete = cadeteAsignado;
            this.estado = Estado.Asignado;
        }

        public int GetIdCadete()
        {
            return RefCadete != null ? RefCadete.Id : 0;
        }
    }

        public class PedidoRequest
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string ReferenciaDireccion { get; set; }
        public string Observacion { get; set; }
    }
}