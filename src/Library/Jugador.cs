namespace Library;

public class Jugador
{
    private string Id { get; }
    public Tablero Tablero { get; }
    public string Nombre { get; set; }
    private Estadistica Estadistica { get; }

    public Jugador(string id, string nombre, Tablero tablero)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Tablero = tablero;
        this.Estadistica = new Estadistica();
    }
    public ResultadoAtaque Atacar(Coord coord)
    {
        ResultadoAtaque resultado = this.Tablero.Atacar(coord);
        if (resultado == ResultadoAtaque.Agua)
        {
            this.Estadistica.Fallos++;
            return resultado;
        }
        else if (resultado == ResultadoAtaque.Tocado)
        {
            this.Estadistica.Aciertos++;
            return resultado;
        }
        else
        {
            this.Estadistica.Hundidos++;
            return resultado;
        }
    }
    public void Radar(Coord esqSupIzq, Coord esqInfDer)
    {
        this.Tablero.Radar(esqSupIzq, esqInfDer);
    }
}