using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiantesPorAsig.Models
{
    public abstract class Estudiante
    {
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public double NotaExamen { get; set; }
        public double NotaPractica { get; set; }

        protected Estudiante(string matricula, string nombre)
        {
            Matricula = matricula;
            Nombre = nombre;
            NotaExamen = 0;
            NotaPractica = 0;
        }

        public abstract double CalcularNotaFinal();

        public virtual string ObtenerReporte()
        {
            return $"Matrícula: {Matricula} | Nombre: {Nombre} | Examen: {NotaExamen} | Prácticas: {NotaPractica} | Nota Final: {CalcularNotaFinal():F2}";
        }
    }
}
