namespace Library;

public class BuscarPartidaHandler : BaseHandler
{
    public GestorPartidas GestorPartidas { get; }

    public BuscarPartidaHandler(GestorPartidas partidas, BaseHandler next) : base(next)
    {
        this.GestorPartidas = partidas;
        this.Keywords = new string[]
        {
            "buscar partida",
            "buscar",
        };
    }

    protected override bool InternalHandle(Message message, out string response, out string response2)
    {
        response2 = string.Empty;
        if (this.CanHandle(message))
        {
            if (message.Partida != null)
            {
                response = "Ya está en una partida";
                return true;
            }

            var partida = GestorPartidas.BuscarNuevaPartida(message.Usuario, conReloj: false);

            if (partida == null)
            {
                response = "Esperando un oponente";
                return true;
            }
            else
            {
                var mensajes = new List<string>()
                {
                    "¡Que comience la batalla!",
                };

                mensajes.AddRange(MensajesDePartida.Mensajes(message.Usuario, partida));

                response = String.Join('\n', mensajes);
                response2 = response;
                return true;
            }
        }

        response = string.Empty;
        return false;
    }
}
