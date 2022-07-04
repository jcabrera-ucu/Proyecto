using System;
namespace Library;

/// <summary>
/// Une todas las piezas para soportar una partida de BatallaNaval.
/// </summary>
public class BatallaNaval
{
    /// <summary>
    ///
    /// </summary>
    public HistóricoEstadísticas Estadísticas { get; }

    /// <summary>
    ///
    /// </summary>
    public GestorPartidas GestorPartidas { get; }

    /// <summary>
    ///
    /// </summary>
    public IHandler Chain { get; }

    /// <summary>
    ///
    /// </summary>
    public BatallaNaval()
    {
        Estadísticas = new();
        GestorPartidas = new(Estadísticas);
        Chain =
            new InicioHandler(
            new MenuHandler(GestorPartidas,
            new BuscarPartidaHandler(GestorPartidas, conReloj: false,
            new BuscarPartidaHandler(GestorPartidas, conReloj: true,
            new EstadisticasHandler(Estadísticas,
            new AtaqueHandler(GestorPartidas,
            new ConfiguracionHandler(GestorPartidas,
            new JugadasHandler(GestorPartidas,
            new RelojHandler(GestorPartidas,
            new RadarHandler(GestorPartidas,
            new TableroHandler(GestorPartidas,
            new NullHandler())))))))))));
    }

    public Respuesta ProcesarMensaje(Message mensaje)
    {
        try
        {
            string remitente;
            string oponente;
            Chain.Handle(mensaje, out remitente, out oponente);

            Estadísticas.Guardar();

            return new Respuesta(remitente: remitente,
                                 oponente: oponente,
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
        catch (EstadoPartidaIncorrecto)
        {
            return new Respuesta(remitente: "No puede realizar esa acción en éste momento",
                                 oponente: "",
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
        catch (JugadorIncorrecto)
        {
            return new Respuesta(remitente: "No puede realizar esa acción en éste momento",
                                 oponente: "",
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
        catch (CoordenadaFueraDelTablero exc)
        {
            return new Respuesta(remitente: $"¡Coordenada fuera del tablero: {exc.Coordenada.ToAlfanumérico()}!",
                                 oponente: "",
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
        catch (CoordenadaFormatoIncorrecto exc)
        {
            switch (exc.Razón)
            {
                case CoordenadaFormatoIncorrecto.Error.Rango:
                    return new Respuesta(remitente: $"¡Error de rango numérico en: {exc.Value}!",
                                         oponente: "",
                                         idRemitente: mensaje.IdJugador,
                                         idOponente: ObtenterIdOponente(mensaje.IdJugador));
                case CoordenadaFormatoIncorrecto.Error.Sintaxis:
                default:
                    return new Respuesta(remitente: $"¡Error de sintaxis en: {exc.Value}!",
                                         oponente: "",
                                         idRemitente: mensaje.IdJugador,
                                         idOponente: ObtenterIdOponente(mensaje.IdJugador));
            }
        }
        catch (RadaresAgotados)
        {
            return new Respuesta(remitente: $"¡Ya no te quedan radares!",
                                 oponente: "",
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
        catch (CoordenadasNoAlineadas exc)
        {
            return new Respuesta(remitente: $"¡Las coordenadas no están en línea recta!\n"
                                          + $" >> {exc.Primera.ToAlfanumérico()} y {exc.Segunda.ToAlfanumérico()}",
                                 oponente: "",
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
        catch (BarcosSuperpuestos exc)
        {
            return new Respuesta(remitente: $"El barco que está intentando agregar se superpone con otro\n"
                                          + $" >> ({exc.Primero}) y ({exc.Segundo})",
                                 oponente: "",
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
        catch (BarcoYaExiste exc)
        {
            return new Respuesta(remitente: $"¡Ya existe un barco de tamaño {exc.Largo}",
                                 oponente: "",
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
        catch (Exception exc)
        {
            return new Respuesta(remitente: $"Error inesperado: {exc.Message}",
                                 oponente: "",
                                 idRemitente: mensaje.IdJugador,
                                 idOponente: ObtenterIdOponente(mensaje.IdJugador));
        }
    }

    /// <summary>
    /// Dada una partida y un identificador de un jugador, devuelve el identificador
    /// de su oponente.
    /// </summary>
    /// <param name="partida">Instancia de una partida (puede ser null)</param>
    /// <param name="idJugador">Identificador de un jugador de la partida</param>
    /// <returns>Identificador del oponente de <c>idJugador</c>, o null</returns>
    private Ident? ObtenterIdOponente(Ident idJugador)
    {
        var partida = GestorPartidas.ObtenerPartida(idJugador);
        if (partida != null)
        {
            var oponente = partida.OponenteDe(idJugador);
            if (oponente != null)
            {
                return oponente.Id;
            }
        }

        return null;
    }
}