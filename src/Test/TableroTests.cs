using NUnit.Framework;
using Library;

namespace Test;

public class TableroTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(26, 26)]
    [TestCase(5, 1)]
    [TestCase(1, 26)]
    public void CreaciónCorrectaDeTableros(int ancho, int alto)
    {
        var t = new Tablero(ancho, alto);

        Assert.AreEqual(ancho, t.Ancho);
        Assert.AreEqual(alto, t.Alto);
        Assert.AreEqual(0, t.Barcos.Count);
        Assert.AreEqual(0, t.Agua.Count);
    }

    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 0)]
    [TestCase(1, 27)]
    [TestCase(27, 1)]
    public void CreaciónIncorrectaDeTableros(int ancho, int alto)
    {
        Assert.Throws<System.ArgumentOutOfRangeException>(() =>
        {
            var t = new Tablero(ancho, alto);
        });
    }

    [Test]
    [TestCase(0, 0, 4, 0)]
    [TestCase(0, 0, 0, 0)]
    [TestCase(2, 2, 2, 5)]
    public void CreaciónCorrectaDeBarcos(int x0, int y0, int x1, int y1)
    {
        var t = new Tablero();
        var c0 = new Coord(x0, y0);
        var c1 = new Coord(x1, y1);
        var b = t.AddBarco(c0, c1);

        Assert.AreEqual(c0, b.Primera);
        Assert.AreEqual(c1, b.Segunda);
    }

    [Test]
    [TestCase(1, 1, 1, 1)]
    [TestCase(0, 2, 6, 2)]
    public void CreaciónDeBarcosSuperpuestos(int x0, int y0, int x1, int y1)
    {
        var t = new Tablero();

        t.AddBarco(new Coord(1, 1), new Coord(1, 3));

        var c0 = new Coord(x0, y0);
        var c1 = new Coord(x1, y1);

        Assert.Throws<BarcosSuperpuestosException>(() =>
        {
            var b = t.AddBarco(c0, c1);
        });
    }

    [Test]
    public void CreaciónDeMúltiplesBarcos()
    {
        var t = new Tablero();

        t.AddBarco(new Coord(1, 1), new Coord(1, 3));
        t.AddBarco(new Coord(2, 3), new Coord(6, 3));
        t.AddBarco(new Coord(0, 0), new Coord(0, 0));

        Assert.AreEqual(3, t.Barcos.Count);
    }

    [Test]
    public void GetCeldasYFlujoBásicoDeUsuario()
    {
        var t = new Tablero(4, 4);

        var b1 = t.AddBarco(new Coord(0, 0), new Coord(0, 0));
        var b2 = t.AddBarco(new Coord(3, 1), new Coord(3, 3));
        var b3 = t.AddBarco(new Coord(1, 2), new Coord(2, 2));

        {
            var celdas = new Celda[,] {
                {Celda.Barco, Celda.Vacio, Celda.Vacio, Celda.Vacio},
                {Celda.Vacio, Celda.Vacio, Celda.Vacio, Celda.Barco},
                {Celda.Vacio, Celda.Barco, Celda.Barco, Celda.Barco},
                {Celda.Vacio, Celda.Vacio, Celda.Vacio, Celda.Barco},
            };

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Assert.AreEqual(celdas[y, x], t.GetCelda(new Coord(x, y)).celda);
                }
            }

            var barcos = new Barco?[,] {
                {  b1, null, null, null},
                {null, null, null,   b2},
                {null,   b3,   b3,   b2},
                {null, null, null,   b2},
            };

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Assert.AreEqual(barcos[y, x], t.GetCelda(new Coord(x, y)).barco);
                }
            }
        }

        Assert.AreEqual(ResultadoAtaque.Agua, t.Atacar(new Coord(1, 1)));
        Assert.AreEqual(ResultadoAtaque.Tocado, t.Atacar(new Coord(3, 1)));
        Assert.AreEqual(ResultadoAtaque.Hundido, t.Atacar(new Coord(0, 0)));

        t.Radar(new Coord(0, 2), new Coord(1, 3));

        {
            var celdas = new Celda[,] {
                {Celda.Tocado, Celda.Vacio,    Celda.Vacio, Celda.Vacio},
                {Celda.Vacio,  Celda.Agua,     Celda.Vacio, Celda.Tocado},
                {Celda.Agua,   Celda.Revelado, Celda.Barco, Celda.Barco},
                {Celda.Agua,   Celda.Agua,     Celda.Vacio, Celda.Barco},
            };

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Assert.AreEqual(celdas[y, x], t.GetCelda(new Coord(x, y)).celda);
                }
            }
        }
    }
}
