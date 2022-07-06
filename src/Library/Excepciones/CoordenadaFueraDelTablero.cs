namespace Library;

/// <summary>
/// Se intentó utilizar una coordenada que está fuera del tablero
/// </summary>
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
