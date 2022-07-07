using NUnit.Framework;
using Library;

namespace Test;

public class TotalDeAciertosYFallosTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void SimulaciónPartidaConAciertosYFallos()
    {
        var batalla = new BatallaNaval();

        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        batalla.ProcesarMensaje(new Message("buscar", idJugadorA, "A"));
        batalla.ProcesarMensaje(new Message("buscar", idJugadorB, "B"));

        batalla.ProcesarMensaje(new Message("agregar a1 a2", idJugadorA, "A"));
        batalla.ProcesarMensaje(new Message("agregar b1 b3", idJugadorA, "A"));
        batalla.ProcesarMensaje(new Message("agregar c1 c4", idJugadorA, "A"));
        batalla.ProcesarMensaje(new Message("agregar d1 d5", idJugadorA, "A"));

        batalla.ProcesarMensaje(new Message("agregar a1 a2", idJugadorB, "B"));
        batalla.ProcesarMensaje(new Message("agregar b1 b3", idJugadorB, "B"));
        batalla.ProcesarMensaje(new Message("agregar c1 c4", idJugadorB, "B"));
        batalla.ProcesarMensaje(new Message("agregar d1 d5", idJugadorB, "B"));

        batalla.ProcesarMensaje(new Message("atacar e1", idJugadorA, "A")); // fallo
        batalla.ProcesarMensaje(new Message("atacar a1", idJugadorB, "B")); // acierto
        batalla.ProcesarMensaje(new Message("atacar b2", idJugadorA, "A")); // acierto
        batalla.ProcesarMensaje(new Message("atacar h1", idJugadorB, "B")); // fallo

        batalla.ProcesarMensaje(new Message("atacar b1", idJugadorA, "A")); // acierto
        batalla.ProcesarMensaje(new Message("atacar a2", idJugadorB, "B")); // acierto
        batalla.ProcesarMensaje(new Message("atacar b8", idJugadorA, "A")); // fallo
        batalla.ProcesarMensaje(new Message("atacar h2", idJugadorB, "B")); // fallo

        // Fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos al agua: 4", res.Remitente);
        }

        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos al agua: 4", res.Remitente);
        }

        // Aciertos
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 4", res.Remitente);
        }

        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 4", res.Remitente);
        }
    }
}
