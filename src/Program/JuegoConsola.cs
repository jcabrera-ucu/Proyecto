using Library;

public class JuegoConsola
{
    public HistóricoEstadísticas Estadísticas { get; }

    public GestorPartidas GestorPartidas { get; }

    public Usuario UsuarioA { get; }

    public Usuario UsuarioB { get; }

    public Usuario UsuarioActual { get; private set; }

    public JuegoConsola()
    {
        Estadísticas = new();
        GestorPartidas = new();

        UsuarioA = new Usuario
        {
            Id = new Ident("Usuario A"),
        };

        UsuarioB = new Usuario
        {
            Id = new Ident("Usuario B"),
        };

        UsuarioA.Estadisticas = Estadísticas.ObtenerEstadística(UsuarioA.Id);
        UsuarioB.Estadisticas = Estadísticas.ObtenerEstadística(UsuarioB.Id);

        UsuarioActual = UsuarioA;
    }

    public void Correr()
    {
        var chain =
            new InicioHandler(
            new MenuHandler(
            new BuscarPartidaHandler(GestorPartidas,
            new BuscarPartidaConRelojHandler(GestorPartidas,
            new EstadisticasHandler(
            new AtaqueHandler(
            new ConfiguracionHandler(
            new JugadasHandler(
            new RadarHandler(
            new TableroHandler(
            new NullHandler()))))))))));

        Console.WriteLine("Escriba /start para comenzar");

        while (true)
        {
            Console.Write($"{UsuarioActual.Id.Value} >> ");

            var line = Console.ReadLine();
            if (line != null)
            {
                if (line == "cambio")
                {
                    if (UsuarioActual == UsuarioA)
                    {
                        UsuarioActual = UsuarioB;
                    }
                    else
                    {
                        UsuarioActual = UsuarioA;
                    }
                }
                else
                {
                    string response;
                    var message = new Message
                    {
                        Usuario = UsuarioActual,
                        Text = line,
                        Partida = GestorPartidas.ObtenerPartida(UsuarioActual),
                    };

                    try
                    {
                        chain.Handle(message, out response);

                        Console.WriteLine();
                        Console.WriteLine(response);
                        Console.WriteLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.WriteLine("¡Error inesperado!");
                        Console.WriteLine(e.Message);
                        Console.WriteLine();
                    }

                    Estadísticas.Guardar();
                }
            }
        }
    }
}
