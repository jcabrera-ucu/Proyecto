using NUnit.Framework;
using Library;

namespace Test;

public class EstadisticaPartidaTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Construccion()
    {
        var e = new EstadisticaPartida();

        Assert.AreEqual(0, e.Aciertos);
        Assert.AreEqual(0, e.Fallos);
    }

    [Test]
    public void IncAciertos()
    {
        var e = new EstadisticaPartida();

        e.IncAciertos();

        Assert.AreEqual(1, e.Aciertos);
        Assert.AreEqual(0, e.Fallos);

        e.IncAciertos();

        Assert.AreEqual(2, e.Aciertos);
        Assert.AreEqual(0, e.Fallos);
    }

    [Test]
    public void IncFallos()
    {
        var e = new EstadisticaPartida();

        e.IncFallos();

        Assert.AreEqual(0, e.Aciertos);
        Assert.AreEqual(1, e.Fallos);

        e.IncFallos();

        Assert.AreEqual(0, e.Aciertos);
        Assert.AreEqual(2, e.Fallos);
    }

    [Test]
    public void IncCombinados()
    {
        var e = new EstadisticaPartida();

        e.IncFallos();

        Assert.AreEqual(0, e.Aciertos);
        Assert.AreEqual(1, e.Fallos);

        e.IncAciertos();

        Assert.AreEqual(1, e.Aciertos);
        Assert.AreEqual(1, e.Fallos);

        e.IncAciertos();

        Assert.AreEqual(2, e.Aciertos);
        Assert.AreEqual(1, e.Fallos);

        e.IncFallos();

        Assert.AreEqual(2, e.Aciertos);
        Assert.AreEqual(2, e.Fallos);
    }
}
