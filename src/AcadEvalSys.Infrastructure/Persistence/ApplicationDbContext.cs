using AcadEvalSys.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcadEvalSys.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User>
{
    // Core Academic Entities
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<Coordinator> Coordinators { get; set; }
    public DbSet<TechnicalCareer> TechnicalCareers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Competency> Competencies { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }

    // Evaluation, Survey, and Form Entities
    public DbSet<EvaluationPeriod> EvaluationPeriods { get; set; }
    public DbSet<FormQuestion> FormQuestions { get; set; }
    public DbSet<QuestionResponse> QuestionResponses { get; set; }
    public DbSet<ProfessorCompetencyAssignment> ProfessorCompetencyAssignments { get; set; }
    public DbSet<StudentCompetencyEvaluation> StudentCompetencyEvaluations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Student>().HasKey(s => s.UserId);
        builder.Entity<Professor>().HasKey(p => p.UserId);
        builder.Entity<Coordinator>().HasKey(c => c.UserId);
     
    }
}