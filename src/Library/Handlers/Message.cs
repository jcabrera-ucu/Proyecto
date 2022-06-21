namespace Library
{
    public struct Message
    {
        public string Text { get; set; } = String.Empty;

        public Usuario Usuario { get; set; }

        public ControladorJuego? Partida { get; set; }

        public Message(string texto, Usuario usuario, ControladorJuego? partida)
        {
            Text = texto;
            Usuario = usuario;
            Partida = partida;
        }
    }
}
