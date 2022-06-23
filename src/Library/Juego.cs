namespace Library;

/// <summary>
///
/// </summary>
public class Juego
{
    /// <summary>
    ///
    /// </summary>
    public HistóricoEstadísticas Estadísticas { get; }

    /// <summary>
    ///
    /// </summary>
    public GestorPartidas GestorPartidas { get; }

    /// <summary>
    ///
    /// </summary>
    public Juego()
    {
        Estadísticas = new();
        GestorPartidas = new();
    }

    public string ProcesarMensaje(Ident id, string nombre, string texto)
    {
        var chain =
            new InicioHandler(
            new MenuHandler(
            new BuscarPartidaHandler(GestorPartidas,
            new BuscarPartidaConRelojHandler(GestorPartidas,
            new EstadisticasHandler(
            new AtaqueHandler(
            new ConfiguracionHandler(
            new JugadasHandler(
            new RadarHandler(
            new TableroHandler(
            new NullHandler()))))))))));

        var usuario = new Usuario
        {
            Id = id,
            Nombre = nombre,
            Estadisticas = Estadísticas.ObtenerEstadística(id),
        };

        var mensaje = new Message
        {
            Text = texto,
            Partida = GestorPartidas.ObtenerPartida(usuario),
            Usuario = usuario,
        };

        string response;
        chain.Handle(mensaje, out response);

        return response;
    }
}
