namespace Library;

/// <summary>
/// Los distintos estados posibles de la Batalla Naval.
/// </summary>
public enum EstadoPartida
{
    /// <summary>Los jugadors deben configurar sus barcos</summary>
    Configuración,

    /// <summary>Es el turno del Jugador A</summary>
    TurnoJugadorA,

    /// <summary>Es el turno del Jugador B</summary>
    TurnoJugadorB,

    /// <summary>La partida ha terminado por hundimiento de barcos</summary>
    Terminado,

    /// <summary>La partida ha terminado por tiempo</summary>
    TerminadoPorReloj,
}
