using System.Text;

namespace Library;

/// <summary>
/// Controlador del juego.
/// </summary>
public class ControladorJuego
{  
    /// <summary>
    /// Primer jugador.
    /// </summary>
    /// <value></value>
    public Jugador jugador1 { get; }
    /// <summary>
    /// Segundo jugador.
    /// </summary>
    /// <value></value>
    public Jugador jugador2 { get; }

    /// <summary>
    /// Define de quién es el turno.
    /// </summary>
    /// <value></value>
    public Turno turnoActual { get; }
    //comentar
    TimeSpan relojJugadorA = new TimeSpan();
    //comentar
    TimeSpan relojJugadorB = new TimeSpan();

    public ControladorJuego(Jugador jugadorA, Jugador jugadorB, Turno turnoActual)
    {
        jugador1 = jugadorA;
        jugador2 = jugadorB;
        this.turnoActual = turnoActual;
    }

    public ResultadoAtaque Atacar(string idJugador, Coord coord)
    {
        
    }
    
}

