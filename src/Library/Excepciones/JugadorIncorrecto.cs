namespace Library;

/// <summary>
/// El jugador que intentó realizar la acción no existe o no es su turno
/// </summary>
public class JugadorIncorrecto : Exception
{
    public Ident Identificador { get; }

    public JugadorIncorrecto(Ident id)
    {
        Identificador = id;
    }
}
