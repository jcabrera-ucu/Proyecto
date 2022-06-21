namespace Library;

/// <summary>
///
/// </summary>
public class JugadorIncorrecto : Exception
{
    public Ident Identificador { get; }

    public JugadorIncorrecto(Ident id)
    {
        Identificador = id;
    }
}
