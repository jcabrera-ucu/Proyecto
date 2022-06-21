namespace Library;

public class MensajesDePartida
{
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
                            mensajes.Add(jugador.Tablero.ImprimirBarcos());
                            mensajes.Add($" >> Faltan agregar los barcos de los siguientes tamaños:");
                            mensajes.Add($" >> ({String.Join(", ", jugador.BarcosFaltantes)})");
                            mensajes.Add($"Para agregar un barco utilice el siguiente comando:");
                            mensajes.Add($" >> agregar <coordenada-1> <coordenada-2>");
                            mensajes.Add($" >> (e.j: agregar b1 b4)");
                        }
                        else
                        {
                            mensajes.Add($"¡Ya configuraste todos tus barcos!");
                            mensajes.Add(jugador.Tablero.ImprimirBarcos());
                            mensajes.Add($"Espera a que tu oponente termine");
                        }
                    }
                }
                break;
            case EstadoPartida.TurnoJugadorA:
            case EstadoPartida.TurnoJugadorB:
                {
                    var jugador = partida.ObtenerJugadorPorId(usuario.Id);
                    if (jugador != null)
                    {
                        if (jugador == partida.JugadorActual)
                        {
                            mensajes.Add($"¡Es tu turno!");
                        }
                        else
                        {
                            mensajes.Add($"¡Es el turno de tu oponente!");
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

        return mensajes;
    }
}
