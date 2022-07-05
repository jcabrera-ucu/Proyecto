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
    public void IdentSinValor()
    {
        var a = new Ident();

        Assert.AreEqual("", a.Value);
    }
}
