namespace Library;

/// <summary>
/// Error cuando dos barcos están superpuestos.
/// </summary>
public class BarcosSuperpuestosException : Exception
{
    /// <summary>
    /// Uno de los barcos que colisionan
    /// </summary>
    public Barco Primero { get; }

    /// <summary>
    /// Uno de los barcos que colisionan
    /// </summary>
    public Barco Segundo { get; }

    public BarcosSuperpuestosException(Barco a, Barco b)
    {
        Primero = a;
        Segundo = b;
    }
}
