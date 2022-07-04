namespace Library;

public class TableroHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; }

    public TableroHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
                "tablero",
                "t",
                "barcos",
                "b",
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
            oponente = string.Empty;

            return true;
        }

        remitente = partida.MostrarTablero(message.IdJugador);
        oponente = string.Empty;

        return true;
    }
}
