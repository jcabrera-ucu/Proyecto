namespace Library
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class RadarHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RadarHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public RadarHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "Radar" };
        }

        /// <summary>
        /// Procesa el mensaje "chau" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            if (CanHandle(message))
            {
                var coords = LeerCoordenadas.Leer(message.Text);
                if (coords.Count != 1)
                {
                    response = string.Empty;
                    return false;

                }
                var partes = message.Text.Split(" ");
                if (partes.Count() != 2)
                {
                    response = string.Empty;

                    return false;

                }
                if (partes[0] != "Atacar")
                {
                    response = string.Empty;

                    return false;
                }
                try
                {
                    var c0 = new Coord(partes[1]);
                    var resultado = ControladorJuego.HacerJugada(new Jugada(message.Id, TipoJugada.Radar, c0));
                    response = $"¡{resultado}!";
                    return true;
                }
                catch (CoordenadaFormatoIncorrecto e)
                {
                    switch (e.Razón)
                    {

                        case CoordenadaFormatoIncorrecto.Error.Rango:
                            response = $"Error de Rango: Las coordenadas {e.Value} estan fuera del tablero";
                            return true;

                            break;
                        case CoordenadaFormatoIncorrecto.Error.Sintaxis:
                        default:
                            response = $"Error de Sintaxis: El formato de {e.Value} no es correcto";
                            return true;

                            break;
                    }

                    return true;

                }
                catch (EstadoPartidaIncorrecto e)
                {
                    response = "No se pueden agregar barcos en este momento";
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }
    }
}