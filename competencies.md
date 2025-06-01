## 📋 Resumen General

El sistema permite a los profesores evaluar estudiantes en competencias específicas durante períodos de evaluación definidos.

### **Estructura Educativa**

- **TechnicalCareer**: Representa tecnicaturas (ej: Desarrollo de Software)
- **Subject**: Asignaturas dentro de cada carrera, asignadas a años específicos (1º, 2º, 3º año)
- **Professor**: Instructores que imparten asignaturas (1 profesor por asignatura por año por carrera)
- **Student**: Estudiantes inscriptos en asignaturas

### **Marco de Evaluación**

- **Competency**: Habilidades/capacidades que necesitan ser evaluadas, vinculadas a carreras y años específicos
- **FormQuestion**: Preguntas utilizadas para evaluar cada competencia (escala Likert 1-5)
- **EvaluationPeriod**: Ventanas de tiempo cuando las evaluaciones tienen lugar (con notificaciones y recordatorios)

### **Flujo de Asignación y Evaluación**

1. **ProfessorCompetencyAssignment**: Vincula profesores con competencias que deben evaluar durante un período específico
2. **StudentCompetencyEvaluation**: Evaluaciones individuales de estudiantes por parte de profesores
3. **QuestionResponse**: Respuestas específicas a preguntas del formulario con puntajes numéricos y comentarios

## 🔄 Reglas de Negocio

- Cada profesor imparte **una asignatura por año por carrera técnica**
- Los estudiantes son evaluados en competencias **relevantes a sus asignaturas inscriptas**
- Todos los estudiantes de una asignatura son evaluados usando las **mismas preguntas del formulario**
- Los profesores proporcionan **puntajes individuales y retroalimentación** para cada estudiante
- Los períodos de evaluación tienen **sistemas configurables de notificación y recordatorios**

## 🎯 Relaciones Clave

- **Dirigido por períodos**: Todo fluye desde EvaluationPeriod → Asignaciones → Evaluaciones Individuales
- **Jerárquico**: TechnicalCareer → Subject → Professor/Student → Competency → Questions → Responses
- **Trazable**: Rastro de auditoría completo desde la creación del período hasta los puntajes finales del estudiante

```mermaid
erDiagram

  TechnicalCareer {
    varchar id PK
    varchar name
    varchar code
    timestamp created_at
  }

  Subject {
    varchar id PK
    varchar name
    varchar technical_career_id FK
    integer year
    varchar professor_id FK
    timestamp created_at
  }

  Professor {
    varchar id PK
    varchar first_name
    varchar last_name
    varchar email
    varchar phone
    boolean is_active
    timestamp created_at
  }

  Student {
    varchar id PK
    varchar first_name
    varchar last_name
    varchar email
    boolean is_active
    timestamp created_at
  }

  StudentSubject {
    varchar id PK
    varchar student_id FK
    varchar subject_id FK
    boolean is_active
    timestamp created_at
  }

  Competency {
    varchar id PK
    varchar name
    varchar description
    varchar type
    varchar technical_career_id FK
    integer year
    timestamp created_at
  }

  FormQuestion {
    varchar id PK
    varchar text
    integer order
    boolean is_required
    varchar competency_id FK
    timestamp created_at
  }

  EvaluationPeriod {
    varchar id PK
    varchar title
    varchar description
    timestamp period_from
    timestamp period_to
    boolean notify_start
    boolean send_reminders
    boolean notify_close
    varchar reminder_frequency
    varchar status
    timestamp created_at
  }

  ProfessorCompetencyAssignment {
    varchar id PK
    varchar evaluation_id FK
    varchar professor_id FK
    varchar technical_career_id FK
    varchar technical_career_name
    integer year
    varchar competency_id FK
    varchar competency_name
    varchar form_name
    varchar status
    timestamp notification_sent_at
    timestamp created_at
  }

  StudentCompetencyEvaluation {
    varchar id PK
    varchar professor_competency_assignment_id FK
    varchar student_id FK
    varchar student_name
    varchar status
    timestamp completed_at
    text comments
    decimal final_score
    timestamp created_at
  }

  QuestionResponse {
    varchar id PK
    varchar question_id FK
    integer numeric_value
    varchar comments
    varchar student_competency_evaluation_id FK
    timestamp created_at
  }

  %% Relationships
  TechnicalCareer ||--o{ Subject : has
  TechnicalCareer ||--o{ Competency : has
  Subject }o--|| Professor : taught_by
  Student ||--o{ StudentSubject : enrolled_in
  Subject ||--o{ StudentSubject : includes
  Competency ||--o{ FormQuestion : has
  EvaluationPeriod ||--o{ ProfessorCompetencyAssignment : contains
  Professor ||--o{ ProfessorCompetencyAssignment : assigned_to
  TechnicalCareer ||--o{ ProfessorCompetencyAssignment : involves
  Competency ||--o{ ProfessorCompetencyAssignment : evaluates
  ProfessorCompetencyAssignment ||--o{ StudentCompetencyEvaluation : evaluates
  Student ||--o{ StudentCompetencyEvaluation : participates
  StudentCompetencyEvaluation ||--o{ QuestionResponse : answers
  FormQuestion ||--o{ QuestionResponse : asked

```
