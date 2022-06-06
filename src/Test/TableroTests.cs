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
}
