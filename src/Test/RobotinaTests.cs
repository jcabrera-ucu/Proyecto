using System;
using NUnit.Framework;
using Library;

namespace Test;

public class RobotinaTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ComandosSiguiente()
    {
        // Para poder testear correctamente esta funcionalidad
        // habría que parametrizar "Robotina" con un generador de
        // números pseudoaleatorios que pueda controlar.

        var idJugadorA = new Ident();
        var idBot = new Ident();

        var j0 = new Jugador(
            idJugadorA,
            "JugadorA",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var jb = new Jugador(
            idBot,
            "Robotina",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var c = new ControladorJuego(j0, jb);

        var bot = new Robotina(idBot, c);

        c.AgregarBarco(j0.Id, new Coord(0, 0), new Coord(0, 1));
        c.AgregarBarco(j0.Id, new Coord(1, 0), new Coord(1, 2));
        c.AgregarBarco(j0.Id, new Coord(2, 0), new Coord(2, 3));
        c.AgregarBarco(j0.Id, new Coord(3, 0), new Coord(3, 4));

        {
            var comandos = bot.Siguiente();

            foreach (var cmd in comandos)
            {
                Assert.AreEqual(Robotina.Comando.Tipo.Agregar, cmd.Accion);
                Assert.AreEqual(2, cmd.Coordenadas.Count);

                c.AgregarBarco(idBot, cmd.Coordenadas[0], cmd.Coordenadas[1]);
            }
        }

        c.HacerJugada(new Jugada(
            idJugadorA,
            TipoJugada.Ataque,
            new Coord(0, 0)
        ));

        {
            var comandos = bot.Siguiente();

            foreach (var cmd in comandos)
            {
                Assert.AreEqual(Robotina.Comando.Tipo.Atacar, cmd.Accion);
            }
        }
    }
}


