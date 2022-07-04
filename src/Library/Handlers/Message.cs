namespace Library
{
    /// <summary>
    ///
    /// </summary>
    public struct Message
    {
        /// <summary>
        ///
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Ident IdJugador { get; set; }

        public string Nombre { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="idJugador"></param>
        /// <param name="partida"></param>
        public Message(string texto, Ident idJugador, string nombre)
        {
            Text = texto;
            IdJugador = idJugador;
            Nombre = nombre;
        }
    }
}
