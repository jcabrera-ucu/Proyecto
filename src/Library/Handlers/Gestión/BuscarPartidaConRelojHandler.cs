namespace Library;

public class BuscarPartidaConRelojHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; }

    public BuscarPartidaConRelojHandler(GestorPartidas partidas, BaseHandler next) : base(next)
    {
        this.GestorPartidas = partidas;
        this.Keywords = new string[]
        {
            "buscar partida con reloj",
            "buscar con reloj",
        };
    }

    protected override bool InternalHandle(Message message, out string response)
    {
        if (this.CanHandle(message))
        {
            if (message.Partida != null)
            {
                response = "Ya está en una partida";
                return true;
            }

            var partida = GestorPartidas.BuscarNuevaPartida(message.Usuario, conReloj: true);

            if (partida == null)
            {
                response = "Esperando un oponente";
                return true;
            }
            else
            {
                response = "¡Que comience la batalla!";
                return true;
            }
        }

        response = string.Empty;
        return false;
    }
}
