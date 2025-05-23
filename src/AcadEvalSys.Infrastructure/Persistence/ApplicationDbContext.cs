using AcadEvalSys.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<Usuario>
{
    // Profile and Core Academic Entities
    public DbSet<PerfilAlumno> PerfilesAlumnos { get; set; }
    public DbSet<PerfilProfesor> PerfilesProfesores { get; set; }
    public DbSet<PerfilCoordinador> PerfilesCoordinadores { get; set; }
    public DbSet<Tecnicatura> Tecnicaturas { get; set; }
    public DbSet<Asignatura> Asignaturas { get; set; }
    public DbSet<Competencia> Competencias { get; set; }

    // Survey Related Entities
    public DbSet<PlantillaEncuesta> PlantillasEncuestas { get; set; }
    public DbSet<PreguntaPlantillaEncuesta> PreguntasPlantillasEncuestas { get; set; }
    public DbSet<Encuesta> Encuestas { get; set; }
    public DbSet<PreguntaEncuesta> PreguntasEncuestas { get; set; }
    public DbSet<Encuestado> Encuestados { get; set; }
    public DbSet<RespuestaEncuesta> RespuestasEncuestas { get; set; }

    // Evaluation Related Entities
    public DbSet<Evaluacion> Evaluaciones { get; set; }
    public DbSet<EvaluacionCompetencia> EvaluacionesCompetencias { get; set; }

    // Utility Entities
    public DbSet<EstadoWizard> EstadosWizard { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Usuario - PerfilAlumno: Uno a Uno
        builder.Entity<Usuario>()
            .HasOne(u => u.PerfilAlumno)
            .WithOne(pa => pa.Usuario)
            .HasForeignKey<PerfilAlumno>(pa => pa.UsuarioId);

        // Usuario - PerfilProfesor: Uno a Uno
        builder.Entity<Usuario>()
            .HasOne(u => u.PerfilProfesor)
            .WithOne(pp => pp.Usuario)
            .HasForeignKey<PerfilProfesor>(pp => pp.UsuarioId);

        // Usuario - PerfilCoordinador: Uno a Uno
        builder.Entity<Usuario>()
            .HasOne(u => u.PerfilCoordinador)
            .WithOne(pc => pc.Usuario)
            .HasForeignKey<PerfilCoordinador>(pc => pc.UsuarioId);

        // PerfilAlumno - Tecnicatura: Muchos a Uno (Un PerfilAlumno pertenece a una Tecnicatura)
        builder.Entity<PerfilAlumno>()
            .HasOne(pa => pa.Tecnicatura)
            .WithMany(t => t.PerfilesAlumnos)
            .HasForeignKey(pa => pa.TecnicaturaId);

        // Tecnicatura - PerfilCoordinador: Uno a Uno (Una Tecnicatura tiene un PerfilCoordinador opcional)
        builder.Entity<Tecnicatura>()
            .HasOne(t => t.PerfilCoordinador) 
            .WithOne() // Un PerfilCoordinador puede estar asociado como máximo a una Tecnicatura a través de esta FK.
            .HasForeignKey<Tecnicatura>(t => t.PerfilCoordinadorId) 
            .IsRequired(false); // PerfilCoordinadorId en Tecnicatura es nulable.

        builder.Entity<Tecnicatura>()
            .HasIndex(t => t.PerfilCoordinadorId)
            .IsUnique()
            .HasFilter("[PerfilCoordinadorId] IS NOT NULL"); // Índice único para PerfilCoordinadorId no nulos.

        // PerfilCoordinador - Tecnicatura: Uno a Uno (Un PerfilCoordinador pertenece a una Tecnicatura)
        builder.Entity<PerfilCoordinador>()
            .HasOne(pc => pc.TecnicaturaCoordinada) 
            .WithMany() // Una Tecnicatura, en principio, podría ser referenciada por varios PerfilCoordinador si no fuera por otras restricciones.
                        // La configuración anterior asegura que una Tecnicatura solo apunte a UNO vía PerfilCoordinadorId.
            .HasForeignKey(pc => pc.TecnicaturaId) 
            .IsRequired(true); // TecnicaturaId en PerfilCoordinador no es nulable.

        builder.Entity<PerfilCoordinador>()
            .HasIndex(pc => pc.TecnicaturaId)
            .IsUnique(); // Índice único para TecnicaturaId en PerfilCoordinador.

        // Asignatura - Tecnicatura: Muchos a Uno (Una Asignatura pertenece a una Tecnicatura)
        builder.Entity<Asignatura>()
            .HasOne(a => a.Tecnicatura)
            .WithMany(t => t.Materias) // 'Materias' es la colección en Tecnicatura.
            .HasForeignKey(a => a.TecnicaturaId);

        // Asignatura - PerfilProfesor: Muchos a Uno (Una Asignatura es impartida por un PerfilProfesor opcional)
        // Un PerfilProfesor puede impartir muchas Asignaturas.
        builder.Entity<Asignatura>()
            .HasOne(a => a.PerfilProfesor) 
            .WithMany(p => p.AsignaturasImpartidas) 
            .HasForeignKey(a => a.PerfilProfesorId) 
            .IsRequired(false); // PerfilProfesorId en Asignatura es nulable.

        // PlantillaEncuesta - PreguntaPlantillaEncuesta: Uno a Muchos
        builder.Entity<PlantillaEncuesta>()
            .HasMany(p => p.PreguntasPlantillaEncuesta)
            .WithOne(pp => pp.PlantillaEncuesta)
            .HasForeignKey(pp => pp.PlantillaId);

        // PlantillaEncuesta - Usuario (Creador): Muchos a Uno
        builder.Entity<PlantillaEncuesta>()
            .HasOne(p => p.CreadoPorUsuario)
            .WithMany() // Asumiendo que Usuario no tiene una colección directa de PlantillasCreadas.
            .HasForeignKey(p => p.CreadoPorUsuarioId);

        // PlantillaEncuesta - Encuesta: Uno a Muchos (Una Plantilla puede usarse en muchas Encuestas)
        builder.Entity<PlantillaEncuesta>()
            .HasMany(p => p.Encuestas)
            .WithOne(e => e.PlantillaEncuesta)
            .HasForeignKey(e => e.PlantillaId)
            .IsRequired(false); // PlantillaId en Encuesta es nulable.

        // Encuesta - Usuario (Creador): Muchos a Uno
        builder.Entity<Encuesta>()
            .HasOne(e => e.CreadoPorUsuario)
            .WithMany() // Asumiendo que Usuario no tiene una colección directa de EncuestasCreadas.
            .HasForeignKey(e => e.CreadoPorUsuarioId);

        // Encuesta - PreguntaEncuesta: Uno a Muchos
        builder.Entity<Encuesta>()
            .HasMany(e => e.PreguntasEncuesta)
            .WithOne(pe => pe.Encuesta)
            .HasForeignKey(pe => pe.EncuestaId);

        // Encuesta - Encuestado: Uno a Muchos
        builder.Entity<Encuesta>()
            .HasMany(e => e.Encuestados)
            .WithOne(en => en.Encuesta)
            .HasForeignKey(en => en.EncuestaId);
            
        // Encuestado - RespuestaEncuesta: Uno a Muchos
        builder.Entity<Encuestado>()
            .HasMany(en => en.RespuestasEncuesta)
            .WithOne(re => re.Encuestado)
            .HasForeignKey(re => re.EncuestadoId);
            
        // Encuestado - Usuario: Muchos a Uno (Un Encuestado es un Usuario)
        builder.Entity<Encuestado>()
            .HasOne(en => en.Usuario)
            .WithMany() // Asumiendo que Usuario no tiene una colección directa de participaciones como Encuestado.
            .HasForeignKey(en => en.UsuarioId);

        // PreguntaEncuesta - RespuestaEncuesta: Uno a Muchos
        builder.Entity<PreguntaEncuesta>()
            .HasMany(pe => pe.RespuestasEncuesta)
            .WithOne(re => re.PreguntaEncuesta)
            .HasForeignKey(re => re.PreguntaEncuestaId);

        // Evaluacion - Usuario (Evaluador): Muchos a Uno
        builder.Entity<Evaluacion>()
            .HasOne(ev => ev.EvaluadorUsuario)
            .WithMany() // Si Usuario tiene coleccion 'EvaluacionesComoEvaluador', especificar aquí.
            .HasForeignKey(ev => ev.EvaluadorUsuarioId)
            .OnDelete(DeleteBehavior.Restrict); // Evita ciclos o múltiples rutas de cascada.

        // Evaluacion - Usuario (Evaluado): Muchos a Uno
        builder.Entity<Evaluacion>()
            .HasOne(ev => ev.EvaluadoUsuario)
            .WithMany() // Si Usuario tiene coleccion 'EvaluacionesComoEvaluado', especificar aquí.
            .HasForeignKey(ev => ev.EvaluadoUsuarioId)
            .OnDelete(DeleteBehavior.Restrict); // Evita ciclos o múltiples rutas de cascada.

        // Evaluacion - EvaluacionCompetencia: Uno a Muchos
        builder.Entity<Evaluacion>()
            .HasMany(ev => ev.EvaluacionesCompetencias)
            .WithOne(ec => ec.Evaluacion)
            .HasForeignKey(ec => ec.EvaluacionId);

        // Competencia - EvaluacionCompetencia: Uno a Muchos
        builder.Entity<Competencia>()
            .HasMany(c => c.EvaluacionesCompetencias)
            .WithOne(ec => ec.Competencia)
            .HasForeignKey(ec => ec.CompetenciaId);
            
        // EstadoWizard - Usuario: Muchos a Uno
        builder.Entity<EstadoWizard>()
            .HasOne(ew => ew.Usuario)
            .WithMany() // Si Usuario tiene coleccion de EstadoWizard, especificar aquí.
            .HasForeignKey(ew => ew.UsuarioId);

        // --- Configuraciones de Longitud Máxima para Strings ---
        // ... (el resto de las configuraciones de MaxLength permanecen igual)
    }
}