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
erDiagram
    personas ||--o| usuarios : ""
    usuarios ||--o{ modulos_usuarios : ""
    modulos ||--o{ modulos_usuarios : ""
    especialidades ||--o{ planes : ""
    planes ||--o{ personas : ""
    planes ||--o{ materias : ""
    planes ||--o{ comisiones : ""
    materias ||--o{ cursos : ""
    comisiones ||--o{ cursos : ""
    cursos ||--o{ docentes_cursos : ""
    personas ||--o{ docentes_cursos : ""
    cursos ||--o{ alumnos_inscripciones : ""
    personas ||--o{ alumnos_inscripciones : ""

    usuarios {
        int id_usuario
        varchar nombre_usuario
        varchar clave
        bit habilitado
        varchar nombre
        varchar apellido
        varchar email
        bit cambia_clave
        int id_persona
    }

    modulos_usuarios {
        int id_modulo_usuario
        int id_modulo
        int id_usuario
        bit alta
        bit baja
        bit modificacion
        bit consulta
    }

    modulos {
        int id_modulo
        varchar desc_modulo
        varchar ejecuta
    }

    personas {
        int id_persona
        varchar nombre
        varchar apellido
        varchar direccion
        varchar email
        varchar telefono
        datetime fecha_nac
        int legajo
        int tipo_persona
        int id_plan
    }

    especialidades {
        int id_especialidad
        varchar desc_especialidad
    }

    planes {
        int id_plan
        varchar desc_plan
        int id_especialidad
    }

    comisiones {
        int id_comision
        varchar desc_comision
        int anio_especialidad
        int id_plan
    }

    materias {
        int id_materia
        varchar desc_materia
        int hs_semanales
        int hs_totales
        int id_plan
    }

    cursos {
        int id_curso
        int id_materia
        int id_comision
        int anio_calendario
        int cupo
    }

    docentes_cursos {
        int id_dictado
        int id_curso
        int id_docente
        int cargo
    }

    alumnos_inscripciones {
        int id_inscripcion
        int id_alumno
        int id_curso
        varchar condicion
        int nota
    }
```
