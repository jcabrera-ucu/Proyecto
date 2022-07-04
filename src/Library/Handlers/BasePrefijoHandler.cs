namespace Library;

/// <summary>
///
/// </summary>
public abstract class BasePrefijoHandler : BaseHandler
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="next"></param>
    public BasePrefijoHandler(IHandler? next)
        : base(next)
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="keywords"></param>
    /// <param name="next"></param>
    public BasePrefijoHandler(string[] keywords, IHandler? next)
        : base(keywords, next)
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    protected override bool CanHandle(Message message)
    {
        // Obtiene la primer palabra del texto.
        var text = message.Text.Split(' ').FirstOrDefault(String.Empty);

        return base.CanHandle(message with
        {
            Text = text
        });
    }
}
