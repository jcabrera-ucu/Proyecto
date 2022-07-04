namespace Library;

public class JugadasHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; }

    public JugadasHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
                "jugadas",
                "j",
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

        remitente = partida.MostrarJugadas(message.IdJugador);
        return true;
    }
}
