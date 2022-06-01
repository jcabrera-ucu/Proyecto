namespace Library;

/// <summary>
/// Representa una coordenada (x, y) de un tablero.
/// </summary>
/// <remarks>
/// Una coordenada es válida si ambas componentes (x e y) con mayores que cero
/// y menores que Coord.Alfabeto.Count.
/// </remarks>
public record struct Coord
{
    /// <summary>
    /// La componente X (horizontal) de la coordenada.
    /// </summary>
    public int X { get; }

    /// <summary>
    /// La componente Y (vertical) de la coordenada.
    /// </summary>
    public int Y { get; }

    /// <summary>
    ///
    /// </summary>
    public static List<char> Alfabeto { get; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();

    /// <summary>
    /// Construye una coordenada
    /// </summary>
    /// <param name="x">La componente horizontal</param>
    /// <param name="y">La componente vertical</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Si x es menor que 0</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Si x es mayor o igual a Alfabeto.Count</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Si y es menor que 0</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Si y es mayor o igual a Alfabeto.Count</exception>
    public Coord(int x, int y)
    {
        if (x < 0 || x >= Alfabeto.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }

        if (y < 0 || y >= Alfabeto.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(y));
        }

        X = x;
        Y = y;
    }

    /// <summary>
    /// Convierte la coordenada a su representación alfanumérica
    /// </summary>
    /// <returns>Una versión legible de la coordenada (e.j: F13)</returns>
    public string ToAlfanumérico()
    {
        return String.Format("{0}{1:00}", Alfabeto[X], Y + 1);
    }
}