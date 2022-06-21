namespace Library;

public class InicioHandler : BaseHandler
{
    public InicioHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[] { "/start" };
    }

    protected override bool InternalHandle(Message message, out string response)
    {
        if (this.CanHandle(message))
        {
            var mensajes = new string[]
            {
                "Bienvenido al bot de Batalla Naval del Equipo 3",
                "Para comenzar escriba: Menu",
            };

            response = String.Join('\n', mensajes);
            return true;
        }

        response = string.Empty;
        return false;
    }
}
