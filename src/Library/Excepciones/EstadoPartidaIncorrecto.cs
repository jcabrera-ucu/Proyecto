namespace Library;

/// <summary>
/// El estado actual de la partida no es el esperado
/// </summary>
public class EstadoPartidaIncorrecto : Exception
{
    /// <summary>
    /// Los estados esperados
    /// </summary>
    public EstadoPartida[] Esperados { get; }

    /// <summary>
    /// El estado actual
    /// </summary>
    public EstadoPartida Encontrado { get; }

    public EstadoPartidaIncorrecto(EstadoPartida esperado, EstadoPartida encontrado)
    {
        Esperados = new EstadoPartida[] { esperado };
        Encontrado = encontrado;
    }

    public EstadoPartidaIncorrecto(EstadoPartida[] esperados, EstadoPartida encontrado)
    {
        Esperados = esperados;
        Encontrado = encontrado;
    }
}
