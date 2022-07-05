namespace Library;

/// <summary>
/// </summary>
public class BarcoLargoIncorrecto : Exception
{
    public int Largo { get; }

    public Coord Primera { get; }

    public Coord Segunda { get; }

    public BarcoLargoIncorrecto(Coord a, Coord b)
    {
        Primera = a;
        Segunda = b;
        Largo = Coord.Largo(a, b);
    }
}

