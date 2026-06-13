# Sistema de Control de Estudiantes

Aplicación de consola desarrollada en C# para la gestión académica de docentes. Este proyecto aplica conceptos fundamentales de la Programación Orientada a Objetos (POO) como **Herencia, Polimorfismo y Encapsulamiento** para administrar asignaturas, grupos y diferentes tipos de estudiantes.

## Funcionalidades Principales

* **Gestión Académica:** Creación de asignaturas y grupos.
* **Manejo de Estudiantes:** Inscripción de estudiantes clasificándolos en *Presenciales* o *A Distancia*.
* **Registro de Calificaciones:** Asignación de notas de exámenes, prácticas y criterios adicionales según la modalidad del estudiante (asistencia o foros).
* **Generación de Reportes:** Impresión de listados de calificaciones estructurados.
* **Cálculo de Rendimiento:** Cálculo automatizado del porcentaje de estudiantes aprobados (nota $\ge$ 70).

## Arquitectura y Conceptos Aplicados

El proyecto fue estructurado separando las responsabilidades y almacenando los datos dinámicamente en memoria (usando `List<T>`).

* **Herencia:** Clase base abstracta `Estudiante` con clases derivadas `EstudiantePresencial` y `EstudianteDistancia`.
* **Polimorfismo:** Implementación del método sobreescrito `CalcularNotaFinal()` para aplicar diferentes pesos de evaluación según la modalidad del alumno.
* **Manejo de Respuestas:** Uso de una clase `OperationResult` para estandarizar las respuestas del sistema y manejar validaciones sin interrumpir la ejecución.

## Requisitos y Ejecución

1. Clonar el repositorio.
2. Abrir la solución en Visual Studio o ejecutar a través de la CLI de .NET.
3. Compilar y ejecutar el proyecto (`dotnet run`).
4. Interactuar con el menú de la consola introduciendo los datos solicitados.

---
*Desarrollado como práctica de Estructura de datos*
