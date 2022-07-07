namespace Library;

public class FallosHandler : BaseHandler
{
        public GestorPartidas GestorPartidas { get; }

    public FallosHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
            "Fallos",
            "Aguas",
            "F",
        };
    }
    protected override bool InternalHandle(Message message, out string remitente, out string oponente)
    {
        if (!this.CanHandle(message))
        {
            oponente = String.Empty;
            remitente = String.Empty;

            return false;
        }

        var partida = GestorPartidas.ObtenerPartida(message.IdJugador);

        if (partida == null)
        {
            remitente = "No hay ninguna partida activa";
            oponente = String.Empty;

            return true;
        }
        remitente =
            $"En esta partida se han hecho {partida.AciertosFallos.Fallos} ataques que han resultado en agua"; 

        oponente = string.Empty;

        return true;
    }
}