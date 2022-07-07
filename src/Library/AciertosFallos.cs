namespace Library;

// Esta clase cumple con SRP ya que su unica responsabilidad es mantener
// registro de las estadisticas de los distintos usuarios.

/// <summary>
/// Estadísticas globales de un jugador
/// </summary>
public class AciertosFallos
{
    /// <summary>
    /// Cantidad de ataques exitosos
    /// </summary>
    public int Aciertos { get; set; } = 0;

    /// <summary>
    /// Número de ataques que no golpean ningún barco
    /// </summary>
    public int Fallos { get; set; } = 0;

}