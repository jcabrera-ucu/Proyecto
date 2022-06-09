namespace Library;

/// <summary>
/// Controlador del juego, implementa la máquina de estados para
/// la batalla naval.
/// </summary>
public class ControladorJuego
{
    /// <summary>
    /// Primer jugador.
    /// </summary>
    public Jugador JugadorA { get; }

    /// <summary>
    /// Segundo jugador.
    /// </summary>
    public Jugador JugadorB { get; }

    /// <summary>
    ///
    /// </summary>
    public EstadoPartida Estado { get; private set; }

    // /// <summary>
    // ///
    // /// </summary>
    // /// <returns></returns>
    // TimeSpan relojJugadorA = new TimeSpan();

    // /// <summary>
    // ///
    // /// </summary>
    // /// <returns></returns>
    // TimeSpan relojJugadorB = new TimeSpan();

    public ControladorJuego(int ancho, int alto)
    {
        JugadorA = new Jugador("A", "JugadorA", new Tablero(ancho, alto));
        JugadorB = new Jugador("B", "JugadorB", new Tablero(ancho, alto));

        Estado = EstadoPartida.Configuración;
    }

    public Jugador? ObtenerJugadorPorId(string id)
    {
        if (JugadorA.Id == id)
        {
            return JugadorA;
        }
        else if (JugadorB.Id == id)
        {
            return JugadorB;
        }
        else
        {
            return null;
        }
    }


    /// <summary>
    ///
    /// </summary>
    public Jugador? JugadorActual
    {
        get
        {
            switch (Estado)
            {
                case EstadoPartida.TurnoJugadorA:
                    return JugadorA;
                case EstadoPartida.TurnoJugadorB:
                    return JugadorB;
                default:
                    return null;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public Jugador? OponenteActual
    {
        get
        {
            switch (Estado)
            {
                case EstadoPartida.TurnoJugadorA:
                    return JugadorB;
                case EstadoPartida.TurnoJugadorB:
                    return JugadorA;
                default:
                    return null;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <param name="coordA"></param>
    /// <param name="coordB"></param>
    public void AgregarBarco(string id, Coord coordA, Coord coordB)
    {
        if (Estado != EstadoPartida.Configuración)
        {
            throw new Exception("Hola");
        }

        var jugador = ObtenerJugadorPorId(id);
        if (jugador == null)
        {
            throw new Exception("Hola");
        }

        var longitud = Coord.Distancia(coordA, coordB);
        if (2 <= longitud && longitud <= 5)
        {
            var barcos = jugador.Tablero.BarcosConLongitud(longitud);
            if (barcos.Count == 0)
            {
                jugador.Tablero.AgregarBarco(coordA, coordB);
            }
            else
            {
                throw new Exception("hola");
            }
        }

        if (JugadorA.Tablero.Barcos.Count == 4 &&
            JugadorB.Tablero.Barcos.Count == 4)
        {
            Estado = EstadoPartida.TurnoJugadorA;
        }
    }

    private void SiguienteTurno()
    {
        switch (Estado)
        {
            case EstadoPartida.TurnoJugadorA:
                Estado = EstadoPartida.TurnoJugadorB;
                break;
            case EstadoPartida.TurnoJugadorB:
                Estado = EstadoPartida.TurnoJugadorA;
                break;
            default:
                break;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="jugada"></param>
    public ResultadoJugada HacerJugada(Jugada jugada)
    {
        if (JugadorActual == null || jugada.Id != JugadorActual.Id)
        {
            throw new Exception ("Turno equivocado");
        }
        else
        {
            switch(jugada.Tipo)
            {
                case TipoJugada.Ataque:
                    var resultadoAtaque = OponenteActual.Atacar(jugada.Coordenada);
                    var barcosRestantes = OponenteActual.Tablero.BarcosAFlote();
                    if (barcosRestantes.Count == 0)
                    {
                        Estado = EstadoPartida.Terminado;
                    }
                    SiguienteTurno();
                    switch (resultadoAtaque)
                    {
                        case ResultadoAtaque.Agua:
                            return ResultadoJugada.Agua;
                        case ResultadoAtaque.Hundido:
                            return ResultadoJugada.Hundido;
                        case ResultadoAtaque.Tocado:
                            return ResultadoJugada.Tocado;
                        default:
                            throw new Exception("Hola");
                    }
                case TipoJugada.Radar:
                    OponenteActual.Radar(jugada.Coordenada);
                    SiguienteTurno();
                    return ResultadoJugada.RadarDesplegado;
                default:
                    throw new Exception("Hola");
            }
        }
    }
}

