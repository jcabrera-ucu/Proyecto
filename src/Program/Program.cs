using Library;

var c = new ControladorJuego(10, 10);


while (true)
{
    Console.WriteLine($"Estado actual: {c.Estado}");
    var line = Console.ReadLine();
    var partes = line.Split(' ');
    var idJugador = partes[0];

    switch (c.Estado)
    {
        case EstadoPartida.Configuración:
            var coordA = new Coord(partes[1]);
            var coordB = new Coord(partes[2]);
            c.AgregarBarco(idJugador, coordA, coordB);
            break;
        case EstadoPartida.TurnoJugadorA:
        case EstadoPartida.TurnoJugadorB:
            var acción = partes[1];
            var coord = new Coord(partes[2]);
            switch (acción)
            {
                case "atacar":
                case "ataque":
                case "a":
                    Console.WriteLine(c.HacerJugada(new Jugada(idJugador, TipoJugada.Ataque, coord)));
                    break;
                case "radar":
                case "r":
                    Console.WriteLine(c.HacerJugada(new Jugada(idJugador, TipoJugada.Radar, coord)));
                    break;
                default:
                    Console.WriteLine("Acción desconocida");
                    break;
            }
            break;
    }
}
