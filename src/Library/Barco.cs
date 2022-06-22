namespace Library;
// Esta clase cumple con el patron SRP, su responsabilidad es manejar todo lo relacionado con los barcos.
/// <summary>
/// Posición y estado de un barco.
/// </summary>

public class Barco
{
    /// <summary>
    /// Primera coordenada del barco (la más arriba y más a la izquierda).
    /// </summary>
    public Coord Primera { get; }

    /// <summary>
    /// Segunda coordenada del barco (la más abajo y más a la derecha).
    /// </summary>
    public Coord Segunda { get; }

    /// <summary>
    /// Conjunto de todos los golpes recibidos por el barco.
    /// </summary>
    public HashSet<Coord> Golpes { get; } = new();

    /// <summary>
    /// Conjunto de las coordenadas del barco que fueron reveladas
    /// por algún 'radar'
    /// </summary>
    public HashSet<Coord> Revelados { get; } = new();

    /// <summary>
    /// Devuelve 'true' si éste barco ya fue hundido
    /// </summary>
    public bool Hundido
    {
        get
        {
            return Golpes.Count == Largo;
        }
    }

    /// <summary>
    /// El largo del barco (cantidad de celdas que ocupa en el tablero)
    /// </summary>
    public int Largo
    {
        get
        {
            return Coord.Largo(Primera, Segunda);
        }
    }

    /// <summary>
    /// Contruye un barco en disposición horizontal o vertical
    /// </summary>
    /// <remark>
    /// Para ser válido, ambas coordenadas tiene que estar "alineadas"
    /// (<c>Coord.Alineadas(a, b) == true</c>)
    /// </remark>
    /// <param name="a">La primera coordenada</param>
    /// <param name="b">La segunda coordenada</param>
    /// <exception cref="CoordenadasNoAlineadas">
    /// Cuando las coordenadas no están alineadas.
    /// </exception>
    public Barco(Coord a, Coord b)
    {
        if (!Coord.Alineadas(a, b))
        {
            throw new CoordenadasNoAlineadas(a, b);
        }

        var (primera, segunda) = Coord.Ordenar(a, b);

        Primera = primera;
        Segunda = segunda;
    }

    /// <summary>
    /// Verifica la intersección entre éste barco y otro
    /// </summary>
    /// <param name="barco">El barco con el cual probar la intersección</param>
    /// <returns>True si hay intersección entre éste y 'barco'</returns>
    public bool Intersecta(Barco barco)
    {
        return (Primera.X <= barco.Segunda.X) && (Segunda.X >= barco.Primera.X)
            && (Primera.Y <= barco.Segunda.Y) && (Segunda.Y >= barco.Primera.Y);
    }

    /// <summary>
    /// Verifica la intersección entre éste barco y una coordenada
    /// </summary>
    /// <param name="coord">La coordenada con el cual probar la intersección</param>
    /// <returns>True si hay intersección entre éste barco y 'coord'</returns>
    public bool Intersecta(Coord coord)
    {
        return Intersecta(new Barco(coord, coord));
    }

    /// <summary>
    /// Verifica y agrega un 'golpe' al barco
    /// </summary>
    /// <param name="coord">
    /// La coordenada donde se 'golpea' al barco, si 'coord'
    /// no intersecta al barco, no hace nada.
    /// </param>
    /// <returns>True si coord intersecta al barco, False si no lo hace</returns>
    public bool Golpear(Coord coord)
    {
        if (this.Intersecta(coord))
        {
            Golpes.Add(coord);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Verifica y agrega un 'revelado' al barco
    /// </summary>
    /// <param name="coord">
    /// La coordenada donde se 'revela' el barco, si 'coord'
    /// no intersecta al barco, no hace nada.
    /// </param>
    /// <returns>True si coord intersecta al barco, False si no lo hace</returns>
    public bool Revelar(Coord coord)
    {
        if (this.Intersecta(coord))
        {
            Revelados.Add(coord);

            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return $"{Primera.ToAlfanumérico()}-{Segunda.ToAlfanumérico()}";
    }
}
