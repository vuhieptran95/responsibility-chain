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
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<MetricStatus> MetricStatuses { get; set; }
        public DbSet<Threshold> Thresholds { get; set; }
        public DbSet<DoDReport> DoDReports { get; set; }
        public DbSet<DivisionProjectStatus> DivisionProjectStatuses { get; set; }
        public DbSet<WeeklyReportStatus> WeeklyReportStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigProjectModel(modelBuilder);

            modelBuilder.Entity<Metric>(e =>
            {
                e.HasKey(m => m.Id);
                e.Property(m => m.Name).HasColumnName("Name").IsRequired();
                e.Property(m => m.ValueType).HasColumnName("ValueType").IsRequired();
                e.Property(m => m.Unit).HasColumnName("Unit");
                e.Property(m => m.Order).HasColumnName("Order");
                e.Property(m => m.Tool).HasColumnName("Tool").IsRequired();
                e.Property(m => m.ToolOrder).HasColumnName("ToolOrder");
                e.Property(m => m.SelectValues).HasColumnName("SelectValues");
                e.HasMany(m => m.Thresholds).WithOne(t => t.Metric)
                    .HasForeignKey(t => t.MetricId);
                e.HasMany(m => m.DoDReports).WithOne(d => d.Metric)
                    .HasForeignKey(d => d.MetricId);
            });

            modelBuilder.Entity<MetricStatus>(e =>
            {
                e.HasKey(m => m.Id);
                e.Property(m => m.Name).HasColumnName("Name").IsRequired();
                e.HasMany(m => m.Thresholds).WithOne(t => t.MetricStatus)
                    .HasForeignKey(t => t.MetricStatusId);
            });

            modelBuilder.Entity<Threshold>(e =>
            {
                e.HasKey(t => new {t.MetricId, t.MetricStatusId});
                e.Property(t => t.Value).HasColumnName("Value");
                e.Property(t => t.IsRange).HasColumnName("IsRange");
                e.Property(t => t.LowerBound).HasColumnName("LowerBound");
                e.Property(t => t.UpperBound).HasColumnName("UpperBound");
                e.Property(t => t.LowerBoundOperator).HasColumnName("LowerBoundOperator");
                e.Property(t => t.UpperBoundOperator).HasColumnName("UpperBoundOperator");
            });

            modelBuilder.Entity<ProjectAccess>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasIndex(pa => new {pa.ProjectId, pa.Role, pa.Email}).IsUnique();
                e.Property(pa => pa.Email).IsRequired();
                e.Property(pa => pa.Role).IsRequired();
            });


            modelBuilder.Entity<BacklogItem>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();
                e.Property(b => b.Sprint).HasColumnName("Sprint");
                e.Property(b => b.ItemsAdded).HasColumnName("ItemsAdded").IsRequired();
                e.Property(b => b.ItemsDone).HasColumnName("ItemsDone").IsRequired();
                e.Property(b => b.YearWeek).HasColumnName("YearWeek");
                e.Property(b => b.StoryPointsAdded).HasColumnName("StoryPointsAdded");
                e.Property(b => b.StoryPointsDone).HasColumnName("StoryPointsDone");
            });

            modelBuilder.Entity<QualityReport>(e =>
            {
                e.HasKey(q => q.Id);
                e.HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();
                e.Property(q => q.CriticalBugs).HasColumnName("CriticalBugs");
                e.Property(q => q.DoneBugs).HasColumnName("DoneBugs");
                e.Property(q => q.MajorBugs).HasColumnName("MajorBugs");
                e.Property(q => q.MinorBugs).HasColumnName("MinorBugs");
                e.Property(q => q.ReOpenBugs).HasColumnName("ReOpenBugs");
            });

            modelBuilder.Entity<Status>(e =>
            {
                e.HasKey(s => s.Id);
                e.HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();
                e.Property(s => s.Milestone).HasColumnName("Milestone");
                e.Property(s => s.MilestoneDate).HasColumnName("MilestoneDate");
                e.Property(s => s.ProjectStatus).HasColumnName("ProjectStatus");
                e.Property(s => s.StatusColor).HasColumnName("StatusColor").IsRequired();
                e.Property(s => s.RetrospectiveFeedBack).HasColumnName("RetrospectiveFeedBack");
            });

            modelBuilder.Entity<WeeklyReportStatus>(e =>
            {
                e.HasKey(w => w.Id);
                e.HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();
                e.Property(w => w.IsDeadlineMissed).HasColumnName("IsDeadlineMissed");
            });

            modelBuilder.Entity<DivisionProjectStatus>(e =>
            {
                e.HasKey(d => d.Id);
                e.HasIndex(b => new {b.YearWeek, b.ProjectId}).IsUnique();
                e.Property(d => d.Actions).HasColumnName("Actions");
                e.Property(d => d.ProjectStatus).HasColumnName("ProjectStatus");
                e.Property(d => d.StatusColor).HasColumnName("StatusColor").IsRequired();
            });

            modelBuilder.Entity<DoDReport>(e =>
            {
                e.HasKey(d => new {d.MetricId, d.ProjectId, d.YearWeek});
                e.Property(d => d.Value).HasColumnName("Value");
                e.Property(d => d.LinkToReport).HasColumnName("LinkToReport");
                e.Property(d => d.ReportFileName).HasColumnName("ReportFileName");
            });

            modelBuilder.Entity<ProjectStateType>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.State).HasColumnName("State").IsRequired();
            });
        }

        private static void ConfigProjectModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasIndex(p => p.Code).IsUnique();
                e.HasIndex(p => new {p.Id, p.DeliveryResponsibleName}).IsUnique();
                e.Property(p => p.Name).HasColumnName("Name").IsRequired();
                e.Property(p => p.Code).HasColumnName("Code").IsRequired();
                e.Property(p => p.KeyAccountManager).HasColumnName("KeyAccountManager").IsRequired();
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
                e.Property(p => p.Division).HasColumnName("Division").IsRequired();
                e.HasMany(p => p.Statuses).WithOne(s => s.Project)
                    .HasForeignKey(s => s.ProjectId);
                e.HasMany(p => p.BacklogItems).WithOne(b => b.Project)
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
        }
    }
}