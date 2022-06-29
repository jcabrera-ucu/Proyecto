using System;
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

    public (string remitente, string oponente, string? idOponente) ProcesarMensaje(Ident id, string nombre, string texto)
    {
        Console.WriteLine($"ID: {id.Value} {nombre} {texto}");
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

        var partida = GestorPartidas.ObtenerPartida(usuario);

        var mensaje = new Message
        {
            Text = texto,
            Partida = partida,
            Usuario = usuario,
        };

        string response;
        string response2;
        chain.Handle(mensaje, out response, out response2);
        partida = GestorPartidas.ObtenerPartida(usuario);
        Ident? hola = new Ident("a");
        if (partida != null)
        {
        Console.WriteLine("Hay Partida");
            if (partida.JugadorA.Id.Value == id.Value)
            {
                hola = null;
                hola = new Ident(partida.JugadorB.Id.Value);
            }
            else
            {
                hola = null;
                hola = new Ident(partida.JugadorA.Id.Value);
            }
        }
        Console.WriteLine($" xx  {hola.Value}    xx");
        return ($"```\n{response}\n```", $"```\n{response2}\n```", hola.Value);
    }
}
