namespace Library;
// Esta clase cumple con SRP ya que su unica responsabilidad es mantener registro de las estadisticas de los distintos usuarios.
/// <summary>
/// Estadísticas globales de un jugador
/// </summary>
public class Estadistica
{
    /// <summary>
    /// Cantidad de victorias acumuladas
    /// </summary>
    public int Victorias { get; set; } = 0;

    /// <summary>
    /// Cantidad de derrotas acumuladas
    /// </summary>
    public int Derrotas { get; set; } = 0;

    /// <summary>
    /// Cantidad de ataques exitosos
    /// </summary>
    public int Aciertos { get; set; } = 0;

    /// <summary>
    /// Número de ataques que no golpean ningún barco
    /// </summary>
    public int Fallos { get; set; } = 0;

    /// <summary>
    /// Cantidad de total de barcos hundidos
    /// </summary>
    public int Hundidos { get; set; } = 0;

    /// <summary>
    /// Cantidad de veces que se usaron radares
    /// </summary>
    public int Radares { get; set; } = 0;
}
