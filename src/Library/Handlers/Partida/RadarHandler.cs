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

    protected override bool InternalHandle(Message message, out string response, out string response2)
    {
        response2 = string.Empty;
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
                $"¡Radar desplegado!",
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
        catch (JugadorIncorrecto)
        {
            response = "¡No es tu turno!";
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            response = "¡Coordenada fuera del tablero!";
            return true;
        }
        catch (RadaresAgotados)
        {
            response = "¡Ya no te quedan radares!";
            return true;
        }
        catch (CoordenadaFormatoIncorrecto exc)
        {
            switch (exc.Razón)
            {
                case CoordenadaFormatoIncorrecto.Error.Rango:
                    response = $"¡Error de rango numérico en: {exc.Value}!";
                    break;
                case CoordenadaFormatoIncorrecto.Error.Sintaxis:
                default:
                    response = $"¡Error de sintaxis en: {exc.Value}!";
                    break;
            }
            return true;
        }
        catch (Exception)
        {
            response = $"¡Error inesperado, intente nuevamente!";
            return true;
        }
    }
}
