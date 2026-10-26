# Sistema de Gestión Académica (SGA) — TPI Academia

> **Trabajo Práctico Integrador**  
> **Materia:** Tecnologías de Desarrollo de Software IDE  
> **Universidad Tecnológica Nacional — Facultad Regional Rosario (UTN FRRo)**  
> **Año:** 2026

---

## Integrantes — Grupo N° 3

| Integrante | Legajo | 
| :--- | :---: | 
| **Martín Cabrera** | 53952 |
| **Nicolás Bolzico** | 53742 | 

---

## Sistema

El sistema elegido es el **Sistema de Gestión Académica (SGA)** propuesto por la cátedra.
```mermaid
erDiagram
    Personas ||--o| Usuarios : ""
    Usuarios ||--o{ ModulosUsuarios : ""
    Modulos ||--o{ ModulosUsuarios : ""
    Especialidades ||--o{ Planes : ""
    Planes ||--o{ Personas : ""
    Planes ||--|{ Materias : ""
    Planes ||--o{ Comisiones : ""
    Materias ||--o{ Cursos : ""
    Comisiones ||--o{ Cursos : ""
    Cursos ||--o{ DocentesCursos : ""
    Personas ||--o{ DocentesCursos : ""
    Cursos ||--o{ AlumnosInscripciones : ""
    Personas ||--o{ AlumnosInscripciones : ""

    Personas {
        int Id PK
        int Legajo
        string Nombre
        string Apellido
        string Direccion
        string Email
        string Telefono
        DateTime FechaNacimiento
        int TipoPersona
        int IdPlan FK
    }

    Usuarios {
        int Id PK
        string NombreUsuario
        string PasswordHash
        bool Habilitado
        bool CambiaClave
        int IdPersona FK
    }

    ModulosUsuarios {
        int Id PK
        int IdModulo FK
        int IdUsuario FK
        bool PuedeAlta
        bool PuedeBaja
        bool PuedeModificar
        bool PuedeConsultar
    }

    Modulos {
        int Id PK
        string Descripcion
        bool Activo
    }

    Especialidades {
        int Id PK
        string Descripcion
    }

    Planes {
        int Id PK
        string Descripcion
        int IdEspecialidad FK
    }

    Comisiones {
        int Id PK
        string Descripcion
        int AnioEspecialidad
        int IdPlan FK
    }

    Materias {
        int Id PK
        string Descripcion
        int HsSemanales
        int HsTotales
        int IdPlan FK
    }

    Cursos {
        int Id PK
        int IdMateria FK
        int IdComision FK
        int AnioCalendario
        int Cupo
    }

    DocentesCursos {
        int Id PK
        int IdCurso FK
        int IdDocente FK
        int Cargo
    }

    AlumnosInscripciones {
        int Id PK
        int IdAlumno FK
        int IdCurso FK
        string Condicion
        int Nota
    }
```
