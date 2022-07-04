namespace Library;

public class BuscarPartidaHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; }

    public bool ConReloj { get; }

    public BuscarPartidaHandler(GestorPartidas partidas, bool conReloj, BaseHandler next) : base(next)
    {
        this.GestorPartidas = partidas;
        this.ConReloj = conReloj;
        if (conReloj)
        {
            this.Keywords = new string[]
            {
                "buscar partida con reloj",
                "buscar con reloj",
            };
        }
        else
        {
            this.Keywords = new string[]
            {
                "buscar partida",
                "buscar",
            };
        }
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

        partida = GestorPartidas.BuscarNuevaPartida(new GestorPartidas.Usuario
        {
            Id = message.IdJugador,
            Nombre = message.Nombre,
        }, ConReloj);

        if (partida == null)
        {
            remitente = "Esperando un oponente";
            oponente = string.Empty;

            return true;
        }
        else
        {
            var jugador = partida.ObtenerJugadorPorId(message.IdJugador);
            var jugadorOponente = partida.OponenteDe(message.IdJugador);
            if (jugador == null || jugadorOponente == null)
            {
                // Esto no ocurre nunca, ya que la partida se pidió con `IdJugador`
                throw new InvalidOperationException();
            }

            string mensajeComun =
                $"Que comience la batalla\n" +
                $"Debe agregar barcos de los siguientes tamaños:\n" +
                $" => ({String.Join(", ", partida.BarcosFaltantes(jugador.Id))})\n" +
                $"Para agregar un barco utilice el siguiente comando:\n" +
                $" >> agregar <coordenada-1> <coordenada-2> (e.j: agregar a1 a2)\n" +
                $"    ag <coordenada-1> <coordenada-2> (e.j: ag c3 e3)\n" +
                partida.MostrarTablero(jugador.Id); // En este caso no importa cuál tablero
                                                    // se muestra, ya que ambos estan vacíos.

            if (!String.IsNullOrEmpty(jugadorOponente.Nombre))
            {
                remitente =
                    $"Tu oponente es: {jugadorOponente.Nombre}\n{mensajeComun}";
            }
            else
            {
                remitente = mensajeComun;
            }

            if (!String.IsNullOrEmpty(jugador.Nombre))
            {
                oponente =
                    $"Tu oponente es: {jugador.Nombre}\n{mensajeComun}";
            }
            else
            {
                oponente = mensajeComun;
            }

            return true;
        }
    }
}
