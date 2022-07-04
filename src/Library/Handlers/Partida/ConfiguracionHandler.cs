namespace Library;

public class ConfiguracionHandler : BasePrefijoHandler
{
    public GestorPartidas GestorPartidas { get; }

    public ConfiguracionHandler(GestorPartidas gestorPartidas, BaseHandler? next)
        : base(next)
    {
        this.GestorPartidas = gestorPartidas;
        this.Keywords = new string[] {
            "agregar",
            "ag",
        };
    }

    protected override bool InternalHandle(Message message, out string remitente, out string oponente)
    {
        oponente = String.Empty;

        if (!CanHandle(message))
        {
            remitente = String.Empty;
            oponente = String.Empty;

            return false;
        }

        var partida = GestorPartidas.ObtenerPartida(message.IdJugador);

        if (partida == null)
        {
            remitente = "No hay ninguna partida activa";
            oponente = String.Empty;

            return true;
        }

        var coords = LeerCoordenadas.Leer(message.Text);

        if (coords.Count != 2)
        {
            remitente = "Error: se esperaban dos coordenadas";
            oponente = String.Empty;

            return true;
        }

        partida.AgregarBarco(message.IdJugador, coords[0], coords[1]);

        oponente = String.Empty;

        remitente =
            $"Barco agregado {coords[0].ToAlfanumérico()}-{coords[1].ToAlfanumérico()}\n" +
            $"{partida.MostrarTablero(message.IdJugador)}\n";

        if (partida.BarcosFaltantes(message.IdJugador).Count != 0)
        {
            remitente +=
                $"Aún te faltan agregar barcos de los siguientes tamaños:\n" +
                $" => ({String.Join(", ", partida.BarcosFaltantes(message.IdJugador))})\n";
        }
        else
        {
            remitente +=
                $"¡Ya has agregado todos tus barcos!\n" +
                $"Espera a que tu oponente termine\n";

            switch (partida.Estado)
            {
                case EstadoPartida.TurnoJugadorA:
                case EstadoPartida.TurnoJugadorB:
                    if (partida.EsTurnoDe(message.IdJugador))
                    {
                        remitente +=
                            "¡Es tu turno!\n";
                        oponente = "Es el turno de tu oponente";
                    }
                    else
                    {
                        remitente += "Es el turno de tu oponente";
                        oponente = "¡Es tu turno!";
                    }
                    break;
                default:
                    break;
            }

        }

        return true;
    }
}
