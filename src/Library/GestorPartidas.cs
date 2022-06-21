namespace Library;

/// <summary>
/// Mantiene registro de todas las partidas en juego.
/// </summary>
public class GestorPartidas
{
    /// <summary>
    /// Diccionario que asocia a cada usuario con su partida
    /// </summary>
    public Dictionary<Usuario, ControladorJuego> Partidas { get; private set; }

    /// <summary>
    /// Una referencia a un Usuario que está actualmente buscando partida, puede ser null
    /// </summary>
    public Usuario? EnEspera { get; private set; }

    /// <summary>
    /// Una referencia a un Usuario que está actualmente buscando partida con reloj,
    /// puede ser null
    /// </summary>
    public Usuario? EnEsperaConReloj { get; private set; }

    /// <summary>
    /// Construye un gestor vacío
    /// </summary>
    public GestorPartidas()
    {
        Partidas = new();
    }

    /// <summary>
    /// Busca una partida
    /// </summary>
    /// <param name="usuario">Usuario que busca la partida</param>
    /// <param name="conReloj">True si es un partida con reloj</param>
    /// <returns>Retorna una instancia de la partida, retorna null si no encontró oponente.</returns>
    public ControladorJuego? BuscarNuevaPartida(Usuario usuario, bool conReloj = false)
    {
        if (conReloj)
        {
            if (EnEsperaConReloj == null)
            {
                EnEsperaConReloj = usuario;
                return null;
            }

            if (EnEsperaConReloj == usuario)
            {
                return null;
            }

            var controladorJuego = new ControladorJuego(
                new Jugador(
                    id: EnEsperaConReloj.Id,
                    tablero: new Tablero(),
                    reloj: new Reloj(TimeSpan.FromMinutes(5)),
                    radaresDisponibles: 1,
                    estadistica: EnEsperaConReloj.Estadisticas
                ),
                new Jugador(
                    id: usuario.Id,
                    tablero: new Tablero(),
                    reloj: new Reloj(TimeSpan.FromMinutes(5)),
                    radaresDisponibles: 1,
                    estadistica: usuario.Estadisticas
                )
            );

            AgregarPartida(EnEsperaConReloj, usuario, controladorJuego);

            EnEsperaConReloj = null;

            return controladorJuego;
        }
        else
        {
            if (EnEspera == null)
            {
                EnEspera = usuario;
                return null;
            }

            if (EnEspera == usuario)
            {
                return null;
            }

            var controladorJuego = new ControladorJuego(
                new Jugador(
                    id: EnEspera.Id,
                    tablero: new Tablero(),
                    reloj: null,
                    radaresDisponibles: 1,
                    estadistica: EnEspera.Estadisticas
                ),
                new Jugador(
                    id: usuario.Id,
                    tablero: new Tablero(),
                    reloj: null,
                    radaresDisponibles: 1,
                    estadistica: usuario.Estadisticas
                )
            );

            AgregarPartida(EnEspera, usuario, controladorJuego);

            EnEspera = null;

            return controladorJuego;
        }
    }

    /// <summary>
    /// Asocia una partida y sus jugadores
    /// </summary>
    /// <param name="usuario1">Usuario de la partida</param>
    /// <param name="usuario2">Usuario de la partida</param>
    /// <param name="controladorJuego">La partida de los usuarios</param>
    private void AgregarPartida(Usuario usuario1, Usuario usuario2, ControladorJuego controladorJuego)
    {
        Partidas.Add(usuario1, controladorJuego);
        Partidas.Add(usuario2, controladorJuego);
    }

    /// <summary>
    /// Devuelve instancia de la partida que le corresponde a un usuario
    /// </summary>
    /// <param name="usuario">Usuario para el cual buscar su partida</param>
    /// <returns>Instancia de un ControladorJuego, o null</returns>
    public ControladorJuego? ObtenerPartida(Usuario usuario)
    {
        if (Partidas.ContainsKey(usuario))
        {
            return Partidas[usuario];
        }

        return null;
    }
}
