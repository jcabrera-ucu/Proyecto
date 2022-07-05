using NUnit.Framework;
using Library;

namespace Test;

public class BatallaNavalTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NoHayPartidaActiva()
    {
        var batalla = new BatallaNaval();

        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        {
            var res = batalla.ProcesarMensaje(new Message(
                "atacar a1",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.IsNull(res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("ninguna partida activa"));
        }
    }

    [Test]
    public void ComienzaElBot()
    {
        var batalla = new BatallaNaval();

        var idJugadorA = new Ident();

        {
            var res = batalla.ProcesarMensaje(new Message(
                "/start",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.IsNull(res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("Bienvenido a"));
            Assert.That(res.Remitente, Contains.Substring("escriba: menu"));
        }
    }

    [Test]
    public void BuscarPartida()
    {
        var batalla = new BatallaNaval();

        var idJugadorA = new Ident();
        var idJugadorB = new Ident();

        {
            var res = batalla.ProcesarMensaje(new Message(
                "buscar",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.IsNull(res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("Esperando un oponente"));
        }

        {
            var res = batalla.ProcesarMensaje(new Message(
                "buscar",
                idJugadorB,
                "Jugador B"
            ));

            Assert.AreEqual(idJugadorB, res.IdRemitente);
            Assert.AreEqual(idJugadorA, res.IdOponente);
            Assert.That(res.Remitente, Contains.Substring("Jugador A"));
            Assert.That(res.Remitente, Contains.Substring("Que comience la batalla"));
            Assert.That(res.Oponente, Contains.Substring("Jugador B"));
            Assert.That(res.Oponente, Contains.Substring("Que comience la batalla"));
        }
    }

    [Test]
    public void AgregarBarcos()
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
                "agregar a1 a2",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("Barco agregado"));
            Assert.That(res.Remitente, Contains.Substring("A01"));
            Assert.That(res.Remitente, Contains.Substring("A02"));
        }

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

        {
            var res = batalla.ProcesarMensaje(new Message(
                "agregar d1 d5",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.IsEmpty(res.Oponente);
            Assert.That(res.Remitente, Contains.Substring("Barco agregado"));
            Assert.That(res.Remitente, Contains.Substring("D01"));
            Assert.That(res.Remitente, Contains.Substring("D05"));
            Assert.That(res.Remitente, Contains.Substring("que tu oponente termine"));
        }

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

        {
            var res = batalla.ProcesarMensaje(new Message(
                "agregar d1 d5",
                idJugadorB,
                "Jugador B"
            ));

            Assert.AreEqual(idJugadorB, res.IdRemitente);
            Assert.AreEqual(idJugadorA, res.IdOponente);
            Assert.That(res.Oponente, Contains.Substring("Es tu turno"));
            Assert.That(res.Remitente, Contains.Substring("Barco agregado"));
            Assert.That(res.Remitente, Contains.Substring("D01"));
            Assert.That(res.Remitente, Contains.Substring("D05"));
            Assert.That(res.Remitente, Contains.Substring("turno de tu oponente"));
        }
    }

    [Test]
    public void Ataque()
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
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.That(res.Remitente, Contains.Substring("A10"));
            Assert.That(res.Remitente, Contains.Substring("Agua"));
            Assert.That(res.Remitente, Contains.Substring("turno de tu oponente"));
            Assert.That(res.Oponente, Contains.Substring("A10"));
            Assert.That(res.Oponente, Contains.Substring("Agua"));
            Assert.That(res.Oponente, Contains.Substring("Es tu turno"));
        }

        {
            var res = batalla.ProcesarMensaje(new Message(
                "atacar a2",
                idJugadorB,
                "Jugador B"
            ));

            Assert.AreEqual(idJugadorB, res.IdRemitente);
            Assert.AreEqual(idJugadorA, res.IdOponente);
            Assert.That(res.Remitente, Contains.Substring("A02"));
            Assert.That(res.Remitente, Contains.Substring("Tocado"));
            Assert.That(res.Remitente, Contains.Substring("turno de tu oponente"));
            Assert.That(res.Oponente, Contains.Substring("A02"));
            Assert.That(res.Oponente, Contains.Substring("Tocado"));
            Assert.That(res.Oponente, Contains.Substring("Es tu turno"));
        }

        {
            var res = batalla.ProcesarMensaje(new Message(
                "atacar c2",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.That(res.Remitente, Contains.Substring("C02"));
            Assert.That(res.Remitente, Contains.Substring("Tocado"));
            Assert.That(res.Remitente, Contains.Substring("turno de tu oponente"));
            Assert.That(res.Oponente, Contains.Substring("C02"));
            Assert.That(res.Oponente, Contains.Substring("Tocado"));
            Assert.That(res.Oponente, Contains.Substring("Es tu turno"));
        }

        {
            var res = batalla.ProcesarMensaje(new Message(
                "atacar a1",
                idJugadorB,
                "Jugador B"
            ));

            Assert.AreEqual(idJugadorB, res.IdRemitente);
            Assert.AreEqual(idJugadorA, res.IdOponente);
            Assert.That(res.Remitente, Contains.Substring("A01"));
            Assert.That(res.Remitente, Contains.Substring("Hundido"));
            Assert.That(res.Remitente, Contains.Substring("turno de tu oponente"));
            Assert.That(res.Oponente, Contains.Substring("A01"));
            Assert.That(res.Oponente, Contains.Substring("Hundido"));
            Assert.That(res.Oponente, Contains.Substring("Es tu turno"));
        }
    }

    [Test]
    public void Radar()
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
                "radar c4",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.That(res.Remitente, Contains.Substring("C04"));
            Assert.That(res.Remitente, Contains.Substring("Radar desplegado"));
            Assert.That(res.Remitente, Contains.Substring("turno de tu oponente"));
            Assert.That(res.Oponente, Contains.Substring("C04"));
            Assert.That(res.Oponente, Contains.Substring("Desplegaron un radar en"));
            Assert.That(res.Oponente, Contains.Substring("Es tu turno"));
        }
    }
    [Test]
    public void GanadorPerdedorTest()
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
            "atacar a1",
            idJugadorA,
            "Jugador A"
        ));

        batalla.ProcesarMensaje(new Message(
            "atacar a1",
            idJugadorB,
            "Jugador B"
        ));

        batalla.ProcesarMensaje(new Message(
            "atacar a2",
            idJugadorA,
            "Jugador A"
        ));
        batalla.ProcesarMensaje(new Message(
            "atacar a2",
            idJugadorB,
            "Jugador B"
        ));
        batalla.ProcesarMensaje(new Message(
           "atacar c1",
           idJugadorA,
           "Jugador A"
       ));
        batalla.ProcesarMensaje(new Message(
      "atacar c1",
      idJugadorB,
      "Jugador B"
        ));
        batalla.ProcesarMensaje(new Message(
           "atacar c2",
           idJugadorA,
           "Jugador A"
       ));
        batalla.ProcesarMensaje(new Message(
         "atacar c2",
         idJugadorB,
         "Jugador B"
        ));
        batalla.ProcesarMensaje(new Message(
          "atacar c3",
          idJugadorA,
          "Jugador A"
        ));
        batalla.ProcesarMensaje(new Message(
          "atacar c3",
          idJugadorB,
          "Jugador B"
        ));
        batalla.ProcesarMensaje(new Message(
            "atacar c4",
        idJugadorA,
        "Jugador A"
        ));
        batalla.ProcesarMensaje(new Message(
           "atacar c4",
           idJugadorB,
           "Jugador B"
       ));
        batalla.ProcesarMensaje(new Message(
            "atacar b1",
        idJugadorA,
        "Jugador A"
        ));
        batalla.ProcesarMensaje(new Message(
         "atacar b1",
         idJugadorB,
         "Jugador B"
     ));
        batalla.ProcesarMensaje(new Message(
     "atacar b2",
 idJugadorA,
 "Jugador A"
 ));
        batalla.ProcesarMensaje(new Message(
          "atacar b2",
          idJugadorB,
          "Jugador B"
      ));
        batalla.ProcesarMensaje(new Message(
            "atacar b3",
        idJugadorA,
        "Jugador A"
        ));
        batalla.ProcesarMensaje(new Message(
          "atacar b3",
          idJugadorB,
          "Jugador B"
      ));
        //Cut
        batalla.ProcesarMensaje(new Message(
        "atacar d1",
        idJugadorA,
        "Jugador A"
      ));
        batalla.ProcesarMensaje(new Message(
          "atacar d1",
          idJugadorB,
          "Jugador B"
        ));
        batalla.ProcesarMensaje(new Message(
            "atacar d2",
        idJugadorA,
        "Jugador A"
        ));
        batalla.ProcesarMensaje(new Message(
           "atacar d2",
           idJugadorB,
           "Jugador B"
       ));
        batalla.ProcesarMensaje(new Message(
            "atacar d3",
        idJugadorA,
        "Jugador A"
        ));
        batalla.ProcesarMensaje(new Message(
         "atacar d3",
         idJugadorB,
         "Jugador B"
     ));
        batalla.ProcesarMensaje(new Message(
     "atacar d4",
 idJugadorA,
 "Jugador A"
 ));
        batalla.ProcesarMensaje(new Message(
          "atacar d4",
          idJugadorB,
          "Jugador B"
      ));
        {
            var res = batalla.ProcesarMensaje(new Message(
                "atacar d5",
                idJugadorA,
                "Jugador A"
            ));

            Assert.AreEqual(idJugadorA, res.IdRemitente);
            Assert.AreEqual(idJugadorB, res.IdOponente);
            Assert.That(res.Remitente, Contains.Substring("Ganaste"));
            Assert.That(res.Oponente, Contains.Substring("Perdiste"));
        }

    }
}
