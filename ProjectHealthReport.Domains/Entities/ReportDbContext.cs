﻿using Microsoft.EntityFrameworkCore;

 namespace ProjectHealthReport.Domains.Entities
{
    public class ReportDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.;Database=LastChance;Integrated Security=True");
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<QualityReport> QualityReports { get; set; }
        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<AdditionalInfo> AdditionalInfos { get; set; }
        public DbSet<AdditionalInfoIssues> AdditionalInfoIssues { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<DivisionProjectStatus> DivisionProjectStatuses { get; set; }
        public DbSet<DivisionAvailableResource> DivisionAvailableResources { get; set; }
        public DbSet<DivisionFutureResource> DivisionFutureResources { get; set; }
        public DbSet<DivisionSoonAvailableResource> DivisionSoonAvailableResources { get; set; }
        public DbSet<DivisionUpdatedResource> DivisionUpdatedResources { get; set; }
        public DbSet<DivisionConcern> DivisionConcerns { get; set; }
        public DbSet<WeeklyReportStatus> WeeklyReportStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Name).IsUnique();
            
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Code).IsUnique();
            
            modelBuilder.Entity<Milestone>()
                .HasIndex(m => new { m.Date, m.ProjectId }).IsUnique();

            modelBuilder.Entity<AdditionalInfoIssues>()
                .HasKey(ai => new { ai.IssueId, ai.AdditionalInfoId });

            modelBuilder.Entity<BacklogItem>()
                .HasIndex(b => new { b.YearWeek, b.ProjectId }).IsUnique();

            modelBuilder.Entity<QualityReport>()
                .HasIndex(b => new { b.YearWeek, b.ProjectId }).IsUnique();

            modelBuilder.Entity<Status>()
                .HasIndex(b => new { b.YearWeek, b.ProjectId }).IsUnique();

            modelBuilder.Entity<AdditionalInfo>()
               .HasIndex(b => new { b.YearWeek, b.ProjectId }).IsUnique();
            
            modelBuilder.Entity<WeeklyReportStatus>()
                .HasIndex(b => new { b.YearWeek, b.ProjectId }).IsUnique();


            modelBuilder.Entity<DivisionProjectStatus>()
                .HasIndex(b => new { b.YearWeek, b.ProjectId }).IsUnique();

        }
    }
}