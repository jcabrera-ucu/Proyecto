namespace Library;

using System;

// Reloj cumple con SRP, solamente lleva una cuenta de tiempo.
/// <summary>
/// Reloj de juego, lleva cuenta del tiempo que dispone un jugador.
/// </summary>
public class Reloj
{
    /// <summary>
    /// Momento en el que inició el turno del jugador
    /// </summary>
    public DateTime? Inicio { get; private set; }

    private TimeSpan _tiempoRestante;

    /// <summary>
    /// Tiempo disponible
    /// </summary>
    public TimeSpan TiempoRestante
    {
        get
        {
            if (Inicio != null)
            {
                var delta = Now() - (DateTime) Inicio;
                var restante = _tiempoRestante - delta;

                if (restante <= TimeSpan.Zero)
                {
                    return TimeSpan.Zero;
                }

                return restante;
            }
            return _tiempoRestante;
        }
    }

    /// <summary>
    /// Función para obtener la hora actual
    /// </summary>
    public Func<DateTime> Now { get; }

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
    /// <param name="now">
    /// Función que retorna "la fecha actual",
    /// ésta es la función que se usa para obtener la hora en la clase
    /// </param>
    public Reloj(TimeSpan tiempoRestante, Func<DateTime> now)
    {
        Now = now;
        _tiempoRestante = tiempoRestante;
        if (_tiempoRestante <= TimeSpan.Zero)
        {
            _tiempoRestante = TimeSpan.Zero;
        }
    }

    /// <summary>
    /// Construye un reloj usando DateTime.UtcNow como "Now"
    /// </summary>
    /// <param name="tiempoRestante">Tiempo disponible</param>
    public Reloj(TimeSpan tiempoRestante)
        : this(tiempoRestante, () => DateTime.UtcNow)
    {
    }

    /// <summary>
    /// Inicia el "turno del jugador"
    /// </summary>
    public void Iniciar()
    {
        Inicio = Now();
    }

    /// <summary>
    /// Termina el "turno del jugador" y actualiza el tiempo restante.
    /// </summary>
    public void Terminar()
    {
        _tiempoRestante = TiempoRestante;
        Inicio = null;
    }
}
