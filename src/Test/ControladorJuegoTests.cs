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
    public void Pruebas()
    {
        var j0 = new Jugador(
            new Ident("0"),
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var j1 = new Jugador(
            new Ident("1"),
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        var c = new ControladorJuego(j0, j1);

        Assert.AreEqual(EstadoPartida.Configuración, c.Estado);
        Assert.AreEqual(j0, c.JugadorA);
        Assert.AreEqual(j1, c.JugadorB);
        Assert.AreEqual(j0, c.ObtenerJugadorPorId(new Ident("0")));
        Assert.AreEqual(j1, c.ObtenerJugadorPorId(new Ident("1")));
        Assert.IsNull(c.ObtenerJugadorPorId(new Ident("2")));

        Assert.IsNull(c.JugadorActual);
        Assert.IsNull(c.OponenteActual);
        Assert.IsNull(c.Ganador);
        Assert.IsNull(c.Perdedor);

        c.AgregarBarco(new Ident("0"), new Coord(0, 0), new Coord(0, 1));
        c.AgregarBarco(new Ident("0"), new Coord(1, 0), new Coord(1, 2));
        c.AgregarBarco(new Ident("0"), new Coord(2, 0), new Coord(2, 3));
        c.AgregarBarco(new Ident("0"), new Coord(3, 0), new Coord(3, 4));

        Assert.AreEqual(0, j0.BarcosFaltantes.Count);

        c.AgregarBarco(new Ident("1"), new Coord(0, 0), new Coord(0, 1));
        c.AgregarBarco(new Ident("1"), new Coord(1, 0), new Coord(1, 2));
        c.AgregarBarco(new Ident("1"), new Coord(2, 0), new Coord(2, 3));
        c.AgregarBarco(new Ident("1"), new Coord(3, 0), new Coord(3, 4));

        Assert.AreEqual(0, j1.BarcosFaltantes.Count);

        Assert.AreEqual(EstadoPartida.TurnoJugadorA, c.Estado);

        Assert.AreEqual(j0, c.JugadorActual);
        Assert.AreEqual(j1, c.OponenteActual);
        Assert.IsNull(c.Ganador);
        Assert.IsNull(c.Perdedor);

        Assert.AreEqual(ResultadoJugada.Tocado, c.HacerJugada(new Jugada(
            new Ident("0"), TipoJugada.Ataque, new Coord(0, 0))));

        Assert.Throws<JugadorIncorrecto>(() =>
        {
            c.HacerJugada(new Jugada(
                new Ident("0"), TipoJugada.Ataque, new Coord(0, 0)));
        });
    }
}
