namespace Library;

public class RadarHandler : BasePrefijoHandler
{
    public RadarHandler(BaseHandler? next)
        : base(next)
    {
        this.Keywords = new string[] {
                "radar",
                "r",
            };
    }

    protected override bool InternalHandle(Message message, out string response)
    {
        if (!CanHandle(message))
        {
            response = string.Empty;
            return false;
        }

        if (message.Partida == null)
        {
            response = "No hay ninguna partida activa";
            return true;
        }

        try
        {
            var coords = LeerCoordenadas.Leer(message.Text);

            if (coords.Count != 1)
            {
                response = "Se esperaba una coordenada";
                return true;
            }

            var resultado = message.Partida.HacerJugada(new Jugada(
                message.Usuario.Id,
                TipoJugada.Radar,
                coords[0]
            ));

            var mensajes = new List<string>()
            {
                $"¡Radar desplegado!\n{message.Partida.MostrarJugadas(message.Usuario.Id)}",
            };

            mensajes.AddRange(MensajesDePartida.Mensajes(message.Usuario, message.Partida));
            response = String.Join('\n', mensajes);
            return true;
        }
        catch (EstadoPartidaIncorrecto)
        {
            response = "No se puede desplegar el radar en este momento";
            return true;
        }
    }
}
