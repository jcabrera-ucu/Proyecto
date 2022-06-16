namespace Library;

/// <summary>
/// </summary>
public class EstadoPartidaIncorrecto : Exception
{
    /// <summary>
    /// </summary>
    public EstadoPartida Esperado { get; }

    /// <summary>
    /// </summary>
    public EstadoPartida Encontrado { get; }

    public EstadoPartidaIncorrecto(EstadoPartida esperado, EstadoPartida encontrado)
    {
        Esperado = esperado;
        Encontrado = encontrado;
    }
}
