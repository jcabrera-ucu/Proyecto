namespace Library;

// Esta clase cumple con SRP, se encarga de representar la información de una jugada
/// <summary>
/// Contiene los datos para una jugada.
/// </summary>
 public  class Jugada
 {
    /// <summary>
    /// Identificador del jugador que realiza la jugada
    /// </summary>
    public Ident Id { get; }

    /// <summary>
    /// El tipo de jugada que es (ataque o radar)
    /// </summary>
    public TipoJugada Tipo { get; }

    /// <summary>
    /// La coordenada sobre la cual se realiza la jugada
    /// </summary>
    public Coord Coordenada { get; }

    /// <summary>
    /// Construye una jugada
    /// </summary>
    /// <param name="id">Id del jugador</param>
    /// <param name="tipo">Tipo de jugada</param>
    /// <param name="coord">Coordenada de destino</param>
    public Jugada(Ident id, TipoJugada tipo, Coord coord)
    {
        Id = id;
        Tipo = tipo;
        Coordenada = coord;
    }
 }
