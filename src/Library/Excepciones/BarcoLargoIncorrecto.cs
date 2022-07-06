namespace Library;

/// <summary>
/// Se intentó agregar un barco de un largo que no es soportado
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

