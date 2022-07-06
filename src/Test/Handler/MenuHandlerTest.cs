using NUnit.Framework;
using Library;

namespace Test;

public class MenuHandlerTest
{

    [SetUp]
    public void Setup()
    {

    }


    [Test]
    public void DistintasFacesMenuHandlerTest()
    {
        var batalla = new BatallaNaval();

        var idJugadorA = new Ident();
        var idJugadorB = new Ident();
        {
            var res = batalla.ProcesarMensaje(new Message(
                "Menu",
                idJugadorA,
                "Jugador A"
            ));

            Assert.That(res.Remitente, Contains.Substring("Puede utilizar los siguientes comandos"));
            Assert.That(res.Remitente, Contains.Substring("menu"));
            Assert.That(res.Remitente, Contains.Substring("estadisticas"));
            Assert.That(res.Remitente, Contains.Substring("buscar"));
            Assert.That(res.Remitente, Contains.Substring("jugar"));


        }

        batalla.ProcesarMensaje(new Message(
            "buscar",
            idJugadorA,
            "Jugador A"
        ));

        batalla.ProcesarMensaje(new Message(
            "buscar",
            idJugadorB,
            "Jugador B"
        ));

        {
            var res = batalla.ProcesarMensaje(new Message(
                "Menu",
                idJugadorA,
                "Jugador A"
            ));

            Assert.That(res.Remitente, Contains.Substring("Puede utilizar los siguientes comandos"));
            Assert.That(res.Remitente, Contains.Substring("agregar"));
            Assert.That(res.Remitente, Contains.Substring("tablero"));
            Assert.That(res.Remitente, Contains.Substring("jugadas"));

        }

        batalla.ProcesarMensaje(new Message(
                "agregar a1 a2",
                idJugadorA,
                "Jugador A"
            ));

        batalla.ProcesarMensaje(new Message(
            "agregar b1 b3",
            idJugadorA,
            "Jugador A"
        ));

        batalla.ProcesarMensaje(new Message(
            "agregar c1 c4",
            idJugadorA,
            "Jugador A"
        ));


        batalla.ProcesarMensaje(new Message(
            "agregar d1 d5",
            idJugadorA,
            "Jugador A"
        ));


        batalla.ProcesarMensaje(new Message(
            "agregar a1 a2",
            idJugadorB,
            "Jugador B"
        ));

        batalla.ProcesarMensaje(new Message(
            "agregar b1 b3",
            idJugadorB,
            "Jugador B"
        ));

        batalla.ProcesarMensaje(new Message(
            "agregar c1 c4",
            idJugadorB,
            "Jugador B"
        ));


        batalla.ProcesarMensaje(new Message(
                "agregar d1 d5",
                idJugadorB,
                "Jugador B"
            ));

        {
            var res = batalla.ProcesarMensaje(new Message(
                "Menu",
                idJugadorA,
                "Jugador A"
            ));

            Assert.That(res.Remitente, Contains.Substring("Puede utilizar los siguientes comandos"));
            Assert.That(res.Remitente, Contains.Substring("atacar"));
            Assert.That(res.Remitente, Contains.Substring("radar"));

        }
        batalla.ProcesarMensaje(new Message(
                       "atacar d5",
                       idJugadorA,
                       "Jugador A"
                   ));
         {
            var res = batalla.ProcesarMensaje(new Message(
                "Menu",
                idJugadorA,
                "Jugador A"
            ));

            Assert.That(res.Remitente, Contains.Substring("Puede utilizar los siguientes comandos"));
            Assert.That(res.Remitente, !Contains.Substring("atacar"));
            Assert.That(res.Remitente, !Contains.Substring("radar"));

        }
    }

}