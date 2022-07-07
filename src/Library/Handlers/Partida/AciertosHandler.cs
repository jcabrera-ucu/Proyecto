namespace Library;

public class AciertosHandler : BaseHandler
{
        public GestorPartidas GestorPartidas { get; }

    public AciertosHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
            "Aciertos",
            "Tocados",
            "Ac",
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
            $"En esta partida se han hecho {partida.AciertosFallos.Aciertos} ataques que han resultado en un acierto"; 

        oponente = string.Empty;

        return true;
    }
}