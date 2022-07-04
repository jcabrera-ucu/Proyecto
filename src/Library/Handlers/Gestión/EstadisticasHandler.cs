namespace Library;

public class EstadisticasHandler : BaseHandler
{
    public HistóricoEstadísticas Estadísticas { get; set; }

    public EstadisticasHandler(HistóricoEstadísticas estadísticas, BaseHandler next) : base(next)
    {
        this.Estadísticas = estadísticas;
        this.Keywords = new string[]
        {
            "estadisticas",
            "estadistica",
            "estadísticas",
            "estadística",
            "stats",
            "stat",
        };
    }

    protected override bool InternalHandle(Message message, out string remitente, out string oponente)
    {
        if (!this.CanHandle(message))
        {
            oponente = string.Empty;
            remitente = string.Empty;

            return false;
        }

        var stats = Estadísticas.ObtenerEstadística(message.IdJugador);

        remitente =
            $"Estadísticas:\n" +
            $">> Ha ganado:   {stats.Victorias} veces\n" +
            $">> Ha perdido:  {stats.Derrotas} veces\n" +
            $">> Ha acertado: {stats.Aciertos} veces\n" +
            $">> Ha fallado:  {stats.Fallos} veces\n" +
            $">> Ha hundido:  {stats.Hundidos} barcos\n" +
            $">> Ha usado:    {stats.Radares} radares";

        oponente = string.Empty;

        return true;
    }
}
