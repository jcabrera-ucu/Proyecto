namespace Library;

/// <summary>
/// Par de coordenadas no alineadas.
/// </summary>
public class CoordenadasNoAlineadas : Exception
{
    /// <summary>
    /// La primera coordenada del par
    /// </summary>
    public Coord Primera { get; }

    /// <summary>
    /// La segunda coordenada del par
    /// </summary>
    public Coord Segunda { get; }

    public CoordenadasNoAlineadas(Coord primera, Coord segunda)
    {
        Primera = primera;
        Segunda = segunda;
    }
}
