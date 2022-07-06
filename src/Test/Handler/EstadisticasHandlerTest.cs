using NUnit.Framework;
using Library;

namespace Test;

public class EstadisticasHandlerTest
{

    [SetUp]
    public void Setup()
    {

    }
 [Test]
    public void EstadisticaHandlerTest()
    {
        var batalla = new BatallaNaval();

        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        {
            var res = batalla.ProcesarMensaje(new Message(
                "stats",
                idJugadorA,
                "Jugador A"
            ));

            Assert.That(res.Remitente, Contains.Substring("Estadísticas:"));
        }
    }
}