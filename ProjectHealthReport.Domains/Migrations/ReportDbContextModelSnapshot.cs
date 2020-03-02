﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectHealthReport.Domains.Entities;

namespace ProjectHealthReport.Domains.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    partial class ReportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.AdditionalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("AdditionalInfos");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.AdditionalInfoIssues", b =>
                {
                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<int>("AdditionalInfoId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IssueId", "AdditionalInfoId");

                    b.HasIndex("AdditionalInfoId");

                    b.ToTable("AdditionalInfoIssues");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.AuditLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommandText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommandType")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("EntityId")
                        .HasColumnType("nvarchar(63)")
                        .HasMaxLength(63);

                    b.Property<DateTime>("Recorded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.BacklogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemsAdded")
                        .HasColumnType("int");

                    b.Property<int>("ItemsDone")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("Sprint")
                        .HasColumnType("int");

                    b.Property<int?>("StoryPointsAdded")
                        .HasColumnType("int");

                    b.Property<int?>("StoryPointsDone")
                        .HasColumnType("int");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("BacklogItems");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.DivisionAvailableResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Billable")
                        .HasColumnType("int");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nonbillable")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DivisionAvailableResources");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.DivisionConcern", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Concerns")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DivisionConcerns");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.DivisionFutureResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Project")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DivisionFutureResources");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.DivisionProjectStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Actions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("DivisionProjectStatuses");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.DivisionSoonAvailableResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Availability")
                        .HasColumnType("int");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartingAvailableDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DivisionSoonAvailableResources");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.DivisionUpdatedResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OnBoardDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResourceEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DivisionUpdatedResources");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Impact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OpenedYearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.Milestone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Target")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("Date", "ProjectId")
                        .IsUnique()
                        .HasFilter("[Date] IS NOT NULL");

                    b.ToTable("Milestones");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeliveryResponsibleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JIRALink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyAccountManager")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("NextMileStoneDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlatformVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProjectEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProjectStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SourceCodeLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SprintEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SprintStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.QualityReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CriticalBugs")
                        .HasColumnType("int");

                    b.Property<int>("DoneBugs")
                        .HasColumnType("int");

                    b.Property<int>("MajorBugs")
                        .HasColumnType("int");

                    b.Property<int>("MinorBugs")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("ReOpenBugs")
                        .HasColumnType("int");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("QualityReports");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RetrospectiveFeedBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.WeeklyReportStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsDeadlineMissed")
                        .HasColumnType("bit");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("WeeklyReportStatuses");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.AdditionalInfo", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Entities.Project", "Project")
                        .WithMany("AdditionalInfos")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.AdditionalInfoIssues", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Entities.AdditionalInfo", "AdditionalInfo")
                        .WithMany("AdditionalInfoIssues")
                        .HasForeignKey("AdditionalInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectHealthReport.Domains.Entities.Issue", "Issue")
                        .WithMany("AdditionalInfoIssues")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.BacklogItem", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Entities.Project", "Project")
                        .WithMany("BacklogItems")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.DivisionProjectStatus", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Entities.Project", "Project")
                        .WithMany("DivisionProjectStatuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.Milestone", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Entities.Project", "Project")
                        .WithMany("Milestones")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.QualityReport", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Entities.Project", "Project")
                        .WithMany("QualityReports")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.Status", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Entities.Project", "Project")
                        .WithMany("Statuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Entities.WeeklyReportStatus", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Entities.Project", "Project")
                        .WithMany("WeeklyReportStatuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
