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
    public Jugador JugadorA { get; }
    /// <summary>
    /// Segundo jugador.
    /// </summary>
    /// <value></value>
    public Jugador JugadorB { get; }

    /// <summary>
    /// Define de quién es el turno.
    /// </summary>
    /// <value></value>
    public Turno TurnoActual { get; set; }
    //comentar
    TimeSpan relojJugadorA = new TimeSpan();
    //comentar
    TimeSpan relojJugadorB = new TimeSpan();

    public ControladorJuego(Jugador jugadorA, Jugador jugadorB, Turno turnoActual)
    {
        JugadorA = jugadorA;
        JugadorB = jugadorB;
        TurnoActual = turnoActual;
    }
    private Jugador JugadorActual()
    {
        switch (TurnoActual)
        {
            case Turno.JugadorA:
                return JugadorA;
            case Turno.JugadorB:
                return JugadorB;
        }
   
   }
    private Jugador OponenteActual()
    {
        switch (TurnoActual)
        {
            case Turno.JugadorA:
                return JugadorB;
            case Turno.JugadorB:
                return JugadorA;
        }
    }


    public void HacerJugada(Jugada jugada)
    {
        var jugadorActual = JugadorActual();
        if (jugada.Id != jugadorActual.Id)
        {
            throw new Exception ("Turno equivocado");
        }
        else
        {
            var oponenteActual = OponenteActual();
            switch(jugada.Tipo)
            {
                case TipoJugada.Ataque:
                    oponenteActual.Atacar(jugada.Coordenada);
                    break;
                case TipoJugada.Radar:
                    oponenteActual.Radar(jugada.Coordenada);
                    break;
            }  
        }
        if (TurnoActual == JugadorA)
        {
            TurnoActual = JugadorB;
        }
        else
        {
            TurnoActual = JugadorA;
        }


    }
}

