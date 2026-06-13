using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEstudiantesPorAsig.Models;
using System.Linq;

namespace GestionEstudiantesPorAsig.Services
{

    public class GestorAcademico
    {
        private readonly List<Asignatura> _asignaturas;

        public GestorAcademico()
        {
            _asignaturas = new List<Asignatura>();
        }

      
        public OperationResult RegistrarAsignaturaConGrupo(string codigoAsig, string nombreAsig, string codigoGrupo)
        {
            try
            {
                var asignatura = _asignaturas.FirstOrDefault(a => a.CodigoAsignatura.ToUpper() == codigoAsig.ToUpper());
                if (asignatura == null)
                {
                    asignatura = new Asignatura(codigoAsig.ToUpper(), nombreAsig);
                    _asignaturas.Add(asignatura);
                }

                if (asignatura.Grupos.Any(g => g.CodigoGrupo.ToUpper() == codigoGrupo.ToUpper()))
                {
                    return new OperationResult { Success = false, Message = "El grupo ya existe en esta asignatura." };
                }

                asignatura.Grupos.Add(new Grupo(codigoGrupo.ToUpper()));
                return new OperationResult { Success = true, Message = $"Asignatura {codigoAsig} con Grupo {codigoGrupo} creada exitosamente." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = $"Error interno: {ex.Message}" };
            }
        }

   
        public OperationResult AgregarEstudianteAGrupo(string codigoAsig, string codigoGrupo, Estudiante estudiante)
        {
            var asignatura = _asignaturas.FirstOrDefault(a => a.CodigoAsignatura.ToUpper() == codigoAsig.ToUpper());
            if (asignatura == null)
                return new OperationResult { Success = false, Message = "Asignatura no encontrada." };

            var grupo = asignatura.Grupos.FirstOrDefault(g => g.CodigoGrupo.ToUpper() == codigoGrupo.ToUpper());
            if (grupo == null)
                return new OperationResult { Success = false, Message = "Grupo no encontrado en la asignatura especificada." };

            if (grupo.Estudiantes.Any(e => e.Matricula == estudiante.Matricula))
                return new OperationResult { Success = false, Message = "Esta matrícula ya está registrada en este grupo." };

            grupo.Estudiantes.Add(estudiante);
            return new OperationResult { Success = true, Message = $"Estudiante {estudiante.Nombre} agregado correctamente al grupo {codigoGrupo}." };
        }

       
        public OperationResult RegistrarCalificacionesEstudiante(string codigoAsig, string codigoGrupo, string matricula, double examen, double practica, double notaAdicional)
        {
            var asignatura = _asignaturas.FirstOrDefault(a => a.CodigoAsignatura.ToUpper() == codigoAsig.ToUpper());
            if (asignatura == null) return new OperationResult { Success = false, Message = "Asignatura no encontrada." };

            var grupo = asignatura.Grupos.FirstOrDefault(g => g.CodigoGrupo.ToUpper() == codigoGrupo.ToUpper());
            if (grupo == null) return new OperationResult { Success = false, Message = "Grupo no encontrado." };

            var estudiante = grupo.Estudiantes.FirstOrDefault(e => e.Matricula == matricula);
            if (estudiante == null) return new OperationResult { Success = false, Message = "Estudiante no encontrado en este grupo." };

            estudiante.NotaExamen = examen;
            estudiante.NotaPractica = practica;

            
            if (estudiante is EstudiantePresencial presencial)
            {
                presencial.NotaAsistencia = notaAdicional;
            }
            else if (estudiante is EstudianteDistancia distancia)
            {
                distancia.NotaForos = notaAdicional;
            }

            return new OperationResult { Success = true, Message = $"Calificaciones actualizadas para el estudiante {estudiante.Nombre}." };
        }

       
        public OperationResult ObtenerListadoCalificaciones(string codigoAsig, string codigoGrupo)
        {
            var asignatura = _asignaturas.FirstOrDefault(a => a.CodigoAsignatura.ToUpper() == codigoAsig.ToUpper());
            if (asignatura == null) return new OperationResult { Success = false, Message = "Asignatura no encontrada." };

            var grupo = asignatura.Grupos.FirstOrDefault(g => g.CodigoGrupo.ToUpper() == codigoGrupo.ToUpper());
            if (grupo == null) return new OperationResult { Success = false, Message = "Grupo no encontrado." };

            if (grupo.Estudiantes.Count == 0)
                return new OperationResult { Success = false, Message = "No hay estudiantes inscritos en este grupo." };

            List<string> reportes = grupo.Estudiantes.Select(e => e.ObtenerReporte()).ToList();
            return new OperationResult { Success = true, Message = "Listado generado.", Data = reportes };
        }

        
        public OperationResult CalcularPorcentajeAprobados(string codigoAsig, string codigoGrupo)
        {
            var asignatura = _asignaturas.FirstOrDefault(a => a.CodigoAsignatura.ToUpper() == codigoAsig.ToUpper());
            if (asignatura == null) return new OperationResult { Success = false, Message = "Asignatura no encontrada." };

            var grupo = asignatura.Grupos.FirstOrDefault(g => g.CodigoGrupo.ToUpper() == codigoGrupo.ToUpper());
            if (grupo == null) return new OperationResult { Success = false, Message = "Grupo no encontrado." };

            int totalEstudiantes = grupo.Estudiantes.Count;
            if (totalEstudiantes == 0)
                return new OperationResult { Success = true, Message = "El grupo no tiene estudiantes.", Data = 0.0 };

            int aprobados = grupo.Estudiantes.Count(e => e.CalcularNotaFinal() >= 70);
            double porcentaje = ((double)aprobados / totalEstudiantes) * 100;

            return new OperationResult { Success = true, Message = "Porcentaje calculado con éxito.", Data = porcentaje };
        }
    }
}
