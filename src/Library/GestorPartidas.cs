namespace Library;

// Expert, esta clase es experta en partidas, es la encargada de gestionarlas
// y juntar jugadores para enfrentarse en juego.

/// <summary>
/// Mantiene registro de todas las partidas en juego.
/// </summary>
public class GestorPartidas
{
    public class Usuario
    {
        public Ident Id { get; set; }

        public string Nombre { get; set; } = String.Empty;
    }

    /// <summary>
    /// Diccionario que asocia a cada usuario con su partida
    /// </summary>
    public Dictionary<Ident, ControladorJuego> Partidas { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public HistóricoEstadísticas Estadísicas { get; }

    /// <summary>
    /// Un identificador de usuario que está actualmente
    /// buscando partida, puede ser null
    /// </summary>
    public Usuario? EnEspera { get; private set; }

    /// <summary>
    /// Un identificador de usuario que está actualmente buscando
    /// una partida con reloj, puede ser null
    /// </summary>
    public Usuario? EnEsperaConReloj { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public TimeSpan TiempoReloj { get; } = TimeSpan.FromSeconds(15);

    /// <summary>
    /// Construye un gestor vacío
    /// </summary>
    public GestorPartidas(HistóricoEstadísticas estadísiticas)
    {
        Partidas = new();
        Estadísicas = estadísiticas;
    }

    /// <summary>
    /// Busca una partida
    /// </summary>
    /// <param name="usuario">Usuario que busca la partida</param>
    /// <param name="conReloj">True si es un partida con reloj</param>
    /// <returns>Retorna una instancia de la partida, retorna null si no encontró oponente.</returns>
    public ControladorJuego? BuscarNuevaPartida(Usuario usuario, bool conReloj = false)
    {
        Usuario? oponente = null;
        if (conReloj)
        {
            if (EnEsperaConReloj == null || EnEsperaConReloj.Id == usuario.Id)
            {
                EnEsperaConReloj = usuario;
            }
            else
            {
                oponente = EnEsperaConReloj;
            }
        }
        else
        {
            if (EnEspera == null || EnEspera.Id == usuario.Id)
            {
                EnEspera = usuario;
            }
            else
            {
                oponente = EnEspera;
            }
        }

        if (oponente == null)
        {
            return null;
        }

        var controladorJuego = NuevaPartida(usuario, oponente, conReloj);

        // Estas asignaciones se hacen en este punto para evitar que éste
        // objeto quede en un estado inválido en caso de que el código de arriba
        // tire una excepción.
        if (conReloj)
        {
            EnEsperaConReloj = null;
        }
        else
        {
            EnEspera = null;
        }

        return controladorJuego;
    }

    /// <summary>
    /// Remueve una partida del gestor
    /// </summary>
    /// <param name="partida">Partida a eliminar</param>
    public void EliminarPartida(ControladorJuego partida)
    {
        var id1 = partida.JugadorA.Id;
        var id2 = partida.JugadorB.Id;

        Partidas.Remove(id1);
        Partidas.Remove(id2);
    }

    /// <summary>
    /// Agrega una nueva partida
    /// </summary>
    /// <param name="usuarioA">Usuario jugador</param>
    /// <param name="usuarioB">Usuario jugador</param>
    /// <param name="conReloj">True si la partida tiene reloj</param>
    /// <returns>Instancia de la partida</returns>
    public ControladorJuego NuevaPartida(Usuario usuarioA, Usuario usuarioB, bool conReloj = false)
    {
        var controladorJuego = new ControladorJuego(
            new Jugador(
                id: usuarioB.Id,
                nombre: usuarioB.Nombre,
                tablero: new Tablero(),
                reloj: conReloj ?  new Reloj(TiempoReloj) : null,
                radaresDisponibles: 1,
                estadistica: Estadísicas.ObtenerEstadística(usuarioB.Id)
            ),
            new Jugador(
                id: usuarioA.Id,
                nombre: usuarioA.Nombre,
                tablero: new Tablero(),
                reloj: conReloj ?  new Reloj(TiempoReloj) : null,
                radaresDisponibles: 1,
                estadistica: Estadísicas.ObtenerEstadística(usuarioA.Id)
            )
        );

        AgregarPartida(usuarioA.Id, usuarioB.Id, controladorJuego);

        return controladorJuego;
    }

    /// <summary>
    /// Asocia una partida y sus jugadores
    /// </summary>
    /// <param name="usuario1">Usuario de la partida</param>
    /// <param name="usuario2">Usuario de la partida</param>
    /// <param name="controladorJuego">La partida de los usuarios</param>
    private void AgregarPartida(Ident usuario1, Ident usuario2, ControladorJuego controladorJuego)
    {
        Partidas.Add(usuario1, controladorJuego);
        Partidas.Add(usuario2, controladorJuego);
    }

    /// <summary>
    /// Devuelve instancia de la partida que le corresponde a un usuario
    /// </summary>
    /// <param name="usuario">Usuario para el cual buscar su partida</param>
    /// <returns>Instancia de un ControladorJuego, o null</returns>
    public ControladorJuego? ObtenerPartida(Ident idJugador)
    {
        if (Partidas.ContainsKey(idJugador))
        {
            return Partidas[idJugador];
        }

        return null;
    }
}
