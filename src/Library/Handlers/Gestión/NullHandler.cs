namespace Library;

public class NullHandler : BaseHandler
{
    public NullHandler() : base(null)
    {
    }

    protected override bool InternalHandle(Message message, out string remitente, out string oponente)
    {
        remitente = "¡Comando no reconocido!\nUtilice el comando 'menu' para ver las opciones disponibles";
        oponente = string.Empty;

        return true;
    }
}
