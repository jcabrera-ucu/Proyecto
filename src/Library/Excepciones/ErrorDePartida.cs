namespace Library;

public class ErrorDePartida
{
    public Exception Exc { get; }

    public Ident? IdJugador { get; }

    public ControladorJuego? Partida { get; }

    public ErrorDePartida(Exception exc, Ident? idJugador, ControladorJuego? partida)
    {
        Exc = exc;
        IdJugador = idJugador;
        Partida = partida;
    }
}
