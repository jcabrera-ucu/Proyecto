using NUnit.Framework;
using Library;

namespace Test;

public class CoordTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(10, 0)]
    [TestCase(0, 15)]
    [TestCase(25, 25)]
    public void CreaciónCorrectaDeCoords(int x, int y)
    {
        var coord = new Coord(x, y);

        Assert.AreEqual(x, coord.X);
        Assert.AreEqual(y, coord.Y);
    }

    [Test]
    [TestCase(-1, 0)]
    [TestCase(-1, 5)]
    [TestCase(0, -1)]
    [TestCase(5, -1)]
    [TestCase(-1, -1)]
    [TestCase(26, 0)]
    [TestCase(0, 26)]
    public void CreaciónIncorrectaDeCoords(int x, int y)
    {
        Assert.Throws<System.ArgumentOutOfRangeException>(() =>
        {
            var coord = new Coord(x, y);
        });
    }

    [Test]
    [TestCase(0, 0, "A01")]
    [TestCase(2, 4, "C05")]
    [TestCase(10, 11, "K12")]
    public void Alfanuméricos(int x, int y, string alfanumérico)
    {
        var coord = new Coord(x, y);
        Assert.AreEqual(alfanumérico, coord.ToAlfanumérico());
    }
}