using System.Text;

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

/// <summary>
/// Tablero del jugador.
/// </summary>
public class Tablero
{
    /// <summary>
    /// Cantidad de celdas horizontalmente
    /// </summary>
    public int Ancho { get; }

    /// <summary>
    /// Cantidad de celdas verticalmente
    /// </summary>
    public int Alto { get; }

    /// <summary>
    /// Lista de los barcos del jugador.
    /// (todos disjuntos)
    /// </summary>
    public List<Barco> Barcos { get; } = new();

    /// <summary>
    /// Lista de coordenadas que al ser atacadas, dieron "agua"
    /// </summary>
    public List<Coord> Agua { get; } = new();

    /// <summary>
    /// Construye un tablero de 10x10
    /// </summary>
    public Tablero()
        : this(10, 10)
    {
    }

    /// <summary>
    /// Construye el tablero vacío
    /// </summary>
    /// <param name="ancho">Cantidad de celdas horizontalmente</param>
    /// <param name="alto">Cantidad de celdas verticalmente</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Si ancho es menor que 1 o mayor que Coord.Max + 1
    /// </exception>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Si alto es menor que 1 o mayor que Coord.Max + 1
    /// </exception>
    public Tablero(int ancho, int alto)
    {
        if (ancho < (Coord.Min + 1) || ancho > (Coord.Max + 1))
        {
            throw new ArgumentOutOfRangeException(nameof(ancho));
        }

        if (alto < (Coord.Min + 1) || alto > (Coord.Max + 1))
        {
            throw new ArgumentOutOfRangeException(nameof(alto));
        }

        Ancho = ancho;
        Alto = alto;
    }

    /// <summary>
    /// Verifica que una coordenada esté "adentro" de éste tablero
    /// </summary>
    /// <param name="coord">Coordenada a verificar</param>
    /// <returns>True si 'coord' es válida para este tablero</returns>
    private bool EsValida(Coord coord)
    {
        return (coord.X < Ancho) && (coord.Y < Alto);
    }

    /// <summary>
    /// Agrega un nuevo Barco al tablero
    /// </summary>
    /// <param name="a">Coordenada de inicio del barco (más arriba y más a la izquierda)</param>
    /// <param name="b">Coordenada de fin del barco (más abajo y más a la derecha)</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Si 'a' no es válidad para las dimensiones del tablero
    /// </exception>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Si 'b' no es válidad para las dimensiones del tablero
    /// </exception>
    /// <exception cref="Library.BarcosSuperpuestosException">
    /// Si el barco que se intenta crear intersecta a algún otro
    /// barco ya existente en el tablero
    /// </exception>
    /// <returns>La instancia del Barco creada</returns>
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
    /// Realiza un ataque sobre éste tablero
    /// </summary>
    /// <param name="coord">La coordenada a atacar</param>
    /// <returns>El restulado del ataque</returns>
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

    /// <summary>
    /// Dada una coordenada retorna el estado de la celda en ese punto.
    /// </summary>
    /// <param name="coord">La celda a obtener</param>
    /// <returns>El contenido de la celda</returns>
    public Celda GetCelda(Coord coord)
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

        return Celda.Vacio;
    }

    /// <summary>
    /// Crea la representación del tablero en cadena
    /// </summary>
    /// <param name="getChar">Se llama con una 'Celda' y debe devolver
    /// el caracter a presentar en el tablero</param>
    /// <returns>Representación en texto del tablero</returns>
    private string Imprimir(Func<Celda, char> getChar)
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
                builder.AppendFormat("{0}|", getChar(GetCelda(new Coord(x, y))));
            }

            builder.Append("\n");
            builder.Append(tabulación);
            builder.Append(líneaHorizontal);
            builder.Append("\n");
        }

        return builder.ToString();
    }

    /// <summary>
    /// Representación del tablero para el jugador
    /// </summary>
    /// <returns>Tablero desde el punto de vista del jugador</returns>
    public string ImprimirBarcos()
    {
        return Imprimir((celda) =>
        {
            switch (celda)
            {
                case Celda.Barco:
                case Celda.Revelado:
                case Celda.Tocado:
                    return 'B';
                case Celda.Agua:
                case Celda.Vacio:
                default:
                    return ' ';
            }
        });
    }

    /// <summary>
    /// Representación del tablero para el oponente
    /// </summary>
    /// <returns>El tablero desde el punto de vista del oponente</returns>
    public string ImprimirJugadas()
    {
        return Imprimir(celda =>
        {
            switch (celda)
            {
                case Celda.Agua:
                    return 'A';
                case Celda.Revelado:
                    return 'B';
                case Celda.Tocado:
                    return 'T';
                case Celda.Barco:
                case Celda.Vacio:
                default:
                    return ' ';
            }
        });
    }
}

