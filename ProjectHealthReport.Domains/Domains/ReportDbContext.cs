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
        public DbSet<Milestone> Milestones { get; set; }
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
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Code).IsUnique();

            modelBuilder.Entity<Project>()
                .HasIndex(p => new {p.Id, p.DeliveryResponsibleName}).IsUnique();

            modelBuilder.Entity<ProjectAccess>()
                .HasIndex(pa => new {pa.ProjectId, pa.Role, pa.Email}).IsUnique();

            modelBuilder.Entity<ProjectAccess>()
                .Property(pa => pa.Email).IsRequired();

            modelBuilder.Entity<ProjectAccess>()
                .Property(pa => pa.Role).IsRequired();

            modelBuilder.Entity<Milestone>()
                .HasIndex(m => new {m.Date, m.ProjectId}).IsUnique();

            modelBuilder.Entity<AdditionalInfoIssues>()
                .HasKey(ai => new {ai.IssueId, ai.AdditionalInfoId});

            modelBuilder.Entity<BacklogItem>()
                .HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();

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