using NUnit.Framework;
using Library;

namespace Test;

public class JugadaTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Pruebas()
    {
        var j = new Jugada(new Ident("A"), TipoJugada.Ataque, new Coord(2, 2));

        Assert.AreEqual(new Ident("A"), j.Id);
        Assert.AreEqual(TipoJugada.Ataque, j.Tipo);
        Assert.AreEqual(new Coord(2, 2), j.Coordenada);
    }
}
