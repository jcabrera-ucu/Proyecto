namespace Library;

public class MenuHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; set; }

    public MenuHandler(GestorPartidas gestorPartidas, BaseHandler next) : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[]
        {
                "menu",
                "menú",
                "comandos",
                "ayuda",
        };
    }

    protected override bool InternalHandle(Message message, out string remitente, out string oponente)
    {
        if (!this.CanHandle(message))
        {
            oponente = string.Empty;
            remitente = string.Empty;

            return false;
        }

        remitente =
            $"Puede utilizar los siguientes comandos:\n" +
            $" >> menu\n" +
            $" >> estadisticas\n" +
            $"    stats\n";

        var partida = GestorPartidas.ObtenerPartida(message.IdJugador);

        if (partida == null
            || partida.Estado == EstadoPartida.Terminado
            || partida.Estado == EstadoPartida.TerminadoPorReloj)
        {
            remitente +=
                $" >> buscar partida\n" +
                $"    buscar\n" +
                $" >> buscar partida con reloj\n" +
                $"    buscar con reloj\n" +
                $" >> jugar con bot\n";
        }

        if (partida != null)
        {
            remitente +=
                $" >> tablero\n" +
                $"    t\n" +
                $" >> jugadas\n" +
                $"    j\n";

            switch (partida.Estado)
            {
                case EstadoPartida.Configuración:
                    remitente +=
                        $" >> agregar <coordenada-1> <coordenada-2> (e.j: agregar a1 a2)\n" +
                        $"    ag <coordenada-1> <coordenada-2> (e.j: ag c3 e3)\n";
                    break;
                case EstadoPartida.TurnoJugadorA:
                case EstadoPartida.TurnoJugadorB:
                    {
                        var jugador = partida.ObtenerJugadorPorId(message.IdJugador);
                        if (jugador != null)
                        {
                            if (jugador.Reloj != null)
                            {
                                remitente +=
                                    $" >> reloj\n";
                            }

                            if (partida.JugadorActual != null && partida.JugadorActual.Id == message.IdJugador)
                            {
                                remitente +=
                                    $" >> atacar <coordenada> (e.j: atacar a1)\n" +
                                    $"    a <coordenada> (e.j: a f4)\n" +
                                    $" >> radar <coordenada> (e.j: radar a1)\n" +
                                    $"    r <coordenada> (e.j: r f4)\n";
                            }
                        }
                    }
                    break;
                case EstadoPartida.Terminado:
                case EstadoPartida.TerminadoPorReloj:
                default:
                    break;
            }
        }

        oponente = String.Empty;

        return true;
    }
}
