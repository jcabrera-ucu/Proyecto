namespace Library;

public class CoordenadaFormatoIncorrecto : Exception
{
    public enum Error
    {
        Sintaxis,
        Rango,
    }

    public Error Razón { get; }

    public string Value { get; }

    public CoordenadaFormatoIncorrecto(Error razón, string value)
    {
        Razón = razón;
        Value = value;
    }
}
