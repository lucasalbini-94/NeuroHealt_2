using System;
using System.Collections.Generic;

namespace NeuroHealthDesktop.Servicios
{
    public class ServicioObservaciones
    {
        private IRepositorioObservaciones repositorioObservaciones;
        private IRepositorioPacientes repositorioPacientes;

        public ServicioObservaciones(IRepositorioObservaciones repositorioObservaciones, IRepositorioPacientes repositorioPacientes)
        {
            this.repositorioObservaciones = repositorioObservaciones;
            this.repositorioPacientes = repositorioPacientes;
        }

        public ResultadoOperacion<Observacion> AgregarObservacion(long dniPaciente, string texto)
        {
            // TODO: Validar paciente, validar texto y agregar observación.

            if (string.IsNullOrEmpty(texto) || string.IsNullOrWhiteSpace(texto))
            {
                return ResultadoOperacion<Observacion>.Error("El texto de la observación no puede estar vacío.");
            }

            Paciente? paciente = repositorioPacientes.BuscarPorDni(dniPaciente);

            if (paciente == null)
            {
                return ResultadoOperacion<Observacion>.Error("No se encontró ningún paciente con el DNI " + dniPaciente);
            }

            if (paciente.Nivel == NivelUrgencia.SinEvaluar)
            {
                return ResultadoOperacion<Observacion>.Error("Solo se pueden registrar observaciones para pacientes evaluados");
            }

            try
            {
                Observacion nueva = new Observacion(0, dniPaciente, texto.Trim());
                repositorioObservaciones.Agregar(nueva);

                return ResultadoOperacion<Observacion>.Correcto("Observación agregada con éxito.", nueva);
            }
            catch (Exception error)
            {
                return ResultadoOperacion<Observacion>.Error("Error al guardar: " + error.Message);
            }
        }

        public List<Observacion> ObtenerObservacionesPorPaciente(long dniPaciente)
        {
            // TODO: Obtener observaciones por DNI y ordenarlas.

            if (!repositorioPacientes.ExisteDni(dniPaciente))
            {
                return new List<Observacion>();
            }

            List<Observacion> listaFiltrada = repositorioObservaciones.ObtenerPorDniPaciente(dniPaciente);
            
            // Invertimos la lista manualmente para que queden de más nuevas a más viejas
            List<Observacion> listaOrdenada = new List<Observacion>();
            for (int i = listaFiltrada.Count - 1; i >= 0; i--)
            {
                listaOrdenada.Add(listaFiltrada[i]);
            }

            return listaOrdenada;
        }

        public List<Observacion> ObtenerTodas()
        {
            // TODO: Obtener todas las observaciones.

            var datos = repositorioObservaciones.ObtenerTodas();
            if (datos == null)
            {
                return new List<Observacion>();
            }

            List<Observacion> todasLimpias = new List<Observacion>();
            foreach (var elemento in datos)
            {
                if (elemento != null)
                {
                    todasLimpias.Add(elemento);
                }
            }

            List<Observacion> todasOrdenadas = new List<Observacion>();
            for (int i = todasLimpias.Count - 1; i >= 0; i--)
            {
                todasOrdenadas.Add(todasLimpias[i]);
            }

            return todasOrdenadas;
        }
    }
}