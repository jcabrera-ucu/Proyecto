namespace Library;

public class RelojHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; }

    public RelojHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
                "reloj",
            };
    }

    protected override bool InternalHandle(Message message, out string response, out string response2)
    {
        response2 = string.Empty;
        if (!CanHandle(message))
        {
            response = string.Empty;
            return false;
        }

        var partida = GestorPartidas.ObtenerPartida(message.IdJugador);

        if (partida == null)
        {
            response = "No hay ninguna partida activa";
            return true;
        }

        var jugador = partida.ObtenerJugadorPorId(message.IdJugador);
        if (jugador == null)
        {
            throw new JugadorIncorrecto(message.IdJugador);
        }

        if (jugador.Reloj == null)
        {
            response = "No está en una partida con reloj";
        }
        else
        {
            var minutos = jugador.Reloj.TiempoRestante.Minutes;
            var segundos = jugador.Reloj.TiempoRestante.Seconds;
            response = $"Reloj: {minutos} minutos, {segundos} segundos";
        }

        return true;
    }
}
