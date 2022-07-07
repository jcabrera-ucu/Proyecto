using NUnit.Framework;
using Library;

namespace Test;

public class TotalFallosHandlerTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void EmbocarleNoAgregaFallos()
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

        // Jugador A, no hay fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos al agua: 0", res.Remitente);
        }

        // Jugador B, no hay fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos al agua: 0", res.Remitente);
        }

        batalla.ProcesarMensaje(new Message("a e1", idJugadorA, "A"));

        // Jugador A, se agrega un fallo
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos al agua: 1", res.Remitente);
        }

        // Jugador B, se agrega un fallo
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos al agua: 1", res.Remitente);
        }

        batalla.ProcesarMensaje(new Message("a a1", idJugadorB, "B"));

        // Jugador A, No se agrega ningún "fallo"
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos al agua: 1", res.Remitente);
        }

        // Jugador B, No se agrega ningún "fallo"
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos al agua: 1", res.Remitente);
        }
    }

    [Test]
    public void ErrarleAgregaFallos()
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

        // Jugador A, no hay fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos al agua: 0", res.Remitente);
        }

        // Jugador B, no hay fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos al agua: 0", res.Remitente);
        }

        // Embocarle no agrega un fallo
        batalla.ProcesarMensaje(new Message("a a1", idJugadorA, "A"));

        // Jugador A, no hay fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos al agua: 0", res.Remitente);
        }

        // Jugador B, no hay fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos al agua: 0", res.Remitente);
        }

        // Errarle agrega un fallo
        batalla.ProcesarMensaje(new Message("a h1", idJugadorB, "B"));

        // Jugador A, un fallo
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos al agua: 1", res.Remitente);
        }

        // Jugador B, un fallo
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos al agua: 1", res.Remitente);
        }

        batalla.ProcesarMensaje(new Message("a h1", idJugadorA, "A"));
        batalla.ProcesarMensaje(new Message("a h2", idJugadorB, "B"));
        batalla.ProcesarMensaje(new Message("a c8", idJugadorA, "A"));

        // Jugador A, cuatro fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos al agua: 4", res.Remitente);
        }

        // Jugador B, cuatro fallos
        {
            var res = batalla.ProcesarMensaje(new Message("total fallos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos al agua: 4", res.Remitente);
        }
    }
}
