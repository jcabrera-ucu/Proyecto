using NUnit.Framework;
using Library;

namespace Test;

public class NullHandlerTest
{

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void TestEntraANull()
    {
        var handler = new InicioHandler(new NullHandler());
        var message = new Message();
        message.Text = "Texto Random";
        string response;
        string response2;

        IHandler? result = handler.Handle(message, out response, out response2);

        Assert.That(result, Is.Not.Null);
        Assert.That(response, Contains.Substring("Comando no reconocido"));
        Assert.AreEqual(string.Empty, response2);
    }
    [Test]
    public void TestNoEntraANull()
    {
        var handler = new InicioHandler(new NullHandler());
        var message = new Message();
        message.Text = "/start";
        string response;
        string response2;

        IHandler? result = handler.Handle(message, out response, out response2);

        Assert.That(result, Is.Not.Null);
        Assert.That(response, Contains.Substring("Bienvenido al bot"));
        Assert.AreEqual(string.Empty, response2);
    }
}
