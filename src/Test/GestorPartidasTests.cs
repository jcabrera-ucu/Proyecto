using NUnit.Framework;
using Library;

namespace Test;

public class GestorPartidasTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaBúsqueda()
    {
        var g = new GestorPartidas();

        var u0 = new Usuario
        {
            Id = new Ident(),
        };

        var u1 = new Usuario
        {
            Id = new Ident(),
        };

        Assert.IsNull(g.BuscarNuevaPartida(u0, false));
        Assert.IsNotNull(g.BuscarNuevaPartida(u1, false));
    }

    [Test]
    public void PruebaBúsquedaReloj()
    {
        var g = new GestorPartidas();

        var u0 = new Usuario
        {
            Id = new Ident(),
        };

        var u1 = new Usuario
        {
            Id = new Ident(),
        };

        Assert.IsNull(g.BuscarNuevaPartida(u0, true));
        Assert.IsNotNull(g.BuscarNuevaPartida(u1, true));
    }

    [Test]
    public void PruebaBúsquedaMezclada()
    {
        var g = new GestorPartidas();

        var u0 = new Usuario
        {
            Id = new Ident(),
        };

        var u1 = new Usuario
        {
            Id = new Ident(),
        };

        var u2 = new Usuario
        {
            Id = new Ident(),
        };

        var u3 = new Usuario
        {
            Id = new Ident(),
        };

        Assert.IsNull(g.BuscarNuevaPartida(u0, false));
        Assert.IsNull(g.BuscarNuevaPartida(u1, true));
        Assert.IsNotNull(g.BuscarNuevaPartida(u2, false));
        Assert.IsNotNull(g.BuscarNuevaPartida(u3, true));
    }
}
