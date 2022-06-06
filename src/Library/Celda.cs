namespace Library;

/// <summary>
/// Representación del estado de una celda de un tablero.
/// </summary>
public enum Celda
{
    /// <summary>La celda está vacía y no fue atacada</summary>
    Vacio,
    /// <summary>La celda contiene un barco</summary>
    Barco,
    /// <summary>La celda contiene un barco y fue atacado en esa coordenada</summary>
    Tocado,
    /// <summary>La celda contiene un barco y fue revelado por un radar</summary>
    Revelado,
    /// <summary>La celda está vacía y fue atacada</summary>
    Agua,
}
