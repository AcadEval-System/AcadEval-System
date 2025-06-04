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
    public DbSet<CareerCompetencies> CareerCompetencies { get; set; }
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

        builder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.UserId);

            entity.HasOne(s => s.User)
                  .WithOne(u => u.Student)
                  .HasForeignKey<Student>(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Professor>(entity =>
        {
            entity.HasKey(p => p.UserId);

            entity.HasOne(p => p.User)
                  .WithOne(u => u.Professor)
                  .HasForeignKey<Professor>(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Coordinator>(entity =>
        {
            entity.HasKey(c => c.UserId);

            entity.HasOne(c => c.User)
                  .WithOne(u => u.Coordinator)
                  .HasForeignKey<Coordinator>(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CareerCompetencies>(entity =>
        {
            entity.HasKey(cc => new { cc.CareerId, cc.CompetencyId, cc.CareerYear });
        });

        builder.Entity<CareerCompetencies>(entity =>
        {
            entity.HasOne(cc => cc.Career)
                .WithMany(c => c.CareerCompetencies)
                .HasForeignKey(cc => cc.CareerId);
        });

        builder.Entity<CareerCompetencies>(entity =>
        {
            entity.HasOne(cc => cc.Competency)
                .WithMany(c => c.CareerCompetencies)
                .HasForeignKey(cc => cc.CompetencyId);
        });
    }
}