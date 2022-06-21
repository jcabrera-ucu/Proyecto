namespace Library;

/// <summary>
///
/// </summary>
 public  class Jugada
 {
    /// <summary>
    ///
    /// </summary>
    public Ident Id { get; }

    /// <summary>
    ///
    /// </summary>
    public TipoJugada Tipo { get; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public Coord Coordenada { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tipo"></param>
    /// <param name="coord"></param>
    public Jugada(Ident id, TipoJugada tipo, Coord coord)
    {
        Id = id;
        Tipo = tipo;
        Coordenada = coord;
    }
 }
