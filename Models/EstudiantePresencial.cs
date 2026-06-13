using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiantesPorAsig.Models
{

    public class EstudiantePresencial : Estudiante
    {
        public double NotaAsistencia { get; set; }

        public EstudiantePresencial(string matricula, string nombre) : base(matricula, nombre)
        {
            NotaAsistencia = 0;
        }

        public override double CalcularNotaFinal()
        {
            return (NotaExamen * 0.40) + (NotaPractica * 0.50) + (NotaAsistencia * 0.10);
        }

        public override string ObtenerReporte()
        {
            return base.ObtenerReporte() + $" | Asistencia: {NotaAsistencia} (Presencial)";
        }
    }
}
