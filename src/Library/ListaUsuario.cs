namespace Library;

public class ListaUsuario
{
    private ListaUsuario() { }

    private static ListaUsuario _instance;
    public static ListaUsuario GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ListaUsuario();
        }
        return _instance;
    }

    public Dictionary<string,Usuario> UsuariosExistentes;

    public void CrearUsuario(string id, string nombre)
    {
        UsuariosExistentes.Add(id, new Usuario(id, nombre));
    }
}