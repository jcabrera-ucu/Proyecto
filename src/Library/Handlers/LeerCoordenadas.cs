namespace Library;

public class LeerCoordenadas
{
    public static List<Coord> Leer(string mensaje)
    {
        var partes = mensaje.Split(' ');
        var coords = new List<Coord>();
        for (int i = 1; i < partes.Count(); i++)
        {
            coords.Add(new Coord(partes[i]));
        }
        return coords;
    }
}