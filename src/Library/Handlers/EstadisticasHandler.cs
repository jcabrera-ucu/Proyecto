namespace Library
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class EstadisticasHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EstadisticasHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public EstadisticasHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "Estadisticas" };
        }

        /// <summary>
        /// Procesa el mensaje "chau" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            if (this.CanHandle(message))
            {
                response = $"Estadisticas para usuario {message.Id}\n";
                response+=$"+Ha ganado {ListaUsuario.GetInstance().UsuariosExistentes[message.Id].Estadistica.Victorias} veces\n";
                response+=$"+Ha perdido {ListaUsuario.GetInstance().UsuariosExistentes[message.Id].Estadistica.Derrotas} veces\n";
                response+=$"+Ha acertado {ListaUsuario.GetInstance().UsuariosExistentes[message.Id].Estadistica.Aciertos} veces\n";
                response+=$"+Ha fallado {ListaUsuario.GetInstance().UsuariosExistentes[message.Id].Estadistica.Fallos} veces\n";
                response+=$"+Ha hundido {ListaUsuario.GetInstance().UsuariosExistentes[message.Id].Estadistica.Hundidos} barcos\n";
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}