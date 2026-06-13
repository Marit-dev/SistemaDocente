using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionEstudiantesPorAsig.Models
{

    public class Grupo
    {
        public string CodigoGrupo { get; set; }
        public List<Estudiante> Estudiantes { get; set; }

        public Grupo(string codigoGrupo)
        {
            CodigoGrupo = codigoGrupo;
            Estudiantes = new List<Estudiante>();
        }
    }
}
