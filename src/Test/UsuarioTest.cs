using NUnit.Framework;
using Library;

namespace Test;

public class UsuarioTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Pruebas()
    {
        var e = new Estadistica();
        var u = new Usuario
        {
            Id = new Ident("u"),
            Estadisticas = e,
            Nombre = "Hola"
        };

        Assert.AreEqual(new Ident("u"), u.Id);
        Assert.AreEqual(e, u.Estadisticas);
        Assert.AreEqual("Hola", u.Nombre);
    }
}
