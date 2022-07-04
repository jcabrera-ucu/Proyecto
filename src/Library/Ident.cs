namespace Library;

// SRP, se encarga de representar identificadores y esa es su unica responsabilidad.

/// <summary>
/// Representa un identificador en formato string
/// </summary>
public record struct Ident
{
    /// <summary>
    /// El valor interno del identificador
    /// </summary>
    public string Value { get; }

    public Ident()
    {
        Value = String.Empty;
    }

    /// <summary>
    /// Construye un Id con un identificador dado
    /// </summary>
    public Ident(string id)
    {
        Value = id;
    }
}
