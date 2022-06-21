namespace Library;

public abstract class BasePrefijoHandler : BaseHandler
{
    public BasePrefijoHandler(IHandler? next)
        : base(next)
    {
    }

    public BasePrefijoHandler(string[] keywords, IHandler? next)
        : base(keywords, next)
    {
    }

    protected override bool CanHandle(Message message)
    {
        var text = message.Text.Split(' ').FirstOrDefault(String.Empty);
        return base.CanHandle(message with
        {
            Text = text
        });
    }
}
