# Sistema de Gestión Académica (SGA) — TPI Academia

> **Trabajo Práctico Integrador**  
> **Materia:** Tecnologías de Desarrollo de Software IDE  
> **Universidad Tecnológica Nacional — Facultad Regional Rosario (UTN FRRo)**  
> **Año:** 2026

---

## Integrantes — Grupo N° 4

| Integrante | Legajo | 
| :--- | :---: | 
| **Martín Cabrera** | 53952 |
| **Nicolás Bolzico** | 53742 | 

---

## Sistema

El sistema elegido es el **Sistema de Gestión Académica (SGA)** propuesto por la cátedra.
```mermaid
classDiagram
    Personas "1" -- "0..1" Usuarios
    Usuarios "1" -- "0..N" ModulosUsuarios
    Modulos "1" -- "0..N" ModulosUsuarios
    Especialidades "1" -- "1..N" Planes
    Planes "0..1" -- "0..N" Personas
    Planes "1" -- "1..N" Materias
    Planes "1" -- "0..N" Comisiones
    Materias "1" -- "0..N" Cursos
    Comisiones "1" -- "0..N" Cursos
    Cursos "1" -- "0..N" DocentesCursos
    Personas "1" -- "0..N" DocentesCursos
    Cursos "1" -- "0..N" AlumnosInscripciones
    Personas "1" -- "0..N" AlumnosInscripciones

    class Personas {
        +int Id
        +int Legajo
        +string Nombre
        +string Apellido
        +string Direccion
        +string Email
        +string Telefono
        +DateTime FechaNacimiento
        +int TipoPersona
        +int IdPlan
    }

    class Usuarios {
        +int Id
        +string NombreUsuario
        +string PasswordHash
        +bool Habilitado
        +bool CambiaClave
        +int IdPersona
    }

    class ModulosUsuarios {
        +int Id
        +int IdModulo
        +int IdUsuario
        +bool PuedeAlta
        +bool PuedeBaja
        +bool PuedeModificar
        +bool PuedeConsultar
    }

    class Modulos {
        +int Id
        +string Descripcion
        +bool Activo
    }

    class Especialidades {
        +int Id
        +string Descripcion
    }

    class Planes {
        +int Id
        +string Descripcion
        +int IdEspecialidad
    }

    class Comisiones {
        +int Id
        +string Descripcion
        +int AnioEspecialidad
        +int IdPlan
    }

    class Materias {
        +int Id
        +string Descripcion
        +int HsSemanales
        +int HsTotales
        +int IdPlan
    }

    class Cursos {
        +int Id
        +int IdMateria
        +int IdComision
        +int AnioCalendario
        +int Cupo
    }

    class DocentesCursos {
        +int Id
        +int IdCurso
        +int IdDocente
        +int Cargo
    }

    class AlumnosInscripciones {
        +int Id
        +int IdAlumno
        +int IdCurso
        +string Condicion
        +int Nota
    }
```
