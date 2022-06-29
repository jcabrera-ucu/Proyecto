namespace Library;

/// <summary>
/// Calcula la lista de mensajes a presentar al usuario
/// luego de cada comando.
/// </summary>
public class MensajesDePartida
{
    /// <summary>
    /// Devuelve una lista de mensajes a presentar al usuario
    /// </summary>
    /// <param name="usuario">Instancia del usuario actual</param>
    /// <param name="partida">Instancia de la partida del usuario</param>
    /// <returns>Lista de cadenas</returns>
    public static IList<string> Mensajes(Usuario usuario, ControladorJuego partida)
    {
        var mensajes = new List<string>();

        switch (partida.Estado)
        {
            case EstadoPartida.Configuración:
                {
                    var jugador = partida.ObtenerJugadorPorId(usuario.Id);
                    if (jugador != null)
                    {
                        if (jugador.BarcosFaltantes.Count != 0)
                        {
                            mensajes.Add($"¡A configurar esos barquitos!");
                            mensajes.Add($"{jugador.Tablero.ImprimirBarcos()}");
                            mensajes.Add($" >> Faltan agregar los barcos de los siguientes tamaños:");
                            mensajes.Add($" >> ({String.Join(", ", jugador.BarcosFaltantes)})");
                            mensajes.Add($"Para agregar un barco utilice el siguiente comando:");
                            mensajes.Add($" >> agregar <coordenada-1> <coordenada-2>");
                            mensajes.Add($" >> (e.j: agregar b1 b4)");
                        }
                        else
                        {
                            mensajes.Add($"¡Ya configuraste todos tus barcos!");
                            mensajes.Add($"{jugador.Tablero.ImprimirBarcos()}");
                            mensajes.Add($"Espera a que tu oponente termine");
                        }
                    }
                }
                break;
            case EstadoPartida.TurnoJugadorA:
            case EstadoPartida.TurnoJugadorB:
                {
                    var (jugador, oponente) = MensajesDePartida.ObtenerParDeJugadores(usuario, partida);
                    if (jugador != null && oponente != null)
                    {
                        if (jugador == partida.JugadorActual)
                        {
                            mensajes.Add($"¡Es tu turno!");
                            mensajes.Add($"{oponente.Tablero.ImprimirJugadas()}");
                        }
                        else
                        {
                            mensajes.Add($"¡Es el turno de tu oponente!");
                            mensajes.Add($"{oponente.Tablero.ImprimirJugadas()}");
                        }
                    }
                }
                break;
            case EstadoPartida.Terminado:
            case EstadoPartida.TerminadoPorReloj:
                {
                    mensajes.Add($"¡Partida terminada!");

                    var jugador = partida.ObtenerJugadorPorId(usuario.Id);
                    var ganador = partida.Ganador;

                    if (jugador != null && ganador != null)
                    {
                        if (jugador == ganador)
                            mensajes.Add($" >> ¡GANASTE! <<");
                        else
                            mensajes.Add($" >> ¡Uh, perdiste...! <<");

                        mensajes.Add($" >> Tomate una coquita <<");
                    }
                }
                break;
            default:
                break;
        }

        {
            // Mostrar el tiempo restante si el jugador tiene reloj y
            // hay una partida iniciada.

            var jugador = partida.ObtenerJugadorPorId(usuario.Id);
            if (jugador != null && jugador.Reloj != null)
            {
                var tiempo = jugador.Reloj.TiempoRestante;
                switch (partida.Estado)
                {
                    case EstadoPartida.TurnoJugadorA:
                    case EstadoPartida.TurnoJugadorB:
                        mensajes.Add($"[ Te quedan: {tiempo.Minutes} minutos, {tiempo.Seconds} segundos ]");
                        break;
                    default:
                        break;
                }
            }
        }

        return mensajes;
    }

    /// <summary>
    /// Calcula el par (jugador, oponente).
    /// </summary>
    /// <param name="usuario">Instancia del usuario actual</param>
    /// <param name="partida">Instancia de la partida del usuario</param>
    /// <returns>Un par ordenado con la instancia del jugador actual y su oponente</returns>
    private static (Jugador? jugador, Jugador? oponente) ObtenerParDeJugadores(Usuario usuario, ControladorJuego partida)
    {
        if (partida.JugadorA.Id == usuario.Id)
        {
            return (partida.JugadorA, partida.JugadorB);
        }
        else if (partida.JugadorB.Id == usuario.Id)
        {
            return (partida.JugadorB, partida.JugadorA);
        }
        return (null, null);
    }
}
