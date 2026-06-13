using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiantesPorAsig.Models
{
    public class Asignatura
    {
        public string CodigoAsignatura { get; set; } 
        public string Nombre { get; set; }
        public List<Grupo> Grupos { get; set; }

        public Asignatura(string codigoAsignatura, string nombre)
        {
            CodigoAsignatura = codigoAsignatura;
            Nombre = nombre;
            Grupos = new List<Grupo>();
        }
    }
}
