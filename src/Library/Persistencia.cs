using System;
using System.IO;
/// <summary>
/// Utilizamos la persistencia para guardad los datos de las estadísticas por cada "Usuario" que utilice
/// el juego. Tambien guardamos las partidas ganadas.
/// </summary>
public class Persistencia
{
    /// <summary>
    /// Variables que son utilizadas como contadores.
    /// </summary>
    int Victoria;
    int Derrota;
    int Acierto;
    int Fallo;
    int Hundido;
    /// <summary>
    /// SetVictorias; Seteo el contador de victorias con el número indicado.
    /// </summary>
    public void SetVitorias(int n)
    {
        Victoria = n;
    }
    /// <summary>
    /// GetVictorias devuelve el número de victorias.
    /// </summary>
    public int GetVictorias()
    {
        return Victoria; 
    }
    /// <summary>
    /// Guardo el número que se encuantra en Victoria en la fila VIctorias.
    /// La fila Victorias es creada por "File.Open".
    /// 
    /// </summary>
    public void GuardarVictoria(string Victorias)
    {
        BinaryWriter ficheroSalida = new BinaryWriter(File.Open(Victorias, FileMode.Create));
        ficheroSalida.Write(Victoria);
        ficheroSalida.Close();
    }
    /// <summary>
    /// Cargo un nuevo dato en una fila que fue creada por GuardarVictoria.
    /// La Victoria es cargada por ficheroEntrada.ReadInt32();
    /// </summary>
    public void CargarVictoria(string Victorias)
    {
        BinaryReader ficheroEntrada = new BinaryReader(File.Open(Victorias, FileMode.Open));
        Victoria = ficheroEntrada.ReadInt32(); 
    }
    /// <summary>
    /// SetDerrotas, recibe un número "n" y setea ese valor en Derrota.
    /// </summary>
    public void SetDerrotas(int n)
    {
        Derrota = n;
    }
    /// <summary>
    /// GetDerrotas, devuelve el número que se encuentra en Derrota.
    /// </summary>
    public int GetDerrotas()
    {
        return Derrota;
    }
    /// <summary>
    /// GuardarDerrotas, guarda las derrotas con la misma lógica que GuardarVictorias.
    /// </summary>
    public void GuardarDerrota(string Derrotas)
    {
        BinaryWriter ficheroSalida = new BinaryWriter(File.Open(Derrotas, FileMode.Create));
        ficheroSalida.Write(Derrota);
        ficheroSalida.Close();
    }
    /// <summary>
    /// CargarDerrota, carga un nuevo número en Derrota con la misma lógica que CargarVictorias.
    /// </summary>
    public void CargarDerrota(string Derrotas)
    {
        BinaryReader ficheroEntrada = new BinaryReader(File.Open(Derrotas, FileMode.Open));
        Derrota = ficheroEntrada.ReadInt32();
    }
    /// <summary>
    /// SetAciertos, carga en Acirto el número indicado por parametro.
    /// </summary>
    public void SetAciertos(int n)
    {
        Acierto = n;
    }
    /// <summary>
    /// GetAciertos, devuelve el valor de Acierto.
    /// </summary>
    public int GetAciertos()
    {
        return Acierto;
    }
    /// <summary>
    /// GuardarAciertos, guarda los aciertos usa la misma lógica que GuardarVictorias.
    /// </summary>
    public void GuardarAciertos(string Aciertos)
    {
        BinaryWriter ficheroSalida = new BinaryWriter(File.Open(Aciertos, FileMode.Create));
        ficheroSalida.Write(Acierto);
        ficheroSalida.Close();
    }
    /// <summary>
    ///CargarAcietos, carga nuevo datos usa la misma lógica que CargarVictorias.
    /// </summary>
    public void CargarAciertos(string Aciertos)
    {
        BinaryReader ficheroEntrada = new BinaryReader(File.Open(Aciertos, FileMode.Open));
        Acierto = ficheroEntrada.ReadInt32(); 
    }
    /// <summary>
    /// SetFallos, setea un valor "n" en Fallo.
    /// </summary>
    public void SetFallos(int n)
    {
        Fallo = n;
    }
    /// <summary>
    /// GetFallos, devuelve el número que se encuentra en Fallo.
    /// </summary>
    public int GetFallos()
    {
        return Fallo;
    }
    /// <summary>
    /// GuardarFallos, guarda los Fallos con la misma lógica que GuardarVictorias.
    /// </summary>
    public void GuardarFallos(string Fallos)
    {
        BinaryWriter ficheroSalida = new BinaryWriter(File.Open(Fallos, FileMode.Create));
        ficheroSalida.Write(Fallo);
        ficheroSalida.Close();
    }
    /// <summary>
    /// CargarFallos, carga nuevo dato con la misma logica de CargarVictorias.
    /// </summary>
    public void CargarFallos(string Fallos)
    {
        BinaryReader ficheroEntrada = new BinaryReader(File.Open(Fallos, FileMode.Open));
        Fallo = ficheroEntrada.ReadInt32(); 
    }
    /// <summary>
    /// SetHundidos, setea un número "n" en la variable Hundido. 
    /// </summary>
    public void SetHundidos(int n)
    {
        Hundido = n;
    }
    /// <summary>
    /// GetHundidos, devuelve el valor que se encuentra en la variable Hundido.
    /// </summary>
    public int GetHundidos()
    {
        return Hundido;
    }
    /// <summary>
    /// GuardarHundidos, gurda el número de barcos undidos usando la misma lógica que GuardarVictorias.
    /// </summary>
    public void GuardarHundidos(string Hundidos)
    {
        BinaryWriter ficheroSalida = new BinaryWriter(File.Open(Hundidos, FileMode.Create));
        ficheroSalida.Write(Hundido);
        ficheroSalida.Close();
    }
    /// <summary>
    /// CargarHundidos, carga un nuevo dato usando la misma lógica que CargarVictorias.
    /// </summary>
    public void CargarHundidos(string Hundidos)
    {
        BinaryReader ficheroEntrada = new BinaryReader(File.Open(Hundidos, FileMode.Open));
        Hundido = ficheroEntrada.ReadInt32();
    }
}