using System.Diagnostics;
class Logger
{
    private static string eventos = "Peliculas"; //origen del evento
    private static string registrosEventos = "Application"; //log de windws donde se guardara

    //Inicializamos el log usando EventLog.Create.Source
    public static void InicializarLog()
    {
        try
        {
            if (!EventLog.SourceExists(eventos))
            {
                EventLog.CreateEventSource(eventos, registrosEventos);
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Fallo al crear el evento: {ex.Message}");
        }
    }
    //Funcion para escribir en Console y generar el log en windows, recibe mensaje y el tipo del evento
    public static void EscribirLog(string mensaje, EventLogEntryType tipo)
    {
        try
        {
            InicializarLog();
            EventLog.WriteEntry(eventos, mensaje, tipo);
            Console.WriteLine($"Log {tipo}: {mensaje} ");
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al escribir el log: {ex.Message}");
        }
    }
    //Indica los formatos y las rutas de origen y destino
    public static void LogInicio(string origen, string destino, string pathOrigen, string pathDestino)
    {
        string mensaje = ($"Inicio de la exportación: De {origen} a {destino}. Origen: {pathOrigen} y destino: {pathDestino}");
        EscribirLog (mensaje, EventLogEntryType.Information);
    }
    //Registra los datos de las pelis
    public static void LogPeli (string titulo, string genero, string premiere, int runtime, double imdb, string idioma)
    {
        string mensaje = ($"Procesando película: Título: {titulo}, Género: {genero}, Estreno: {premiere}, Duración: {runtime}, IMDB: {imdb}, Idioma: {idioma}");
        EscribirLog (mensaje, EventLogEntryType.Information);
    }
    //Dice cuando termina el proceso y enseña la nueva ruta
    public static void LogFin(string path)
    {
        string mensaje = ($"Fin de la exportación: Fichero generado con éxito en {path}");
        EscribirLog (mensaje, EventLogEntryType.Information);
    }
    //Dice los errores que hayan durante el proceso
    public static void LogError(string mensajeError)
    {
        EscribirLog(mensajeError, EventLogEntryType.Error);
    }
}