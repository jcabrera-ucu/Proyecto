namespace Library;

public class EstadisticasHandler : BaseHandler
{
    public EstadisticasHandler(BaseHandler next) : base(next)
    {
        this.Keywords = new string[]
        {
            "estadisticas",
            "estadísticas",
            "stats",
        };
    }

    protected override bool InternalHandle(Message message, out string response)
    {
        if (this.CanHandle(message))
        {
            var mensajes = new string[]
            {
                $"Estadísticas:",
                $">> Ha ganado: {message.Usuario.Estadisticas.Victorias} veces",
                $">> Ha perdido: {message.Usuario.Estadisticas.Derrotas} veces",
                $">> Ha acertado: {message.Usuario.Estadisticas.Aciertos} veces",
                $">> Ha fallado: {message.Usuario.Estadisticas.Fallos} veces",
                $">> Ha hundido: {message.Usuario.Estadisticas.Hundidos} barcos",
                $">> Ha usado: {message.Usuario.Estadisticas.Radares} radares",
            };

            response = String.Join('\n', mensajes);
            return true;
        }

        response = string.Empty;
        return false;
    }
}
