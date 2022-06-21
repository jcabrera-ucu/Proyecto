namespace Library;

/// <summary>
///
/// </summary>
public class MaquinaDeEstado
{
    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public EstadoTelegram Estado { get; private set; }
    public MaquinaDeEstado()
    {
        Estado = EstadoTelegram.Inicio;
    }
    /// <summary>
    ///
    /// </summary>
    public void CambiarAMenu()
    {
        switch (Estado)
        {
            case EstadoTelegram.Inicio:
                Estado = EstadoTelegram.Menu;
                break;
            case EstadoTelegram.CreandoBot:
                Estado = EstadoTelegram.Menu;
                break;
            default:
                break;
        }
    }
    /// <summary>
    ///
    /// </summary>
    public void CambiarACreandoBot()
    {
        switch (Estado)
        {
            case EstadoTelegram.Menu:
                Estado = EstadoTelegram.CreandoBot;
                break;
            default:
                break;
        }
    }
    /// <summary>
    ///
    /// </summary>
    public void CambiarAJugando()
    {
        switch (Estado)
        {
            case EstadoTelegram.Menu:
                Estado = EstadoTelegram.Jugando;
                break;
            case EstadoTelegram.CreandoBot:
                Estado = EstadoTelegram.Jugando;
                break;
            default:
                break;
        }

    }
}
