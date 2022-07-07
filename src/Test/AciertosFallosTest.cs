using NUnit.Framework;
using Library;

namespace Test;

public class AciertosFallosTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Pruebas()
    {
        var j = new AciertosFallos();
        Assert.AreEqual(0, j.Aciertos);
        Assert.AreEqual(0, j.Fallos);

        j.Aciertos++;
        j.Fallos++;

        Assert.AreEqual(1, j.Aciertos);
        Assert.AreEqual(1, j.Fallos);
    }
}