 using NUnit.Framework;
using Library;

namespace Test;

public class JugarConHandlerTest
{

    [SetUp]
    public void Setup()
    {

    }
 [Test]
    public void JugarConRobotinaTest()
    {
        var batalla = new BatallaNaval();

        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        {
            var res = batalla.ProcesarMensaje(new Message(
                "jugar",
                idJugadorA,
                "Jugador A"
            ));

            Assert.That(res.Remitente, Contains.Substring("Tu oponente es: Robotina"));
        }
    }
}