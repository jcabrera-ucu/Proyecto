namespace Library;

public class NullHandler : BaseHandler
{
    public NullHandler() : base(null)
    {
    }

    protected override bool InternalHandle(Message message, out string response, out string response2)
    {
        response2 = string.Empty;
        var mensajes = new List<string>();

        if (message.Partida != null)
        {
            mensajes.AddRange(MensajesDePartida.Mensajes(message.Usuario, message.Partida));
        }
        else
        {
            mensajes.Add("¡Comando no reconocido!");
        }
        response = String.Join('\n', mensajes);
        return true;
    }
}
