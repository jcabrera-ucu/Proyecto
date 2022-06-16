namespace Library;
/// <summary>
/// Creamos la clase Usuario para poder guardar la Id de cada usuario en la clase ListaUsuario,
/// La Id la utilizamos para conectar los handlet del telegram con el programa.
/// </summary>
public class Usuario
{
    /// <summary>
    /// Guardamo la Id como un string para poder manejarla mejor más adelante.
    /// La Id es pasada por parametro desde Telegram en el constructor de Usuario con el fin de que cada usurio tenga una Id única.
    /// </summary>
    /// <value>Valor de la Id ovtenida de telegram.</value>
    public string Id { get; set; }
    public Usuario(string id)
    {
        Id = id;
    }
    /// <summary>
    /// Metodo para tener la posibilídad de volver a setear la Id del usuario.
    /// </summary>
    /// <param name="id">Es seteada por el parametro que anteriormente es subido.</param>
    public void SetIdUsuario(string id)
    {
        Id = id;
    }
    /// <summary>
    /// Metodo que devuelve la Id guardada en la variable Id.
    /// No es de gran importancía para nuestro programa.
    /// </summary>
    /// <returns>Devuelve la Id seteada en el metodo constructor de la clase Usuario.</returns>
    public string GetIdUsuario()
    {
        return Id;
    }
}