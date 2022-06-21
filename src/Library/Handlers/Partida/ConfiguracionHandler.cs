namespace Library;

public class ConfiguracionHandler : BasePrefijoHandler
{
    public ConfiguracionHandler(BaseHandler? next)
        : base(next)
    {
        this.Keywords = new string[] {
            "agregar",
            "ag",
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

            if (coords.Count != 2)
            {
                response = "Se esperaban dos coordenadas";
                return true;
            }

            message.Partida.AgregarBarco(message.Usuario.Id, coords[0], coords[1]);

            var mensajes = new List<string>()
            {
                $"Barco agregado {coords[0].ToAlfanumérico()}-{coords[1].ToAlfanumérico()}",
            };

            mensajes.AddRange(MensajesDePartida.Mensajes(message.Usuario, message.Partida));
            response = String.Join('\n', mensajes);
            return true;
        }
        catch (CoordenadasNoAlineadas exc)
        {
            response = $"¡Las coordenadas no están en línea recta!\n"
                     + $" >> {exc.Primera.ToAlfanumérico()} y {exc.Segunda.ToAlfanumérico()}";
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
        catch (BarcosSuperpuestosException exc)
        {
            response = $"El barco que está intentando agregar se superpone con otro\n"
                     + $" >> ({exc.Primero}) y ({exc.Segundo})";
            return true;
        }
        catch (BarcoYaExiste exc)
        {
            response = $"¡Ya existe un barco de tamaño {exc.Largo}";
            return true;
        }
        catch (EstadoPartidaIncorrecto)
        {
            response = "No se pueden agregar barcos en este momento";
            return true;
        }
        catch (Exception)
        {
            response = $"¡Error inesperado, intente nuevamente!";
            return true;
        }
    }
}
