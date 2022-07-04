namespace Library;

/// <summary>
/// Se intentó utilizar un par de instancias de Jugador cuyos tableros
/// no son compatibles. (o sea, sus tableros no son del mismo tamaño)
/// </summary>
public class JugadoresIncompatibles : Exception
{
    public Jugador JugadorA { get; }

    public Jugador JugadorB { get; }

    public JugadoresIncompatibles(Jugador a, Jugador b)
    {
        JugadorA = a;
        JugadorB = b;
    }
}
