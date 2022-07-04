namespace Library;

/// <summary>
/// Error cuando dos barcos están superpuestos.
/// </summary>
public class BarcosSuperpuestos : Exception
{
    /// <summary>
    /// Uno de los barcos que colisionan
    /// </summary>
    public Barco Primero { get; }

    /// <summary>
    /// Uno de los barcos que colisionan
    /// </summary>
    public Barco Segundo { get; }

    public BarcosSuperpuestos(Barco a, Barco b)
    {
        Primero = a;
        Segundo = b;
    }
}
