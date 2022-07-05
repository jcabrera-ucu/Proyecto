using NUnit.Framework;
using Library;

namespace Test;

public class IdentTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void IdentConValor()
    {
        var i = new Ident("HOLA");

        Assert.AreEqual("HOLA", i.Value);
    }

    [Test]
    public void IdentAleatorio()
    {
        var a = new Ident();
        var b = new Ident();

        Assert.AreNotSame(a.Value, b.Value);
        Assert.AreNotSame("", a.Value);
        Assert.AreNotSame("", b.Value);
    }
}
