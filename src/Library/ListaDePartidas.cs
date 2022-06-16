namespace Library;

/// <summary>
/// 
/// </summary>
public class ListaDePartidas
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Usuario"></typeparam>
    /// <typeparam name="ControladorJuego"></typeparam>
    /// <returns></returns>
    IDictionary<Usuario, ControladorJuego> Partidas = new Dictionary<Usuario, ControladorJuego>();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="usuario1"></param>
    /// <param name="usuario2"></param>
    /// <param name="controladorJuego"></param>
    public void AgregarPartida(Usuario usuario1, Usuario usuario2, ControladorJuego controladorJuego)
    {
        Partidas.Add(usuario1, controladorJuego);
        Partidas.Add(usuario2, controladorJuego);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns></returns>
    public ControladorJuego? ObtenerPartida(Usuario usuario)
    {

        if (Partidas.ContainsKey(usuario))
        {
            return Partidas[usuario];
        }

        return null;
    }
}