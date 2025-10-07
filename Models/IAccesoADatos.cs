using espacioCadete;
using espacioPedido;
using espacioCadeteria;

public interface IAccesoADatos
{
    List<Cadete> LeerCadetes(string NombreArchivo);

    Cadeteria LeerCadeteria(string NombreArchivo);
    List<Pedido> LeerPedidos();
    public void Guardar(List<Pedido> pedidos);


}