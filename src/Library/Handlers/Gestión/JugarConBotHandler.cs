namespace Library;

public class JugarConBotHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; }

    public GestorBots GestorBots { get; }

    public JugarConBotHandler(GestorPartidas partidas, GestorBots bots, BaseHandler next) : base(next)
    {
        this.GestorPartidas = partidas;
        this.GestorBots = bots;
        this.Keywords = new string[]
        {
            "jugar con bot",
            "jugar",
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

        var partida = GestorPartidas.ObtenerPartida(message.IdJugador);

        if (partida != null)
        {
            if (partida.SigueEnJuego)
            {
                remitente = "Ya está en una partida";
                oponente = string.Empty;

                return true;
            }
            else
            {
                GestorPartidas.EliminarPartida(partida);
            }
        }

        var idBot = new Ident();

        partida = GestorPartidas.NuevaPartida(
            new GestorPartidas.Usuario
            {
                Id = message.IdJugador,
                Nombre = message.Nombre,
            },
            new GestorPartidas.Usuario
            {
                Id = idBot,
                Nombre = "Robotina",
            }
        );

        var bot = GestorBots.Nuevo(idBot, partida);

        string mensajeComun =
            $"Que comience la batalla\n" +
            $"Debe agregar barcos de los siguientes tamaños:\n" +
            $" => ({String.Join(", ", partida.BarcosFaltantes(message.IdJugador))})\n" +
            $"Para agregar un barco utilice el siguiente comando:\n" +
            $" >> agregar <coordenada-1> <coordenada-2> (e.j: agregar a1 a2)\n" +
            $"    ag <coordenada-1> <coordenada-2> (e.j: ag c3 e3)\n" +
            partida.MostrarTablero(message.IdJugador);

        if (!String.IsNullOrEmpty(bot.Nombre))
        {
            remitente =
                $"Tu oponente es: {bot.Nombre}\n{mensajeComun}";
        }
        else
        {
            remitente = mensajeComun;
        }

        oponente = String.Empty;

        return true;
    }
}
