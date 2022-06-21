namespace Library;

/// <summary>
/// Error cuando se intenta agregar un barco de una longitud
/// que ya fue agregada
/// </summary>
public class BarcoYaExiste : Exception
{
    public int Largo { get; }

    public BarcoYaExiste(int largo)
    {
        Largo = largo;
    }
}
