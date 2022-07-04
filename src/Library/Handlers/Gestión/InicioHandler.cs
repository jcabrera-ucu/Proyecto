namespace Library;

public class InicioHandler : BaseHandler
{
    public InicioHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[] { "/start" };
    }

    protected override bool InternalHandle(Message message, out string remitente, out string oponente)
    {
        if (!this.CanHandle(message))
        {
            oponente = String.Empty;
            remitente = String.Empty;

            return false;
        }

        remitente =
            "Bienvenido al bot de Batalla Naval del Equipo 3\n" +
            "Para comenzar escriba: menu";

        oponente = string.Empty;

        return true;
    }
}
