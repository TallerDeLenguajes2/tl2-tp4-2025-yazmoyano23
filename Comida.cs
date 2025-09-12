namespace tl2_tp4_2025_yazmoyano23.Controllers;

public class Comida
{
    private string? nombre { get; set; }
    private double precio { get; set; }

    public Comida()
    {
        this.nombre = "pizza";
        this.precio = 8000;
    }

    public string? GetNombre()
    {
        return this.nombre;
    }

    public double GetPrecio()
    {
        return this.precio;
    }
}