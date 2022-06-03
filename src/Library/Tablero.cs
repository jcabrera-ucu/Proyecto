using System.Text;

namespace Library;

/// <summary>
///
/// </summary>
public class BarcosSuperpuestosException : Exception
{
    public Barco Primero { get; }

    public Barco Segundo { get; }

    public BarcosSuperpuestosException(Barco a, Barco b)
    {
        Primero = a;
        Segundo = b;
    }
}

enum Celda
{
    Nada,
    Barco,
    Tocado,
    Revelado,
    Agua,
}

/// <summary>
///
/// </summary>
public class Tablero
{
    /// <summary>
    ///
    /// </summary>
    public int Ancho { get; }

    /// <summary>
    ///
    /// </summary>
    public int Alto { get; }

    /// <summary>
    ///
    /// </summary>
    public List<Barco> Barcos { get; } = new();

    /// <summary>
    ///
    /// </summary>
    public List<Coord> Agua { get; } = new();

    /// <summary>
    ///
    /// </summary>
    public Tablero()
        : this(10, 10)
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="ancho"></param>
    /// <param name="alto"></param>
    public Tablero(int ancho, int alto)
    {
        if (ancho <= Coord.Min || ancho > Coord.Max)
        {
            throw new ArgumentOutOfRangeException(nameof(ancho));
        }

        if (alto <= Coord.Min || alto > Coord.Max)
        {
            throw new ArgumentOutOfRangeException(nameof(alto));
        }

        Ancho = ancho;
        Alto = alto;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="coord"></param>
    /// <returns></returns>
    private bool EsValida(Coord coord)
    {
        return (coord.X < Ancho) && (coord.Y < Alto);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public Barco AddBarco(Coord a, Coord b)
    {
        if (!EsValida(a))
        {
            throw new ArgumentOutOfRangeException(nameof(a));
        }

        if (!EsValida(b))
        {
            throw new ArgumentOutOfRangeException(nameof(b));
        }

        var barco = new Barco(a, b);

        foreach (var x in Barcos)
        {
            if (x.Intersecta(barco))
            {
                throw new BarcosSuperpuestosException(barco, x);
            }
        }

        Barcos.Add(barco);

        return barco;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="coord"></param>
    /// <returns></returns>
    public ResultadoAtaque Atacar(Coord coord)
    {
        if (!EsValida(coord))
        {
            throw new ArgumentOutOfRangeException(nameof(coord));
        }

        foreach (var barco in Barcos)
        {
            if (barco.Golpear(coord))
            {
                if (barco.Hundido)
                {
                    return ResultadoAtaque.Hundido;
                }
                else
                {
                    return ResultadoAtaque.Tocado;
                }
            }
        }

        if (!Agua.Exists(x => x == coord))
        {
            Agua.Add(coord);
        }

        return ResultadoAtaque.Agua;
    }

    private Celda GetCelda(Coord coord)
    {
        if (!EsValida(coord))
        {
            throw new ArgumentOutOfRangeException(nameof(coord));
        }

        if (Agua.Exists(x => x == coord))
        {
            return Celda.Agua;
        }

        foreach (var barco in Barcos)
        {
            if (barco.Intersecta(coord))
            {
                if (barco.Golpes.Any(x => x == coord))
                {
                    return Celda.Tocado;
                }

                if (barco.Revelados.Any(x => x == coord))
                {
                    return Celda.Revelado;
                }

                return Celda.Barco;
            }
        }

        return Celda.Nada;
    }

    private string Imprimir(Func<Coord, char> getChar)
    {
        var builder = new StringBuilder();
        var tabulación = new string(' ', 3);
        var líneaHorizontal = new string('-', 2 * Ancho + 1);

        builder.Append(tabulación);

        for (int x = 0; x < Ancho; x++)
        {
            builder.AppendFormat(" {0}", new Coord(x, 0).ToStringX());
        }

        builder.Append("\n");
        builder.Append(tabulación);
        builder.Append(líneaHorizontal);
        builder.Append("\n");

        for (int y = 0; y < Alto; y++)
        {
            builder.AppendFormat("{0} |", new Coord(0, y).ToStringY());

            for (int x = 0; x < Ancho; x++)
            {
                builder.AppendFormat("{0}|", getChar(new Coord(x, y)));
            }

            builder.Append("\n");
            builder.Append(tabulación);
            builder.Append(líneaHorizontal);
            builder.Append("\n");
        }

        return builder.ToString();
    }

    public string ImprimirBarcos()
    {
        return Imprimir((coord) =>
        {
            switch (GetCelda(coord))
            {
                case Celda.Barco:
                case Celda.Revelado:
                case Celda.Tocado:
                    return 'B';
                case Celda.Agua:
                case Celda.Nada:
                default:
                    return ' ';
            }
        });
    }

    public string ImprimirJugadas()
    {
        return Imprimir((coord) =>
        {
            switch (GetCelda(coord))
            {
                case Celda.Agua:
                    return 'A';
                case Celda.Revelado:
                    return 'B';
                case Celda.Tocado:
                    return 'T';
                case Celda.Barco:
                case Celda.Nada:
                default:
                    return ' ';
            }
        });
    }
}

