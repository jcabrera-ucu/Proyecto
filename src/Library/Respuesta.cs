namespace Library;

public class Respuesta
{
    public string Remitente { get; }

    public string Oponente { get; }

    public Ident IdRemitente { get; }

    public Ident? IdOponente { get; }

    public Respuesta(string remitente, string oponente, Ident idRemitente, Ident? idOponente)
    {
        Remitente = remitente;
        Oponente = oponente;
        IdRemitente = idRemitente;
        IdOponente = idOponente;
    }
}
