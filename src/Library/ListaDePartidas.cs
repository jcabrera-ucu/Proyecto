namespace Library;

public class ListaDePartidas
{
    public Usuario Usuario1 { get; }
    public Usuario Usuario2 { get; }
    IDictionary<Usuario, ControladorJuego> asociar = new Dictionary<Usuario, ControladorJuego>();
    public ListaDePartidas(){}
    public void AgregarPartida()
    {

    }
}