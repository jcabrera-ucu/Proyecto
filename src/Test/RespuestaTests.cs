using System;
using NUnit.Framework;
using Library;

namespace Test;

public class RespuestaTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ConstruccionCorrecta()
    {
        var id0 = new Ident();
        var id1 = new Ident();
        var r = new Respuesta("remitente", "oponente", id0, id1);

        Assert.AreEqual("remitente", r.Remitente);
        Assert.AreEqual("oponente", r.Oponente);
        Assert.AreEqual(id0, r.IdRemitente);
        Assert.AreEqual(id1, r.IdOponente);
    }
}

