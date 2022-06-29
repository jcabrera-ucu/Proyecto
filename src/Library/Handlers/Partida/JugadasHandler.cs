namespace Library;

public class JugadasHandler : BaseHandler
{
    public JugadasHandler(BaseHandler? next)
        : base(next)
    {
        this.Keywords = new string[] {
                "jugadas",
                "j",
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

        if (message.Partida == null)
        {
            response = "No hay ninguna partida activa";
            return true;
        }

        var mensajes = new List<string>()
        {
            $"{message.Partida.MostrarJugadas(message.Usuario.Id)}",
        };

        mensajes.AddRange(MensajesDePartida.Mensajes(message.Usuario, message.Partida));
        response = String.Join('\n', mensajes);
        return true;
    }
}
