using System.IO;
using NUnit.Framework;
using Library;

namespace Test;

public class HistóricoTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Pruebas()
    {
        {
            File.Delete("bdd.json");

            var h = new HistóricoEstadísticas("bdd.json");

            Assert.AreEqual(0, h.Estadísticas.Count);

            var i0 = new Ident("0");

            var e0 = h.ObtenerEstadística(i0);

            e0.Derrotas = 15;

            h.Guardar();
        }

        {
            var h = new HistóricoEstadísticas("bdd.json");

            var i0 = new Ident("0");
            var e0 = h.ObtenerEstadística(i0);

            Assert.AreEqual(15, e0.Derrotas);
        }
    }
}
