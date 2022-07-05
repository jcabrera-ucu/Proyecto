using System;
using NUnit.Framework;
using Library;

namespace Test;

public class ControladorJuegoTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Construccion()
    {
        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        var j0 = new Jugador(
            idJugadorA,
            "JugadorA",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var j1 = new Jugador(
            idJugadorB,
            "JugadorB",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var c = new ControladorJuego(j0, j1);

        Assert.AreEqual(EstadoPartida.Configuración, c.Estado);
        Assert.AreEqual(j0, c.JugadorA);
        Assert.AreEqual(j1, c.JugadorB);

        Assert.IsNull(c.JugadorActual);
        Assert.IsNull(c.OponenteActual);
        Assert.IsNull(c.Ganador);
        Assert.IsNull(c.Perdedor);

        Assert.AreEqual(c.BarcosEsperados.Count, c.BarcosFaltantes(j0.Id).Count);
    }

    [Test]
    public void JugadoresIncompatibles()
    {
        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        var j0 = new Jugador(
            idJugadorA,
            "JugadorA",
            new Tablero(10, 10),
            null,
            1,
            new Estadistica()
        );

        var j1 = new Jugador(
            idJugadorB,
            "JugadorB",
            new Tablero(5, 5),
            null,
            1,
            new Estadistica()
        );

        Assert.Throws<JugadoresIncompatibles>(() =>
        {
            var c = new ControladorJuego(j0, j1);
        });
    }

    [Test]
    public void ObtenerJugadorPorId()
    {
        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        var j0 = new Jugador(
            idJugadorA,
            "JugadorA",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var j1 = new Jugador(
            idJugadorB,
            "JugadorB",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var c = new ControladorJuego(j0, j1);

        Assert.AreEqual(j0, c.ObtenerJugadorPorId(j0.Id));
        Assert.AreEqual(j1, c.ObtenerJugadorPorId(j1.Id));
        Assert.AreEqual(null, c.ObtenerJugadorPorId(new Ident()));
    }

    [Test]
    public void OponenteDe()
    {
        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        var j0 = new Jugador(
            idJugadorA,
            "JugadorA",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var j1 = new Jugador(
            idJugadorB,
            "JugadorB",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var c = new ControladorJuego(j0, j1);

        Assert.AreEqual(j1, c.OponenteDe(j0.Id));
        Assert.AreEqual(j0, c.OponenteDe(j1.Id));
        Assert.AreEqual(null, c.OponenteDe(new Ident()));
    }

    [Test]
    public void EsTurnoDe()
    {
        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        var j0 = new Jugador(
            idJugadorA,
            "JugadorA",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var j1 = new Jugador(
            idJugadorB,
            "JugadorB",
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var c = new ControladorJuego(j0, j1);

        Assert.IsFalse(c.EsTurnoDe(j0.Id));
        Assert.IsFalse(c.EsTurnoDe(j1.Id));
        Assert.IsFalse(c.EsTurnoDe(new Ident()));

        c.AgregarBarco(j0.Id, new Coord(0, 0), new Coord(0, 1));
        c.AgregarBarco(j0.Id, new Coord(1, 0), new Coord(1, 2));
        c.AgregarBarco(j0.Id, new Coord(2, 0), new Coord(2, 3));
        c.AgregarBarco(j0.Id, new Coord(3, 0), new Coord(3, 4));

        c.AgregarBarco(j1.Id, new Coord(0, 0), new Coord(0, 1));
        c.AgregarBarco(j1.Id, new Coord(1, 0), new Coord(1, 2));
        c.AgregarBarco(j1.Id, new Coord(2, 0), new Coord(2, 3));
        c.AgregarBarco(j1.Id, new Coord(3, 0), new Coord(3, 4));

        Assert.AreEqual(EstadoPartida.TurnoJugadorA, c.Estado);
        Assert.IsTrue(c.EsTurnoDe(j0.Id));
        Assert.IsFalse(c.EsTurnoDe(j1.Id));
        Assert.IsFalse(c.EsTurnoDe(new Ident()));
    }
}
