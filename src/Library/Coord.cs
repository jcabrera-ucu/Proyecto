using System.Text.RegularExpressions;

namespace Library;

/// <summary>
/// Representa una coordenada (x, y) de un tablero.
/// </summary>
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
    /// El valor mínimo para cada componente
    /// </summary>
    public static int Min { get; } = 0;

    /// <summary>
    /// El valor máximo para cada componente
    /// </summary>
    public static int Max { get; } = 25;

    // La lista (ordenada) de letras posibles para representar el
    // componente X de una Coord
    private static List<char> _alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();

    /// <summary>
    /// Construye una coordenada
    /// </summary>
    /// <param name="x">La componente horizontal</param>
    /// <param name="y">La componente vertical</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Si x es menor que Min</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Si x es mayor que Max</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Si y es menor que Min</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Si y es mayor que Max</exception>
    public Coord(int x, int y)
    {
        if (x < Min || x > Max)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }

        if (y < Min || y > Max)
        {
            throw new ArgumentOutOfRangeException(nameof(y));
        }

        X = x;
        Y = y;
    }

    /// <summary>
    /// Construye una coordenada
    /// </summary>
    /// <param name="coord">
    /// La coordenada en formato alfanumérico (e.j: B12)
    /// Formato:
    ///     {Letra}{Número}, donde {Letra} es una letra de la A a la Z y
    ///     {Número} es un número mayor o igual a 1 y menor o igual a 26
    /// </param>
    /// <exception cref="System.ArgumentException">Si 'coord' no está en formato alfanumérico</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Si Y es menor que Min</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">Si Y es mayor que Max</exception>
    public Coord(string coord)
    {
        var match = Regex.Match(coord, @"\s*([a-z])\s*(\d{1,2})", RegexOptions.IgnoreCase);

        if (!match.Success)
        {
            throw new ArgumentException(nameof(coord));
        }

        var letra = match.Groups[1].Value.ToUpper();
        var num = Int32.Parse(match.Groups[2].Value);

        var x = _alfabeto.FindIndex(e => e.ToString() == letra);
        var y = num - 1;

        if (y < Min || y > Max)
        {
            throw new ArgumentOutOfRangeException(nameof(y));
        }

        X = x;
        Y = y;
    }

    /// <summary>
    /// Devuelve una representación de texto del componente X
    /// </summary>
    /// <returns>
    /// Una letra de la A a la Z correspondiente al componente X
    /// </returns>
    public string ToStringX()
    {
        return String.Format("{0}", _alfabeto[X]);
    }

    /// <summary>
    /// Devuelve una representación de texto del componente Y
    /// </summary>
    /// <returns>
    /// La componente Y convertida en string de dos dígitos,
    /// empezando en '01' y terminando en '26'
    /// </returns>
    public string ToStringY()
    {
        return String.Format("{0:00}", Y + 1);
    }

    /// <summary>
    /// Convierte la coordenada a su representación alfanumérica
    /// </summary>
    /// <returns>Una versión legible de la coordenada (e.j: F13)</returns>
    public string ToAlfanumérico()
    {
        return ToStringX() + ToStringY();
    }
}
