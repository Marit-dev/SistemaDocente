using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiantesPorAsig.Models
{
    public class EstudianteDistancia : Estudiante
    {
        public double NotaForos { get; set; }

        public EstudianteDistancia(string matricula, string nombre) : base(matricula, nombre)
        {
            NotaForos = 0;
        }

        public override double CalcularNotaFinal()
        {
            return (NotaExamen * 0.40) + (NotaPractica * 0.50) + (NotaForos * 0.10);
        }

        public override string ObtenerReporte()
        {
            return base.ObtenerReporte() + $" | Foros virtuales: {NotaForos} (A Distancia)";
        }
    }
}
