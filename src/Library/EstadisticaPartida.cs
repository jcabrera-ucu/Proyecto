namespace Library;

/// <summary>
/// Almacena los datos de aciertos y fallos durante una partida.
/// </summary>
public class EstadisticaPartida
{
    /// <summary>
    /// Cantidad de ataques exitosos
    /// </summary>
    public int Aciertos { get; private set; } = 0;

    /// <summary>
    /// Número de ataques que no golpean ningún barco
    /// </summary>
    public int Fallos { get; private set; } = 0;

    /// <summary>
    /// Incrementa en uno la cantidad de aciertos
    /// </summary>
    public void IncAciertos()
    {
        Aciertos++;
    }

    /// <summary>
    /// Incrementa en uno la cantidad de fallos
    /// </summary>
    public void IncFallos()
    {
        Fallos++;
    }
}
