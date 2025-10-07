using espacioCadete;
using espacioPedido;
using System.Threading.Channels;
using System.Data.Common;
using System.Text.Json;

namespace espacioCadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private string telefono;
        private List<Cadete> listadoCadetes;
        private List<Pedido> listadoPedidos = new List<Pedido>();
        
        const double jornal = 500;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        public Cadeteria() { }
        public Cadeteria(string nombre, string tel)
        {
            this.Nombre = nombre;
            this.Telefono = tel;
            this.ListadoCadetes = new List<Cadete>();
            this.ListadoPedidos = new List<Pedido>();
        }

        public override string ToString()
        {
            return $"Nombre: {this.nombre}, Telefono: {this.telefono}";
        }

        public List<Pedido> GetPedidos()
        {
            return new List<Pedido>(ListadoPedidos); // devuelve una copia de la lista completa
        }

        public List<Cadete> GetCadetes()
        {
            return new List<Cadete>(ListadoCadetes); // devuelve la lista completa
        }
        public void AgregarCadete(int id, string nombre, string dir, string tel)
        {
            Cadete nuevoCadete = new Cadete(id,nombre,dir,tel);
            ListadoCadetes.Add(nuevoCadete);
        }
        
        public void SetCadetes(List<Cadete> listado)
        {
            this.ListadoCadetes = listado;
        }

        public void SetPedidos(List<Pedido> listado)
        {
            this.ListadoPedidos = listado;
        }

        public int TomarPedido(string nombre, string direccion, string telefono, string referenciaDireccion, string observacion)
        {
            int numero;

            if (listadoPedidos.Count() == 0)
            {
                numero = 1;
            }
            else
            {
                numero = listadoPedidos.Last().Id + 1;
            }

            listadoPedidos.Add(new Pedido(numero, observacion,nombre, direccion, telefono,referenciaDireccion));
            return numero;
        }

        public bool AsignarPedidoACadete(int idCadete, int idPedido)
        {
            // Buscar pedido con FirstOrDefault (devuelve null si no existe)
            var pedidoAsignar = ListadoPedidos.FirstOrDefault(p => p.Id == idPedido); //Obtengo el pedido a asignar
            if (pedidoAsignar == null)
            {
                return false;
            }

            // Buscar cadete con LINQ que se va a encargar del pedido
            var cadeteAsignado = ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);
            if (cadeteAsignado == null)
            {
                return false;
            }

            // Asigno el cadete al pedido
            pedidoAsignar.SetCadete(cadeteAsignado);
            return true;
        }
        public bool CambiarEstadoDePedidoPorId(int idPedido, int numeroEstado)
        {
            var pedido = ListadoPedidos.FirstOrDefault(p => p.Id == idPedido);
            if (pedido == null)
            {
                return false;
            }

                // Validar si el número corresponde a un valor del enum
            if (!Enum.IsDefined(typeof(Estado), numeroEstado))
            {
                return false; // El número no corresponde a un estado válido
            }

            pedido.EstadoPedido = (Estado)numeroEstado;
            return true;
        }

        public bool ReasignarPedidoACadete(int idPedido, int idNuevoCadete)
        {

            //Busco en la lista de pedidos
            Pedido pedidoReasignar = ListadoPedidos.FirstOrDefault(p => p.Id == idPedido);
            if (pedidoReasignar == null)
            {
                return false;
            }

            Cadete nuevoCadete = ListadoCadetes.FirstOrDefault(c => c.Id == idNuevoCadete);
            if (nuevoCadete == null)
            {
                return false;
            }

            pedidoReasignar.SetCadete(nuevoCadete);

            return true;
        }

        public double JornalACobrar(int idCadete)
        {
            //buscar dentro de pedidos, y sumar los que cumplan la condicion del id y que su estado sea entregado
            int cantidad = ListadoPedidos.Count(p => p.GetIdCadete() == idCadete && p.EstadoPedido == Estado.Entregado);
            return jornal * cantidad;
            
        }

 /*       public void ListarPedidosSinAsignar()
        {
            var sinAsignar = ListadoPedidos.Where(p => p.EstadoPedido == Estado.SinAsignar);
            foreach (var pedido in sinAsignar)
            {
                pedido.InfoPedido();
            }
        }*/

        public void GenerarInforme()
        {

            Console.WriteLine("=== Informe de Pedidos ===");

            int cantTotalEnvios = 0;
            double totalJornales = 0;

            foreach (var cadete in ListadoCadetes)
            {
                int cantidad = ListadoPedidos.Count(p =>
                    p.Id == cadete.Id &&
                    p.EstadoPedido == Estado.Entregado);

                double monto = cantidad * jornal;

                Console.WriteLine($"Cadete: {cadete.Nombre} | Pedidos entregados: {cantidad} | Jornal: ${monto}");

                cantTotalEnvios += cantidad;
                totalJornales += monto;
            }
            double promedioEnvios = ListadoCadetes.Count > 0 ? cantTotalEnvios / ListadoCadetes.Count : 0;


            Console.WriteLine($"\nTotal de pedidos: {cantTotalEnvios}");
            Console.WriteLine($"Total ganado: ${totalJornales}");
            Console.WriteLine($"Promedio de pedidos por cadete: {promedioEnvios}");
        }
    }
}