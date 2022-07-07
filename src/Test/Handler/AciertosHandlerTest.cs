using NUnit.Framework;
using Library;

namespace Test;

public class AciertosHandlerTest
{

    [SetUp]
    public void Setup()
    {

    }
    [Test]
    public void AciertosSumaTest()
    {
        var batalla = new BatallaNaval();

        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

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

        batalla.ProcesarMensaje(new Message(
                       "atacar a10",
                       idJugadorA,
                       "Jugador A"
                   ));
        {
            var res = batalla.ProcesarMensaje(new Message(
                "Aciertos",
                idJugadorA,
                "Jugador A"
            ));

            Assert.That(res.Remitente, Contains.Substring("En esta partida se han hecho 0 ataques que han resultado en un acierto"));

        }
        batalla.ProcesarMensaje(new Message(
                "atacar a2",
                idJugadorB,
                "Jugador B"
            ));
        {
            var res = batalla.ProcesarMensaje(new Message(
                "Aciertos",
                idJugadorA,
                "Jugador A"
            ));
            Assert.That(res.Remitente, Contains.Substring("En esta partida se han hecho 1 ataques que han resultado en un acierto"));

        }

        batalla.ProcesarMensaje(new Message(
                        "atacar c2",
                        idJugadorA,
                        "Jugador A"
                    ));
        {
            var res = batalla.ProcesarMensaje(new Message(
                "Aciertos",
                idJugadorA,
                "Jugador A"
            ));
            Assert.That(res.Remitente, Contains.Substring("En esta partida se han hecho 2 ataques que han resultado en un acierto"));

        }
        {
            var res = batalla.ProcesarMensaje(new Message(
                "Aciertos",
                idJugadorB,
                "Jugador B"
            ));
            Assert.That(res.Remitente, Contains.Substring("En esta partida se han hecho 2 ataques que han resultado en un acierto"));

        }
    }
}