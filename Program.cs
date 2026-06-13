using GestionEstudiantesPorAsig.Models;
using GestionEstudiantesPorAsig.Services;
using System;

namespace SistemaDocente
{
    class Program
    {
        private static readonly GestorAcademico Gestor = new GestorAcademico();

        static void Main(string[] args)
        {
         
            Gestor.RegistrarAsignaturaConGrupo("IDS325", "Programacion OO", "01");
            Gestor.RegistrarAsignaturaConGrupo("IDS325", "Programacion OO", "02");

            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("       SISTEMA DE CONTROL DE ESTUDIANTES          ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Registrar Asignatura y Grupo");
                Console.WriteLine("2. Agregar Estudiante a un Grupo");
                Console.WriteLine("3. Registrar Calificaciones de Exámenes/Prácticas");
                Console.WriteLine("4. Mostrar Listado de Calificaciones por Grupo");
                Console.WriteLine("5. Calcular Porcentaje de Alumnos Aprobados");
                Console.WriteLine("6. Salir");
                Console.WriteLine("==================================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        CrearAsignaturaGrupo();
                        break;
                    case "2":
                        AgregarEstudiante();
                        break;
                    case "3":
                        RegistrarNotas();
                        break;
                    case "4":
                        MostrarListado();
                        break;
                    case "5":
                        MostrarPorcentajeAprobados();
                        break;
                    case "6":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void CrearAsignaturaGrupo()
        {
            Console.Clear();
            Console.WriteLine("--- REGISTRAR ASIGNATURA Y GRUPO ---");
            Console.Write("Código de Asignatura (Ej: IDS325): ");
            string codigoAsig = Console.ReadLine();
            Console.Write("Nombre de Asignatura: ");
            string nombreAsig = Console.ReadLine();
            Console.Write("Código de Grupo (Ej: 01): ");
            string codigoGrupo = Console.ReadLine();

            OperationResult res = Gestor.RegistrarAsignaturaConGrupo(codigoAsig, nombreAsig, codigoGrupo);
            MostrarResultado(res);
        }

        private static void AgregarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("--- AGREGAR ESTUDIANTE A GRUPO ---");
            Console.Write("Código de Asignatura: ");
            string codigoAsig = Console.ReadLine();
            Console.Write("Código de Grupo: ");
            string codigoGrupo = Console.ReadLine();
            Console.Write("Matrícula del Estudiante: ");
            string matricula = Console.ReadLine();
            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();
            Console.Write("Tipo de Estudiante (1 = Presencial, 2 = A Distancia): ");
            string tipo = Console.ReadLine();

            Estudiante nuevoEstudiante;
            if (tipo == "1")
            {
                nuevoEstudiante = new EstudiantePresencial(matricula, nombre);
            }
            else if (tipo == "2")
            {
                nuevoEstudiante = new EstudianteDistancia(matricula, nombre);
            }
            else
            {
                Console.WriteLine("\n[ERROR] Tipo de estudiante inválido.");
                Console.ReadKey();
                return;
            }

            OperationResult res = Gestor.AgregarEstudianteAGrupo(codigoAsig, codigoGrupo, nuevoEstudiante);
            MostrarResultado(res);
        }

        private static void RegistrarNotas()
        {
            Console.Clear();
            Console.WriteLine("--- REGISTRAR CALIFICACIONES ---");
            Console.Write("Código de Asignatura: ");
            string codigoAsig = Console.ReadLine();
            Console.Write("Código de Grupo: ");
            string codigoGrupo = Console.ReadLine();
            Console.Write("Matrícula del Estudiante: ");
            string matricula = Console.ReadLine();

            try
            {
                Console.Write("Nota del Examen: ");
                double examen = Convert.ToDouble(Console.ReadLine());
                Console.Write("Nota de la Práctica: ");
                double practica = Convert.ToDouble(Console.ReadLine());

                Console.Write("Nota de Criterio Adicional (Asistencia para Presencial / Foros para Distancia): ");
                double adicional = Convert.ToDouble(Console.ReadLine());

                OperationResult res = Gestor.RegistrarCalificacionesEstudiante(codigoAsig, codigoGrupo, matricula, examen, practica, adicional);
                MostrarResultado(res);
            }
            catch (FormatException)
            {
                Console.WriteLine("\n[ERROR] El formato de nota ingresado no es válido.");
                Console.ReadKey();
            }
        }

        private static void MostrarListado()
        {
            Console.Clear();
            Console.WriteLine("--- LISTADO DE CALIFICACIONES ---");
            Console.Write("Código de Asignatura: ");
            string codigoAsig = Console.ReadLine();
            Console.Write("Código de Grupo: ");
            string codigoGrupo = Console.ReadLine();

            OperationResult res = Gestor.ObtenerListadoCalificaciones(codigoAsig, codigoGrupo);

            Console.WriteLine("\n==================================================");
            if (res.Success)
            {
                Console.WriteLine($"Resultados para {codigoAsig.ToUpper()} - Grupo {codigoGrupo.ToUpper()}:");
                foreach (string lineaReporte in res.Data)
                {
                    Console.WriteLine(lineaReporte);
                }
            }
            else
            {
                Console.WriteLine($"Error: {res.Message}");
            }
            Console.WriteLine("==================================================");
            Console.WriteLine("\nPresione cualquier tecla para regresar al menú...");
            Console.ReadKey();
        }

        private static void MostrarPorcentajeAprobados()
        {
            Console.Clear();
            Console.WriteLine("--- PORCENTAJE DE ALUMNOS APROBADOS ---");
            Console.Write("Código de Asignatura: ");
            string codigoAsig = Console.ReadLine();
            Console.Write("Código de Grupo: ");
            string codigoGrupo = Console.ReadLine();

            OperationResult res = Gestor.CalcularPorcentajeAprobados(codigoAsig, codigoGrupo);

            if (res.Success)
            {
                Console.WriteLine($"\n>> El porcentaje de estudiantes aprobados en este grupo es: {res.Data:F2}%");
            }
            else
            {
                Console.WriteLine($"\n[ERROR] {res.Message}");
            }
            Console.WriteLine("\nPresione cualquier tecla para regresar al menú...");
            Console.ReadKey();
        }

        private static void MostrarResultado(OperationResult res)
        {
            Console.WriteLine("\n--------------------------------------------------");
            if (res.Success)
            {
                Console.WriteLine($"[ÉXITO] {res.Message}");
            }
            else
            {
                Console.WriteLine($"[FALLO] {res.Message}");
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}