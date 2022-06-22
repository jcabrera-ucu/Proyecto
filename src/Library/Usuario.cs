namespace Library;
// Esta clase cumple con SRP, su unica responsabilidad es mantener ciertos atributos de los usuarios.
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
    /// <value>Valor de la Id obtenida de telegram.</value>
    public Ident Id { get; set; }

    public string Nombre { get; set; } = String.Empty;

    public Estadistica Estadisticas { get; set; } = new();
}
