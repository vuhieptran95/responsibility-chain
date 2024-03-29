﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectHealthReport.Domains.Domains;

namespace ProjectHealthReport.Domains.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    [Migration("20200325093334_AddDodRequiredField")]
    partial class AddDodRequiredField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectHealthReport.DataAccess.AdditionalInfo", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.AdditionalInfoIssues", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.AuditLog", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.BacklogItem", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionAvailableResource", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionConcern", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionFutureResource", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionProjectStatus", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionSoonAvailableResource", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionUpdatedResource", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DoDReport", b =>
                {
                    b.Property<int>("MetricId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetricId", "ProjectId", "YearWeek");

                    b.HasIndex("ProjectId");

                    b.ToTable("DoDReports");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Issue", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Metric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SelectValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tool")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValueType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.MetricStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MetricStatuses");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Milestone", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryResponsibleName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DmrRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DmrRequiredFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DmrRequiredTo")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DodRequired")
                        .HasColumnType("bit");

                    b.Property<string>("JIRALink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyAccountManager")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NextMileStoneDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhrRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PhrRequiredFrom")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlatformVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProjectEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProjectStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectStateTypeId")
                        .HasColumnType("int");

                    b.Property<string>("SourceCodeLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SprintEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SprintStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("ProjectStateTypeId");

                    b.HasIndex("Id", "DeliveryResponsibleName")
                        .IsUnique()
                        .HasFilter("[DeliveryResponsibleName] IS NOT NULL");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.ProjectAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId", "Role", "Email")
                        .IsUnique();

                    b.ToTable("ProjectAccesses");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.ProjectStateType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectStateTypes");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.QualityReport", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Milestone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MilestoneDate")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Threshold", b =>
                {
                    b.Property<int>("MetricId")
                        .HasColumnType("int");

                    b.Property<int>("MetricStatusId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRange")
                        .HasColumnType("bit");

                    b.Property<decimal?>("LowerBound")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("UpperBound")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetricId", "MetricStatusId");

                    b.HasIndex("MetricStatusId");

                    b.ToTable("Thresholds");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.WeeklyReportStatus", b =>
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.AdditionalInfo", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("AdditionalInfos")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.AdditionalInfoIssues", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.AdditionalInfo", "AdditionalInfo")
                        .WithMany("AdditionalInfoIssues")
                        .HasForeignKey("AdditionalInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectHealthReport.DataAccess.Issue", "Issue")
                        .WithMany("AdditionalInfoIssues")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.BacklogItem", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("BacklogItems")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionProjectStatus", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("DivisionProjectStatuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DoDReport", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Metric", "Metric")
                        .WithMany("DoDReports")
                        .HasForeignKey("MetricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("DoDReports")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Milestone", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("Milestones")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Project", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.ProjectStateType", "ProjectStateType")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectStateTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.ProjectAccess", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("ProjectAccesses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.QualityReport", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("QualityReports")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Status", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("Statuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Threshold", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Metric", "Metric")
                        .WithMany("Thresholds")
                        .HasForeignKey("MetricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectHealthReport.DataAccess.MetricStatus", "MetricStatus")
                        .WithMany("Thresholds")
                        .HasForeignKey("MetricStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.WeeklyReportStatus", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("WeeklyReportStatuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
