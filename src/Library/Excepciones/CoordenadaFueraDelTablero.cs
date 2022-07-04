namespace Library;

public class CoordenadaFueraDelTablero : Exception
{
    public Coord Coordenada { get; }

    public Tablero Tablero { get; }

    public CoordenadaFueraDelTablero(Coord coord, Tablero tablero)
    {
        Coordenada = coord;
        Tablero = tablero;
    }
}
