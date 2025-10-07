using System.Text.Json;
using System.Text.Json.Serialization;
using espacioCadete;
using espacioCadeteria;
using espacioPedido;

namespace espacioJSON
{
    //IAccesoADatos
    public class AccesoADatosJSON : IAccesoADatos
    {
        private string rutaPedidos = "DatosPedidos.json";
        private List<Pedido> pedidos = new List<Pedido>();
        public Cadeteria LeerCadeteria(string nombreArchivo)
        {
            string ruta = nombreArchivo + ".json";

            if (File.Exists(ruta))
            {

                string json = File.ReadAllText(ruta);

                var nuevaCadeteria = JsonSerializer.Deserialize<Cadeteria>(json);

                return nuevaCadeteria;
            }
            else
            {
                Console.WriteLine($"El archivo {nombreArchivo} no existe.");
                return new Cadeteria();
            }
        }


        public List<Cadete> LeerCadetes(string nombreArchivo)
        {
            string ruta = nombreArchivo + ".json"; 
            if (File.Exists(ruta))
            {
                string json = File.ReadAllText(ruta);
                List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);
                return cadetes;
            }
            else
            {
                Console.WriteLine("El archivo no existe.");
                return new List<Cadete>();
            }
        }
        
        public List<Pedido> LeerPedidos()
        {
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Ignora mayúsculas/minúsculas en nombres de propiedades
                Converters = { new JsonStringEnumConverter() } // Permite convertir strings a enums
            };
            if (File.Exists(rutaPedidos))
            {
                string jsonString = File.ReadAllText(rutaPedidos);
                pedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonString,options);
            }
            else
            {
                Guardar(pedidos);
            }
            return pedidos;
        }
        public void Guardar(List<Pedido> pedidos)
        {
            string jsonString = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaPedidos, jsonString);
        }
    }
}