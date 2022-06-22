using NUnit.Framework;
using Library;

namespace Test;

public class JugadorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void IdentConValor()
    {
        var j0 = new Jugador(
            new Ident(),
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        j0.AgregarBarco(new Coord(0, 0), new Coord(2, 0));

        var j1 = new Jugador(
            new Ident(),
            new Tablero(),
            null,
            1,
            new Estadistica()
        );

        j1.AgregarBarco(new Coord(0, 1), new Coord(0, 2));

        Assert.IsTrue(j0.SigueEnJuego);
        Assert.IsTrue(j0.SigueEnJuegoReloj);

        Assert.Contains(2, j0.BarcosFaltantes);
        Assert.Contains(4, j0.BarcosFaltantes);
        Assert.Contains(5, j0.BarcosFaltantes);

        Assert.AreEqual(ResultadoAtaque.Tocado, j0.AtacarJugador(j1, new Coord(0, 1)));
        Assert.AreEqual(1, j0.Estadistica.Aciertos);
        Assert.AreEqual(ResultadoAtaque.Agua, j0.AtacarJugador(j1, new Coord(5, 5)));
        Assert.AreEqual(1, j0.Estadistica.Fallos);
        Assert.AreEqual(ResultadoAtaque.Hundido, j0.AtacarJugador(j1, new Coord(0, 2)));
        Assert.AreEqual(1, j0.Estadistica.Hundidos);
        Assert.AreEqual(1, j0.Estadistica.Victorias);
        Assert.AreEqual(1, j1.Estadistica.Derrotas);
    }
}
