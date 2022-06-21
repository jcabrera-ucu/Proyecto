using System.Text.Json;

namespace Library;

public class HistóricoEstadísticas
{
    public Dictionary<string, Estadistica> Estadísticas { get; private set; }

    public string RutaArchivo { get; }

    public HistóricoEstadísticas() : this("bdd.json")
    {
    }

    public HistóricoEstadísticas(string rutaArchivo)
    {
        RutaArchivo = rutaArchivo;
        Estadísticas = new();

        Cargar();
    }

    public Estadistica ObtenerEstadística(Ident id)
    {
        if (!Estadísticas.ContainsKey(id.Value))
        {
            Estadísticas.Add(id.Value, new Estadistica());
        }

        return Estadísticas[id.Value];
    }

    public void Guardar()
    {
        File.WriteAllText(RutaArchivo, JsonSerializer.Serialize(Estadísticas));
    }

    public void Cargar()
    {
        var json = CargarJson();

        var datos = JsonSerializer.Deserialize<Dictionary<string, Estadistica>>(json);

        if (datos != null)
        {
            Estadísticas = datos;
        }
    }

    private string CargarJson()
    {
        try
        {
            return File.ReadAllText(RutaArchivo);
        }
        catch
        {
            return "{}";
        }
    }
}

