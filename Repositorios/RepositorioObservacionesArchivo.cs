using System;
using System.Collections.Generic;
using System.IO;

namespace NeuroHealthDesktop.Repositorios
{
    public class RepositorioObservacionesArchivo : IRepositorioObservaciones
    {
        private string rutaArchivo;

        public RepositorioObservacionesArchivo()
        {
            // Crea la carpeta "Datos" en el directorio del programa si no existe
            string carpetaDatos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");
            if (!Directory.Exists(carpetaDatos))
            {
                Directory.CreateDirectory(carpetaDatos);
            }

           
            rutaArchivo = Path.Combine(carpetaDatos, "observaciones.txt");
        }

        public void Agregar(Observacion observacion)
        {
           
            int proximoId = ObtenerProximoId();

            // Crea una nueva instancia asegurando que lleve el ID correcto
            Observacion observacionAGuardar = new Observacion(proximoId, observacion.DniPaciente, observacion.Texto);

            // 2. Convertir el objeto a una línea de texto
            string linea = ConvertirObservacionALinea(observacionAGuardar);

            // 3. Escribir (recomponer o añadir) la línea al final del archivo de texto
            File.AppendAllText(rutaArchivo, linea + Environment.NewLine);
        }

        public List<Observacion> ObtenerTodas()
        {
            List<Observacion> lista = new List<Observacion>();

            // Si el archivo no existe todavía, devuelve la lista vacía de forma segura
            if (!File.Exists(rutaArchivo))
            {
                return lista;
            }

            // Leer línea por línea el archivo txt
            string[] lineas = File.ReadAllLines(rutaArchivo);
            foreach (string linea in lineas)
            {
                if (!string.IsNullOrWhiteSpace(linea))
                {
                    Observacion? obs = ConvertirLineaAObservacion(linea);
                    if (obs != null)
                    {
                        lista.Add(obs);
                    }
                }
            }

            return lista;
        }

        public List<Observacion> ObtenerPorDniPaciente(long dniPaciente)
        {
           
            List<Observacion> todas = ObtenerTodas();
            List<Observacion> filtradas = new List<Observacion>();

            foreach (var obs in todas)
            {
                if (obs.DniPaciente == dniPaciente)
                {
                    filtradas.Add(obs);
                }
            }

            return filtradas;
        }

        public int ObtenerProximoId()
        {
            List<Observacion> todas = ObtenerTodas();
            if (todas.Count == 0)
            {
                return 1; // Si está vacío, el primer ID será 1
            }

            // Busca el ID más alto actual y le suma 1
            int maxId = 0;
            foreach (var obs in todas)
            {
                if (obs.Id > maxId)
                {
                    maxId = obs.Id;
                }
            }

            return maxId + 1;
        }

        private string ConvertirObservacionALinea(Observacion observacion)
        {
            // Convierte el objeto a formato: Id|DniPaciente|Texto
            return $"{observacion.Id}|{observacion.DniPaciente}|{observacion.Texto}";
        }

        private Observacion? ConvertirLineaAObservacion(string linea)
        {
            try
            {
               
                string[] partes = linea.Split('|');

                // Valida que tenga las 3 partes necesarias (Id, Dni, Texto)
                if (partes.Length >= 3)
                {
                    int id = int.Parse(partes[0]);
                    long dni = long.Parse(partes[1]);

                    // Vuelve a unir el texto por si la observación contenía caracteres '|' por error
                    string texto = string.Join("|", partes, 2, partes.Length - 2);

                    return new Observacion(id, dni, texto);
                }
            }
            catch
            {
                // Si una línea está corrupta o mal escrita, la ignora para que no rompa el programa
            }

            return null;
        }
    }
}