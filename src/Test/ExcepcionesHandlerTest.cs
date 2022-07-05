using NUnit.Framework;
using Library;

namespace Test;

public class ExcepcionesHandlerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EstadoPartidaIncorrectoTest()
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

        {
            var res = batalla.ProcesarMensaje(new Message(
                "atacar a1",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("No puede realizar esa acción"));
        }
    }
    [Test]
    public void AtacarTurnoIncorrecto()
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
                "atacar a10",
                idJugadorB,
                "Jugador B"
            ));

            Assert.AreEqual(idJugadorB, res.IdRemitente);
            Assert.That(res.Remitente, Contains.Substring("No puede realizar esa acción"));
        }
    }
    [Test]
    public void AgregarBarcoFueraDeRango()
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

        {
            var res = batalla.ProcesarMensaje(new Message(
                "agregar a10 a11",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("Coordenada fuera del tablero"));
            Assert.That(res.Remitente, Contains.Substring("A11"));
        }

    }
    [Test]
    public void CoordFormatoIncorrectoTest()
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

        {
            var res = batalla.ProcesarMensaje(new Message(
                "agregar 10a a11",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("Error de sintaxis"));
            Assert.That(res.Remitente, Contains.Substring("10a"));
        }

    }
    [Test]
    public void SinRadaresTest()
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
            "radar c4",
            idJugadorA,
            "Jugador A"
        ));

        batalla.ProcesarMensaje(new Message(
                   "radar c4",
                   idJugadorB,
                   "Jugador B"
               ));

        {
            var res = batalla.ProcesarMensaje(new Message(
                "radar c5",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.That(res.Remitente, Contains.Substring("Ya no te quedan radares"));
        }
    }
    [Test]
    public void CoordNoAlineadaTest()
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

        {
            var res = batalla.ProcesarMensaje(new Message(
                "agregar a1 c2",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("Las coordenadas no están en línea recta"));
            Assert.That(res.Remitente, Contains.Substring("A01"));
            Assert.That(res.Remitente, Contains.Substring("C02"));
        }

    }
    [Test]
    public void BarcosSuperpuestosTest()
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
            "agregar b1 b3",
            idJugadorA,
            "Jugador A"
        ));
        {
            var res = batalla.ProcesarMensaje(new Message(
                "agregar b1 b4",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("El barco que está intentando agregar se superpone con otro"));
            Assert.That(res.Remitente, Contains.Substring("B01"));
            Assert.That(res.Remitente, Contains.Substring("B04"));
        }
    }
    [Test]
     public void BarcoYaExisteTest()
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
            "agregar b1 b3",
            idJugadorA,
            "Jugador A"
        ));
        {
            var res = batalla.ProcesarMensaje(new Message(
                "agregar a1 a3",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("Ya existe un barco de tamaño"));
            Assert.That(res.Remitente, Contains.Substring("3"));
        }
    }
      [Test]
     public void BarcoLargoIncorrecto()
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
        {
            var res = batalla.ProcesarMensaje(new Message(
                "agregar a1 a7",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("No se puede agregar un barco de tamaño"));
            Assert.That(res.Remitente, Contains.Substring("7"));
        }
    }
}



