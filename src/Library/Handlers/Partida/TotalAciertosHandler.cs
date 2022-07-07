namespace Library;

public class TotalAciertosHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; }

    public TotalAciertosHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
            "total aciertos",
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

        remitente = $"Total de disparos certeros: {partida.TotalAciertos()}";
        oponente = String.Empty;

        return true;
    }
}
