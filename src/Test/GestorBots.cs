using NUnit.Framework;
using Library;

namespace Test;

public class GestorBotsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NuevoBot()
    {
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

        var j1 = new Jugador(
            idBot,
            "Robotina",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var c = new ControladorJuego(j0, j1);

        var gestor = new GestorBots();

        var robotina = gestor.Nuevo(idBot, c);

        Assert.IsNotNull(robotina);
        Assert.AreEqual(idBot, robotina.IdBot);
        Assert.AreEqual(c, robotina.Partida);
        Assert.AreEqual(robotina, gestor.Bots[0]);
    }

    [Test]
    public void EjecutarBots()
    {
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

        var j1 = new Jugador(
            idBot,
            "Robotina",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var c = new ControladorJuego(j0, j1);

        var gestor = new GestorBots();

        var robotina = gestor.Nuevo(idBot, c);

        var mensajes = gestor.EjecutarBots();
        foreach (var mensaje in mensajes)
        {
            Assert.AreEqual(idBot, mensaje.IdJugador);
            Assert.AreEqual(robotina.Nombre, mensaje.Nombre);
            Assert.That(mensaje.Text, Contains.Substring("agregar"));
        }
    }
}
