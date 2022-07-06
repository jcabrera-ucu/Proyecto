using NUnit.Framework;
using Library;

namespace Test;

public class TableroHandlerTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void PedirTablero()
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

        {
            var res = batalla.ProcesarMensaje(new Message(
                "tablero",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.That(res.Remitente, Contains.Substring(
                "    A B C D E F G H I J\n" +
                "01 |B|B|B|B| | | | | | |\n" +
                "02 |B|B|B|B| | | | | | |\n" +
                "03 | |B|B|B| | | | | | |\n" +
                "04 | | |B|B| | | | | | |\n" +
                "05 | | | |B| | | | | | |\n" +
                "06 | | | | | | | | | | |\n" +
                "07 | | | | | | | | | | |\n" +
                "08 | | | | | | | | | | |\n" +
                "09 | | | | | | | | | | |\n" +
                "10 | | | | | | | | | | |\n"
            ));
        }

        {
            var res = batalla.ProcesarMensaje(new Message(
                "jugadas",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.That(res.Remitente, Contains.Substring(
                "    A B C D E F G H I J\n" +
                "01 | | | | | | | | | | |\n" +
                "02 | | | | | | | | | | |\n" +
                "03 | | | | | | | | | | |\n" +
                "04 | | | | | | | | | | |\n" +
                "05 | | | | | | | | | | |\n" +
                "06 | | | | | | | | | | |\n" +
                "07 | | | | | | | | | | |\n" +
                "08 | | | | | | | | | | |\n" +
                "09 | | | | | | | | | | |\n" +
                "10 | | | | | | | | | | |\n"
            ));
        }
    }
}
