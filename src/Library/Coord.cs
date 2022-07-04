using System.Text.RegularExpressions;

namespace Library;

// Esta clase cumple con SRP y expert, se encarga de representar coordenadas
// utilizadas por multiples clases y es experta en las mismas.

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
    /// <exception cref="CoordenadaFormatoIncorrecto">
    /// Si 'coord' no está en formato alfanumérico o si {Número} está fuera
    /// del rango permitido.
    /// </exception>
    public Coord(string coord)
    {
        var match = Regex.Match(coord, @"\s*([a-z])\s*(\d{1,2})", RegexOptions.IgnoreCase);

        if (!match.Success)
        {
            throw new CoordenadaFormatoIncorrecto(CoordenadaFormatoIncorrecto.Error.Sintaxis, coord);
        }

        var letra = match.Groups[1].Value.ToUpper();
        var num = Int32.Parse(match.Groups[2].Value);

        var x = _alfabeto.FindIndex(e => e.ToString() == letra);
        var y = num - 1;

        if (y < Min || y > Max)
        {
            throw new CoordenadaFormatoIncorrecto(CoordenadaFormatoIncorrecto.Error.Rango, coord);
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

    /// <summary>
    /// Verifica si un par de coordenadas están "alineadas".
    /// </summary>
    /// <remarks>
    /// Dos coordenadas están alineadas si y solo si tienen la
    /// misma componente X o la misma componente Y.
    /// </remarks>
    /// <param name="a">La primera coordenada del par</param>
    /// <param name="b">La segunda coordenada del par</param>
    /// <returns>True si lo cumplen, false si no lo hacen</returns>
    public static bool Alineadas(Coord a, Coord b)
    {
        return ((a.X == b.X) || (a.Y == b.Y));
    }

    /// <summary>
    /// Verifica si un par de coordenadas están "alineadas" y ordenadas
    /// en la grilla.
    /// </summary>
    /// <remarks>
    /// <p>Dos coordenadas están ordenadas si y solo si los componentes de
    /// la primera son menor o igual a los componentes de la segunda
    /// (las Xs y las Ys respectivamentes).
    /// </p>
    /// </remarks>
    /// <param name="a">La primera coordenada del par</param>
    /// <param name="b">La segunda coordenada del par</param>
    /// <returns>True si lo cumplen, false si no lo hacen</returns>
    public static bool AlineadasYOrdenadas(Coord a, Coord b)
    {
        return Alineadas(a, b) && (a.X <= b.X && a.Y <= b.Y);
    }

    /// <summary>
    /// Dado un par de coordenadas "alineadas", retorna el par ordenado
    /// de tal forma que cumplan "Coord.AlineadasYOrdenadas()"
    /// </summary>
    /// <remarks>
    /// El orden resultante está indefinido si las coordenadas no están
    /// "Alineadas"
    /// </remarks>
    /// <param name="a">La primera coordenada del par</param>
    /// <param name="b">La segunda coordenada del par</param>
    /// <returns>True si lo cumplen, false si no lo hacen</returns>
    public static (Coord, Coord) Ordenar(Coord a, Coord b)
    {
        if (a.X <= b.X && a.Y <= b.Y)
        {
            return (a, b);
        }

        return (b, a);
    }

    /// <summary>
    /// Calcula la cantidad de celdas entre dos coordenadas "Alineadas"
    /// </summary>
    /// <param name="a">La primera coordenada del par</param>
    /// <param name="b">La segunda coordenada del par</param>
    /// <returns>
    /// La cantidad de celdas ocupadas por ambas coordenadas, o cero en
    /// caso de que <c>Alineadas(a, b) == false</c>.
    /// </returns>
    public static int Largo(Coord a, Coord b)
    {
        return Alineadas(a, b) ? (b.X - a.X) + (b.Y - a.Y) + 1 : 0;
    }
}
