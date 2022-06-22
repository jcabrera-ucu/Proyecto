using System;
using NUnit.Framework;
using Library;

namespace Test;

public class RelojTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Pruebas()
    {
        var now = DateTime.UtcNow;

        var r = new Reloj(
            TimeSpan.FromSeconds(10),
            () =>
            {
                now += TimeSpan.FromSeconds(1);
                return now;
            }
        );

        Assert.AreEqual(TimeSpan.FromSeconds(10), r.TiempoRestante);
        r.Iniciar();
        Assert.AreEqual(now, r.Inicio);
        r.Terminar();
        Assert.AreEqual(TimeSpan.FromSeconds(9), r.TiempoRestante);
    }
}
