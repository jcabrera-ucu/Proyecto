namespace Library;

public class TableroHandler : BaseHandler
{
    public TableroHandler(BaseHandler? next)
        : base(next)
    {
        this.Keywords = new string[] {
                "tablero",
                "t",
                "barcos",
                "b",
            };
    }

    protected override bool InternalHandle(Message message, out string response)
    {
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
            message.Partida.MostrarTablero(message.Usuario.Id)
        };

        mensajes.AddRange(MensajesDePartida.Mensajes(message.Usuario, message.Partida));
        response = String.Join('\n', mensajes);
        return true;
    }
}
