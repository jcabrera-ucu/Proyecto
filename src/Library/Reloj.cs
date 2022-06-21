namespace Library;

using System;

/// <summary>
/// Reloj de juego, lleva cuenta del tiempo que dispone un jugador.
/// </summary>
public class Reloj
{
    /// <summary>
    /// Momento en el que inició el turno del jugador
    /// </summary>
    public DateTime Inicio { get; private set; }

    /// <summary>
    /// Tiempo disponible
    /// </summary>
    public TimeSpan TiempoRestante { get; private set; }

    /// <summary>
    /// True si aún queda tiempo en el reloj
    /// </summary>
    public bool Activo
    {
        get
        {
            return TiempoRestante != TimeSpan.Zero;
        }
    }

    /// <summary>
    /// Construye un reloj
    /// </summary>
    /// <param name="tiempoRestante">Tiempo disponible</param>
    public Reloj(TimeSpan tiempoRestante)
    {
        TiempoRestante = tiempoRestante;
        if (TiempoRestante <= TimeSpan.Zero)
        {
            TiempoRestante = TimeSpan.Zero;
        }
    }

    /// <summary>
    /// Inicia el "turno del jugador"
    /// </summary>
    public void Iniciar()
    {
        Inicio = DateTime.UtcNow;
    }

    /// <summary>
    /// Termina el "turno del jugador" y actualiza el tiempo restante.
    /// </summary>
    public void Terminar()
    {
        var delta = DateTime.UtcNow - Inicio;

        TiempoRestante -= delta;

        if (TiempoRestante < TimeSpan.Zero)
        {
            TiempoRestante = TimeSpan.Zero;
        }
    }
}
