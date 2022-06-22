using System.Text.Json;

namespace Library;

// SRP, su unica responsabilidad es persistir las estadisticas.
/// <summary>
/// Maneja la persistencia de las estadísticas de los usuarios
/// </summary>
public class HistóricoEstadísticas
{
    /// <summary>
    /// Asociación entre ids de usuarios y su estadística
    /// </summary>
    public Dictionary<string, Estadistica> Estadísticas { get; private set; }

    /// <summary>
    /// Ruta al archivo donde se guardan los datos
    /// </summary>
    public string RutaArchivo { get; }

    /// <summary>
    /// Construye el gestor con ruta "bdd.json"
    /// </summary>
    public HistóricoEstadísticas() : this("bdd.json")
    {
    }

    /// <summary>
    /// Construye el gestor con una ruta de archivo dada
    /// </summary>
    /// <param name="rutaArchivo">Ruta al archivo de datos</param>
    public HistóricoEstadísticas(string rutaArchivo)
    {
        RutaArchivo = rutaArchivo;
        Estadísticas = new();

        Cargar();
    }

    /// <summary>
    /// Retorna las estadísticas de un usuario
    /// </summary>
    /// <param name="id">Id del usuario</param>
    /// <returns>Instancia de estadística del usuario</returns>
    public Estadistica ObtenerEstadística(Ident id)
    {
        if (!Estadísticas.ContainsKey(id.Value))
        {
            Estadísticas.Add(id.Value, new Estadistica());
        }

        return Estadísticas[id.Value];
    }

    /// <summary>
    /// Guarda el histórico en el archivo
    /// </summary>
    public void Guardar()
    {
        File.WriteAllText(RutaArchivo, JsonSerializer.Serialize(Estadísticas));
    }

    /// <summary>
    /// Carga el histórico desde el archivo
    /// </summary>
    public void Cargar()
    {
        var json = CargarJson();

        var datos = JsonSerializer.Deserialize<Dictionary<string, Estadistica>>(json);

        if (datos != null)
        {
            Estadísticas = datos;
        }
    }

    /// <summary>
    /// Lee el json del archivo
    /// </summary>
    /// <returns>El contenido del archivo, o un Json "vacío"</returns>
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

