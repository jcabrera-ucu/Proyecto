namespace Library;

// Cumple con SRP ya que su única responsabilidad es el almacenamiento de los bots
// Cumple con Law of Demeter ya que el usuario de la clase no necesita
// conocer como funcionan los tipos internos, ni accederlos.

/// <summary>
/// Gestiona los Bots que están jugando
/// </summary>
public class GestorBots
{
    /// <summary>
    /// Lista de todos los bots en juego
    /// </summary>
    public List<Robotina> Bots { get; private set; } = new();

    /// <summary>
    /// Crea un nuevo bot
    /// </summary>
    /// <param name="idBot">Id del nuevo Bot</param>
    /// <param name="partida">Partida en la que está jugando</param>
    /// <returns>Instancia del bot creado</returns>
    public Robotina Nuevo(Ident idBot, ControladorJuego partida)
    {
        var bot = new Robotina(idBot, partida);
        Bots.Add(bot);
        return bot;
    }

    /// <summary>
    /// Ejecuta todos los bots
    /// </summary>
    /// <returns>Los mensajes que generan cada uno de los bots</returns>
    public List<Message> EjecutarBots()
    {
        var eliminar = new List<Robotina>();
        var mensajes = new List<Message>();

        foreach (var bot in Bots)
        {
            var comandos = bot.Siguiente();

            foreach (var comando in comandos)
            {
                switch (comando.Accion)
                {
                    case Robotina.Comando.Tipo.Eliminar:
                        eliminar.Add(bot);
                        break;
                    case Robotina.Comando.Tipo.Agregar:
                        {
                            var c0 = comando.Coordenadas[0];
                            var c1 = comando.Coordenadas[1];
                            mensajes.Add(new Message
                            {
                                IdJugador = bot.IdBot,
                                Nombre = bot.Nombre,
                                Text = $"agregar {c0.ToAlfanumérico()} {c1.ToAlfanumérico()}",
                            });
                        }
                        break;
                    case Robotina.Comando.Tipo.Atacar:
                        {
                            var c0 = comando.Coordenadas[0];
                            mensajes.Add(new Message
                            {
                                IdJugador = bot.IdBot,
                                Nombre = bot.Nombre,
                                Text = $"atacar {c0.ToAlfanumérico()}",
                            });
                        }
                        break;
                    case Robotina.Comando.Tipo.Radar:
                        {
                            var c0 = comando.Coordenadas[0];
                            mensajes.Add(new Message
                            {
                                IdJugador = bot.IdBot,
                                Nombre = bot.Nombre,
                                Text = $"radar {c0.ToAlfanumérico()}",
                            });
                        }
                        break;
                    case Robotina.Comando.Tipo.Esperar:
                    default:
                        break;
                }
            }
        }

        foreach (var bot in eliminar)
        {
            Bots.Remove(bot);
        }

        return mensajes;
    }
}
