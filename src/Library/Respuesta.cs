namespace Library;

/// <summary>
/// Respuesta resultante de ejecutar un comando de un usuario
/// </summary>
public class Respuesta
{
    /// <summary>
    /// Mensaje a enviar al remitente del comando
    /// </summary>
    public string Remitente { get; }

    /// <summary>
    /// Mensaje a enviar al oponente del remitente, puede estar vacío
    /// </summary>
    public string Oponente { get; }

    /// <summary>
    /// Id del remitente del comando
    /// </summary>
    public Ident IdRemitente { get; }


    /// <summary>
    /// Id del oponente del remitente, puede ser null
    /// </summary>
    public Ident? IdOponente { get; }

    /// <summary>
    /// Construye una respuesta
    /// </summary>
    public Respuesta(string remitente, string oponente, Ident idRemitente, Ident? idOponente)
    {
        Remitente = remitente;
        Oponente = oponente;
        IdRemitente = idRemitente;
        IdOponente = idOponente;
    }
}
