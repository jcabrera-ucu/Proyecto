namespace Library;

public class RadarHandler : BasePrefijoHandler
{
    public GestorPartidas GestorPartidas { get; set; }

    public RadarHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
                "radar",
                "r",
            };
    }

    protected override bool InternalHandle(Message message, out string remitente, out string oponente)
    {
        oponente = string.Empty;
        if (!CanHandle(message))
        {
            remitente = string.Empty;
            return false;
        }

        var partida = GestorPartidas.ObtenerPartida(message.IdJugador);

        if (partida == null)
        {
            remitente = "No hay ninguna partida activa";
            return true;
        }

        var coords = LeerCoordenadas.Leer(message.Text);

        if (coords.Count != 1)
        {
            remitente = "Se esperaba una coordenada";
            return true;
        }

        var resultado = partida.HacerJugada(new Jugada(
            message.IdJugador,
            TipoJugada.Radar,
            coords[0]
        ));

        remitente =
            $"{coords[0].ToAlfanumérico()} ¡Radar desplegado!\n" +
            $"{partida.MostrarJugadas(message.IdJugador)}\n" +
            $"Es el turno de tu oponente";

        oponente =
            $"Desplegaron un radar en: {coords[0].ToAlfanumérico()}\n" +
            $"Estos son tus barcos:\n" +
            $"{partida.MostrarTableroDelOponente(message.IdJugador)}\n" +
            $"Éstas son tus jugadas hasta el momento:\n" +
            $"{partida.MostrarJugadasDelOponente(message.IdJugador)}\n" +
            $"¡Es tu turno!";

        return true;
    }
}
