using System.Text;

namespace Library;

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
    /// Lista de coordenadas, que al ser atacadas, dieron "agua"
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
    /// <param name="a">Coordenada de inicio del barco</param>
    /// <param name="b">Coordenada de fin del barco</param>
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
    public Barco AgregarBarco(Coord a, Coord b)
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
    /// Inserta una coordenada a la lista de "Agua"
    /// </summary>
    /// <param name="coord">
    /// La coordenada a insertar. Si la coordenada, no es válida, o ya
    /// existe en "Agua", no hace nada.
    /// </param>
    private void AgregarAgua(Coord coord)
    {
        if (EsValida(coord) && !Agua.Exists(x => x == coord))
        {
            Agua.Add(coord);
        }
    }

    /// <summary>
    /// Realiza un ataque sobre éste tablero
    /// </summary>
    /// <param name="coord">La coordenada a atacar</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Si la coordenada no es válida para el tablero
    /// </exception>
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

        AgregarAgua(coord);

        return ResultadoAtaque.Agua;
    }

    /// <summary>
    /// Tira el Radar (cuadrícula de 3x3) con centro en 'centro'
    /// </summary>
    /// <param name="centro">El centro de acción del radar</param>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Si 'coord' no es válidad para las dimensiones del tablero
    /// </exception>
    public void Radar(Coord centro)
    {
        if (!EsValida(centro))
        {
            throw new ArgumentOutOfRangeException(nameof(centro));
        }

        var esqSupIzq = new Coord(
            Math.Max(Coord.Min, centro.X - 1),
            Math.Max(Coord.Min, centro.Y - 1)
        );

        var esqInfDer = new Coord(
            Math.Min(Ancho - 1, centro.X + 1),
            Math.Min(Alto - 1, centro.Y + 1)
        );

        for (int x = esqSupIzq.X; x <= esqInfDer.X; x++)
        {
            for (int y = esqSupIzq.Y; y <= esqInfDer.Y; y++)
            {
                var coord = new Coord(x, y);
                var (celda, barco) = GetCelda(coord);
                switch (celda)
                {
                    case Celda.Agua:
                    case Celda.Revelado:
                    case Celda.Tocado:
                        break;
                    case Celda.Barco:
                        if (barco != null)
                        {
                            barco.Revelar(coord);
                        }
                        break;
                    case Celda.Vacio:
                        AgregarAgua(coord);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Obtiene una lista con los barcos que aún no han sido hundidos
    /// </summary>
    /// <returns>Lista de barcos</returns>
    public List<Barco> BarcosAFlote()
    {
        return Barcos.FindAll(barco => !barco.Hundido);
    }

    /// <summary>
    /// Dada una coordenada retorna el estado de la celda en ese punto y la
    /// instancia del barco en esa coordenada (si existe)
    /// </summary>
    /// <param name="coord">La celda a obtener</param>
    /// <returns>Una tupla con el estado de la Celda y la posible instancia de Barco</returns>
    public (Celda celda, Barco? barco) GetCelda(Coord coord)
    {
        if (!EsValida(coord))
        {
            throw new ArgumentOutOfRangeException(nameof(coord));
        }

        if (Agua.Exists(x => x == coord))
        {
            return (Celda.Agua, null);
        }

        foreach (var barco in Barcos)
        {
            if (barco.Intersecta(coord))
            {
                if (barco.Golpes.Any(x => x == coord))
                {
                    return (Celda.Tocado, barco);
                }

                if (barco.Revelados.Any(x => x == coord))
                {
                    return (Celda.Revelado, barco);
                }

                return (Celda.Barco, barco);
            }
        }

        return (Celda.Vacio, null);
    }

    /// <summary>
    /// Crea la representación del tablero en texto
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
                builder.AppendFormat("{0}|", getChar(GetCelda(new Coord(x, y)).celda));
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
