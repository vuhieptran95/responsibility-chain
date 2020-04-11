using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProjectHealthReport.Domains.Domains
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAccess> ProjectAccesses { get; set; }
        public DbSet<ProjectStateType> ProjectStateTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<QualityReport> QualityReports { get; set; }
        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<AdditionalInfo> AdditionalInfos { get; set; }
        public DbSet<AdditionalInfoIssues> AdditionalInfoIssues { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<MetricStatus> MetricStatuses { get; set; }
        public DbSet<Threshold> Thresholds { get; set; }
        public DbSet<DoDReport> DoDReports { get; set; }
        public DbSet<DivisionProjectStatus> DivisionProjectStatuses { get; set; }
        public DbSet<WeeklyReportStatus> WeeklyReportStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasIndex(p => p.Code).IsUnique();
                e.HasIndex(p => new {p.Id, p.DeliveryResponsibleName}).IsUnique();
                e.Property(p => p.Name).HasColumnName("Name");
                e.Property(p => p.Code).HasColumnName("Code");
                e.Property(p => p.KeyAccountManager).HasColumnName("KeyAccountManager");
                e.Property(p => p.PhrRequired).HasColumnName("PhrRequired");
                e.Property(p => p.DmrRequired).HasColumnName("DmrRequired");
                e.Property(p => p.DodRequired).HasColumnName("DodRequired");
                e.Property(p => p.ProjectStateTypeId).HasColumnName("ProjectStateTypeId");
                e.Property(p => p.DeliveryResponsibleName).HasColumnName("DeliveryResponsibleName");
                e.Property(p => p.PlatformVersion).HasColumnName("PlatformVersion");
                e.Property(p => p.JiraLink).HasColumnName("JiraLink");
                e.Property(p => p.SourceCodeLink).HasColumnName("SourceCodeLink");
                e.Property(p => p.Note).HasColumnName("Note");
                e.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                e.Property(p => p.ProjectStartDate).HasColumnName("ProjectStartDate");
                e.Property(p => p.ProjectEndDate).HasColumnName("ProjectEndDate");
                e.Property(p => p.PhrRequiredFrom).HasColumnName("PhrRequiredFrom");
                e.Property(p => p.DmrRequiredFrom).HasColumnName("DmrRequiredFrom");
                e.Property(p => p.DmrRequiredTo).HasColumnName("DmrRequiredTo");
                e.Property(p => p.Division).HasColumnName("Division");
                e.HasMany(p => p.Statuses).WithOne(s => s.Project)
                    .HasForeignKey(s => s.ProjectId);
                e.HasMany(p => p.BacklogItems).WithOne(b => b.Project)
                    .HasForeignKey(s => s.ProjectId);
                e.HasMany(p => p.AdditionalInfos).WithOne(a => a.Project)
                    .HasForeignKey(s => s.ProjectId);
                e.HasMany(p => p.QualityReports).WithOne(q => q.Project)
                    .HasForeignKey(s => s.ProjectId);
                e.HasMany(p => p.DivisionProjectStatuses).WithOne(d => d.Project)
                    .HasForeignKey(s => s.ProjectId);
                e.HasMany(p => p.DoDReports).WithOne(d => d.Project)
                    .HasForeignKey(s => s.ProjectId);
                e.HasMany(p => p.WeeklyReportStatuses).WithOne(w => w.Project)
                    .HasForeignKey(s => s.ProjectId);
                e.HasOne(p => p.ProjectStateType)
                    .WithMany(n => n.Projects)
                    .HasForeignKey(p => p.ProjectStateTypeId);
            });

            modelBuilder.Entity<ProjectAccess>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasIndex(pa => new {pa.ProjectId, pa.Role, pa.Email}).IsUnique();
                e.Property(pa => pa.Email).IsRequired();
                e.Property(pa => pa.Role).IsRequired();
            });

            modelBuilder.Entity<AdditionalInfoIssues>()
                .HasKey(ai => new {ai.IssueId, ai.AdditionalInfoId});

            modelBuilder.Entity<BacklogItem>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();
            });

            modelBuilder.Entity<QualityReport>()
                .HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();

            modelBuilder.Entity<Status>()
                .HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();

            modelBuilder.Entity<AdditionalInfo>()
                .HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();

            modelBuilder.Entity<WeeklyReportStatus>()
                .HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();

            modelBuilder.Entity<DivisionProjectStatus>()
                .HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();

            modelBuilder.Entity<DoDReport>()
                .HasKey(d => new {d.MetricId, d.ProjectId, d.YearWeek});

            modelBuilder.Entity<Threshold>()
                .HasKey(t => new {t.MetricId, t.MetricStatusId});
        }
    }
}