namespace NeuroHealthDesktop.Servicios
{
    public class ResultadoOperacion<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public T? Dato { get; set; }

        public ResultadoOperacion(bool exito, string mensaje, T? dato)
        {
            Exito = exito;
            Mensaje = mensaje;
            Dato = dato;
        }

        public static ResultadoOperacion<T> Correcto(string mensaje, T dato)
        {
            return new ResultadoOperacion<T>(true, mensaje, dato);
        }

        public static ResultadoOperacion<T> Error(string mensaje)
        {
            return new ResultadoOperacion<T>(false, mensaje, default);
        }
    }
}
