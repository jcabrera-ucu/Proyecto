using NUnit.Framework;
using Library;

namespace Test;

public class BarcoTests
{
    [SetUp]
    public void Setup()
    {
    }

    static object[] noIntersectanTestCases = {
        // Pegados horizontalmente
        new Barco[] {
            new Barco(new Coord(10, 10), new Coord(15, 10)),
            new Barco(new Coord(5, 10), new Coord(9, 10)),
        },

        // Pegados horizontalmente (uno en 0)
        new Barco[] {
            new Barco(new Coord(0, 0), new Coord(5, 0)),
            new Barco(new Coord(6, 0), new Coord(10, 0)),
        },

        // Pegados horizontalmente de tamaño 1
        new Barco[] {
            new Barco(new Coord(0, 0), new Coord(0, 0)),
            new Barco(new Coord(1, 0), new Coord(1, 0)),
        },

        // Separados horizontalmente
        new Barco[] {
            new Barco(new Coord(0, 0), new Coord(5, 0)),
            new Barco(new Coord(10, 0), new Coord(10, 0)),
        },

        // Separados verticalmente
        new Barco[] {
            new Barco(new Coord(0, 0), new Coord(5, 0)),
            new Barco(new Coord(0, 5), new Coord(5, 5)),
        },

        // Verticales, pegados verticalmente
        new Barco[] {
            new Barco(new Coord(5, 0), new Coord(5, 5)),
            new Barco(new Coord(5, 6), new Coord(5, 10)),
        },

        // Verticales, pegados horizontalmente
        new Barco[] {
            new Barco(new Coord(5, 0), new Coord(5, 5)),
            new Barco(new Coord(6, 0), new Coord(6, 5)),
        },

        // Pegados verticalmente de tamaño 1
        new Barco[] {
            new Barco(new Coord(5, 5), new Coord(5, 5)),
            new Barco(new Coord(5, 6), new Coord(5, 6)),
        },
    };

    [Test]
    [TestCaseSource(nameof(noIntersectanTestCases))]
    public void BarcosNoIntersectan(Barco a, Barco b)
    {
        Assert.AreEqual(false, a.Intersecta(b));
        Assert.AreEqual(false, b.Intersecta(a));
    }

    static object[] intersectanTestCases = {
        // Horizontales, intersectan en uno final
        new Barco[] {
            new Barco(new Coord(10, 10), new Coord(15, 10)),
            new Barco(new Coord(15, 10), new Coord(16, 10)),
        },

        // Unitarios superpuestos
        new Barco[] {
            new Barco(new Coord(1, 1), new Coord(1, 1)),
            new Barco(new Coord(1, 1), new Coord(1, 1)),
        },

        // En cruz
        new Barco[] {
            new Barco(new Coord(10, 10), new Coord(15, 10)),
            new Barco(new Coord(13, 5), new Coord(13, 15)),
        },

        // En cruz, intersecta casilla final
        new Barco[] {
            new Barco(new Coord(10, 10), new Coord(15, 10)),
            new Barco(new Coord(13, 5), new Coord(13, 10)),
        },
    };

    [Test]
    [TestCaseSource(nameof(intersectanTestCases))]
    public void BarcosIntersectan(Barco a, Barco b)
    {
        Assert.AreEqual(true, a.Intersecta(b));
        Assert.AreEqual(true, b.Intersecta(a));
    }

    [Test]
    public void BarcoCount()
    {
        Assert.AreEqual(1, new Barco(new Coord(1, 0), new Coord(1, 0)).Largo);
        Assert.AreEqual(2, new Barco(new Coord(1, 0), new Coord(2, 0)).Largo);
        Assert.AreEqual(5, new Barco(new Coord(1, 0), new Coord(5, 0)).Largo);

        Assert.AreEqual(2, new Barco(new Coord(1, 0), new Coord(1, 1)).Largo);
        Assert.AreEqual(5, new Barco(new Coord(1, 0), new Coord(1, 4)).Largo);
    }

    [Test]
    public void Golpear()
    {
        var barco = new Barco(new Coord(1, 1), new Coord(2, 1));

        Assert.AreEqual(false, barco.Golpear(new Coord(0, 0)));
        Assert.AreEqual(0, barco.Golpes.Count);
        Assert.AreEqual(false, barco.Hundido);

        Assert.AreEqual(true, barco.Golpear(new Coord(1, 1)));
        Assert.AreEqual(1, barco.Golpes.Count);
        Assert.AreEqual(false, barco.Hundido);

        Assert.AreEqual(true, barco.Golpear(new Coord(2, 1)));
        Assert.AreEqual(2, barco.Golpes.Count);
        Assert.AreEqual(true, barco.Hundido);

        Assert.AreEqual(false, barco.Golpear(new Coord(0, 0)));
        Assert.AreEqual(2, barco.Golpes.Count);
        Assert.AreEqual(true, barco.Hundido);

        Assert.AreEqual(true, barco.Golpear(new Coord(2, 1)));
        Assert.AreEqual(2, barco.Golpes.Count);
        Assert.AreEqual(true, barco.Hundido);
    }

    [Test]
    public void Revelar()
    {
        var barco = new Barco(new Coord(1, 1), new Coord(2, 1));

        Assert.AreEqual(false, barco.Revelar(new Coord(0, 0)));
        Assert.AreEqual(0, barco.Revelados.Count);

        Assert.AreEqual(true, barco.Revelar(new Coord(1, 1)));
        Assert.AreEqual(1, barco.Revelados.Count);

        Assert.AreEqual(false, barco.Revelar(new Coord(4, 0)));
        Assert.AreEqual(1, barco.Revelados.Count);

        Assert.AreEqual(true, barco.Revelar(new Coord(2, 1)));
        Assert.AreEqual(2, barco.Revelados.Count);
    }

    [Test]
    public void ToStringTest()
    {
        var barco = new Barco(new Coord(1, 1), new Coord(2, 1));

        Assert.AreEqual("B02-C02", barco.ToString());
    }
}
