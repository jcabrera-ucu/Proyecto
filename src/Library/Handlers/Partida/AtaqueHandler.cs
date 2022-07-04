namespace Library;

public class AtaqueHandler : BasePrefijoHandler
{
    public GestorPartidas GestorPartidas { get; }

    public AtaqueHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
            "atacar",
            "at",
            "a",
        };
    }

    protected override bool InternalHandle(Message message, out string remitente, out string oponente)
    {
        if (!CanHandle(message))
        {
            remitente = string.Empty;
            oponente = string.Empty;

            return false;
        }

        var partida = GestorPartidas.ObtenerPartida(message.IdJugador);

        if (partida == null)
        {
            remitente = "No hay ninguna partida activa";
            oponente = String.Empty;

            return true;
        }

        var coords = LeerCoordenadas.Leer(message.Text);

        if (coords.Count != 1)
        {
            remitente = "Se esperaba una coordenada: atacar <coordenada>";
            oponente = string.Empty;

            return true;
        }

        var resultado = partida.HacerJugada(
            new Jugada(
                message.IdJugador,
                TipoJugada.Ataque,
                coords[0]
            )
        );

        remitente =
            $"{coords[0].ToAlfanumérico()} ¡{resultado}!\n" +
            $"{partida.MostrarJugadas(message.IdJugador)}\n";

        oponente =
            $"Te atacaron en: {coords[0].ToAlfanumérico()} ¡{resultado}!\n" +
            $"Éstas son tus jugadas hasta el momento:\n" +
            $"{partida.MostrarJugadasDelOponente(message.IdJugador)}\n";

        switch (partida.Estado)
        {
            case EstadoPartida.Terminado:
                {
                    var mensajeVictoria = "¡Ganaste!\nTomate una coquita";
                    var mensajeDerrota = "¡Perdiste!\nTomate una coquita";

                    if (partida.Ganador != null && partida.Ganador.Id == message.IdJugador)
                    {
                        remitente += mensajeVictoria;
                        oponente += mensajeDerrota;
                    }
                    else
                    {
                        remitente += mensajeDerrota;
                        oponente += mensajeVictoria;
                    }
                }
                break;
            case EstadoPartida.TerminadoPorReloj:
                {
                    var mensajeVictoria = "¡Ganaste!\nTu oponente se quedó sin tiempo\nTomate una coquita";
                    var mensajeDerrota = "¡Perdiste!\nTe quedaste sin tiempo\nTomate una coquita";

                    if (partida.Ganador != null && partida.Ganador.Id == message.IdJugador)
                    {
                        remitente += mensajeVictoria;
                        oponente += mensajeDerrota;
                    }
                    else
                    {
                        remitente += mensajeDerrota;
                        oponente += mensajeVictoria;
                    }
                }
                break;
            default:
                remitente += "Es el turno de tu oponente";
                oponente += "¡Es tu turno!";
                break;
        }

        return true;
    }
}
