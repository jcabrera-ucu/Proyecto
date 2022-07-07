using NUnit.Framework;
using Library;

namespace Test;

public class TotalAciertosHandlerTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ErrarleNoAgregaAciertos()
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

        // Jugador A, no hay aciertos
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 0", res.Remitente);
        }

        // Jugador B, no hay aciertos
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 0", res.Remitente);
        }

        batalla.ProcesarMensaje(new Message("a e1", idJugadorA, "A"));

        // Jugador A, No se agrega ningún "acierto"
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 0", res.Remitente);
        }

        // Jugador B, No se agrega ningún "acierto"
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 0", res.Remitente);
        }

        batalla.ProcesarMensaje(new Message("a e1", idJugadorB, "B"));

        // Jugador A, No se agrega ningún "acierto"
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 0", res.Remitente);
        }

        // Jugador B, No se agrega ningún "acierto"
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 0", res.Remitente);
        }
    }

    [Test]
    public void EmbocarleAgregaAciertos()
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

        // Jugador A, no hay aciertos
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 0", res.Remitente);
        }

        // Jugador B, no hay aciertos
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 0", res.Remitente);
        }

        batalla.ProcesarMensaje(new Message("a a1", idJugadorA, "A"));

        // Jugador A, un acierto
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 1", res.Remitente);
        }

        // Jugador B, un acierto
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 1", res.Remitente);
        }

        // Errarle no agrega un acierto
        batalla.ProcesarMensaje(new Message("a h1", idJugadorB, "B"));

        // Jugador A, un acierto
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 1", res.Remitente);
        }

        // Jugador B, un acierto
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 1", res.Remitente);
        }

        // Hundido agrega un acierto
        batalla.ProcesarMensaje(new Message("a b2", idJugadorA, "A"));

        // Jugador A, dos acierto
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 2", res.Remitente);
        }

        // Jugador B, dos acierto
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 2", res.Remitente);
        }

        // Radar no agrega un acierto
        batalla.ProcesarMensaje(new Message("radar b2", idJugadorB, "B"));

        // Jugador A, dos acierto
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorA, "A"));

            Assert.AreEqual("Total de disparos certeros: 2", res.Remitente);
        }

        // Jugador B, dos acierto
        {
            var res = batalla.ProcesarMensaje(new Message("total aciertos", idJugadorB, "B"));

            Assert.AreEqual("Total de disparos certeros: 2", res.Remitente);
        }
    }
}
