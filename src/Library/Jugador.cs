namespace Library;

public class Jugador
{
    public Tablero Tablero { get; }
    public string Id { get; set; }

    public Jugador(string id, string nombre, Tablero tablero)
    {
        this.Id = id;
        this.Tablero = tablero;
    }
    public ResultadoAtaque Atacar(Coord coord)
    {
        ResultadoAtaque resultado = this.Tablero.Atacar(coord);
        if (resultado == ResultadoAtaque.Agua)
        {
            return resultado;
        }
        else if (resultado == ResultadoAtaque.Tocado)
        {
            return resultado;
        }
        else
        {
            return resultado;
        }
    }
    public void Radar(Coord centro)
    {
        this.Tablero.Radar(centro);
    }
}
