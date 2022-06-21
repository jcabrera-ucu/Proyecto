namespace Library;

public class MenuHandler : BaseHandler
{
    public MenuHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[]
        {
                "menu",
                "menú",
                "comandos",
                "ayuda",
        };
    }

    protected override bool InternalHandle(Message message, out string response)
    {
        if (this.CanHandle(message))
        {
            if (message.Partida == null)
            {
                var mensajes = new string[]
                {
                    "Puede utilizar los siguientes comandos:",
                    ">> estadísticas",
                    ">> buscar partida",
                    ">> buscar partida con reloj",
                    ">> jugar con bot",
                };

                response = String.Join('\n', mensajes);
                return true;
            }
            else
            {
                var mensajes = new string[]
                {
                    "Puede utilizar los siguientes comandos:",
                    ">> atacar <coordenada> (e.j: atacar A8)",
                    ">> radar <coordenada> (e.j: radar C1)",
                };


                response = String.Join('\n', mensajes);
                return true;
            }
        }

        response = string.Empty;
        return false;
    }
}
