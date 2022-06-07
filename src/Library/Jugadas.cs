namespace Library;
 public  class Jugada
 {
     public TipoJugada Tipo { get; }
     public Coord Coordenada { get; }
     public string Id { get; }

     public Jugada(string id, TipoJugada tipo, Coord coord)
     {
         Id = id;
         Tipo = tipo;
         Coordenada = coord;
     }

 }