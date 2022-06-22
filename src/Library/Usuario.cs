namespace Library;
// Esta clase cumple con SRP, su unica responsabilidad es mantener ciertos atributos de los usuarios.
/// <summary>
/// Información de usuario
/// </summary>
public class Usuario
{
    /// <summary>
    /// Identificador único del usuario (se va a usar en Jugador también)
    /// </summary>
    public Ident Id { get; set; }

    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string Nombre { get; set; } = String.Empty;

    /// <summary>
    /// Las estadísticas de juego de este usuario
    /// </summary>
    public Estadistica Estadisticas { get; set; } = new();
}
