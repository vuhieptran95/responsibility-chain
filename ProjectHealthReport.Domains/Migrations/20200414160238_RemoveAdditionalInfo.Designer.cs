﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectHealthReport.Domains.Domains;

namespace ProjectHealthReport.Domains.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    [Migration("20200414160238_RemoveAdditionalInfo")]
    partial class RemoveAdditionalInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.AuditLog", b =>
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

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.BacklogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemsAdded")
                        .HasColumnName("ItemsAdded")
                        .HasColumnType("int");

                    b.Property<int>("ItemsDone")
                        .HasColumnName("ItemsDone")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("Sprint")
                        .HasColumnName("Sprint")
                        .HasColumnType("int");

                    b.Property<int?>("StoryPointsAdded")
                        .HasColumnName("StoryPointsAdded")
                        .HasColumnType("int");

                    b.Property<int?>("StoryPointsDone")
                        .HasColumnName("StoryPointsDone")
                        .HasColumnType("int");

                    b.Property<int>("YearWeek")
                        .HasColumnName("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("BacklogItems");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.DivisionProjectStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Actions")
                        .HasColumnName("Actions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectStatus")
                        .HasColumnName("ProjectStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusColor")
                        .IsRequired()
                        .HasColumnName("StatusColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("DivisionProjectStatuses");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.DoDReport", b =>
                {
                    b.Property<int>("MetricId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.Property<string>("LinkToReport")
                        .HasColumnName("LinkToReport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportFileName")
                        .HasColumnName("ReportFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnName("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetricId", "ProjectId", "YearWeek");

                    b.HasIndex("ProjectId");

                    b.ToTable("DoDReports");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.Metric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnName("Order")
                        .HasColumnType("int");

                    b.Property<string>("SelectValues")
                        .HasColumnName("SelectValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tool")
                        .IsRequired()
                        .HasColumnName("Tool")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToolOrder")
                        .HasColumnName("ToolOrder")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .HasColumnName("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValueType")
                        .IsRequired()
                        .HasColumnName("ValueType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.MetricStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MetricStatuses");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryResponsibleName")
                        .HasColumnName("DeliveryResponsibleName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnName("Division")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DmrRequired")
                        .HasColumnName("DmrRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DmrRequiredFrom")
                        .HasColumnName("DmrRequiredFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DmrRequiredTo")
                        .HasColumnName("DmrRequiredTo")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DodRequired")
                        .HasColumnName("DodRequired")
                        .HasColumnType("bit");

                    b.Property<string>("JiraLink")
                        .HasColumnName("JiraLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyAccountManager")
                        .IsRequired()
                        .HasColumnName("KeyAccountManager")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnName("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhrRequired")
                        .HasColumnName("PhrRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PhrRequiredFrom")
                        .HasColumnName("PhrRequiredFrom")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlatformVersion")
                        .HasColumnName("PlatformVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProjectEndDate")
                        .HasColumnName("ProjectEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProjectStartDate")
                        .HasColumnName("ProjectStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectStateTypeId")
                        .HasColumnName("ProjectStateTypeId")
                        .HasColumnType("int");

                    b.Property<string>("SourceCodeLink")
                        .HasColumnName("SourceCodeLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("ProjectStateTypeId");

                    b.HasIndex("Id", "DeliveryResponsibleName")
                        .IsUnique()
                        .HasFilter("[DeliveryResponsibleName] IS NOT NULL");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.ProjectAccess", b =>
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

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.ProjectStateType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnName("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectStateTypes");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.QualityReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CriticalBugs")
                        .HasColumnName("CriticalBugs")
                        .HasColumnType("int");

                    b.Property<int>("DoneBugs")
                        .HasColumnName("DoneBugs")
                        .HasColumnType("int");

                    b.Property<int>("MajorBugs")
                        .HasColumnName("MajorBugs")
                        .HasColumnType("int");

                    b.Property<int>("MinorBugs")
                        .HasColumnName("MinorBugs")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("ReOpenBugs")
                        .HasColumnName("ReOpenBugs")
                        .HasColumnType("int");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("QualityReports");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Milestone")
                        .HasColumnName("Milestone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MilestoneDate")
                        .HasColumnName("MilestoneDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectStatus")
                        .HasColumnName("ProjectStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RetrospectiveFeedBack")
                        .HasColumnName("RetrospectiveFeedBack")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusColor")
                        .IsRequired()
                        .HasColumnName("StatusColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("YearWeek", "ProjectId")
                        .IsUnique();

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.Threshold", b =>
                {
                    b.Property<int>("MetricId")
                        .HasColumnType("int");

                    b.Property<int>("MetricStatusId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRange")
                        .HasColumnName("IsRange")
                        .HasColumnType("bit");

                    b.Property<decimal?>("LowerBound")
                        .HasColumnName("LowerBound")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LowerBoundOperator")
                        .HasColumnName("LowerBoundOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("UpperBound")
                        .HasColumnName("UpperBound")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UpperBoundOperator")
                        .HasColumnName("UpperBoundOperator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnName("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetricId", "MetricStatusId");

                    b.HasIndex("MetricStatusId");

                    b.ToTable("Thresholds");
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.WeeklyReportStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsDeadlineMissed")
                        .HasColumnName("IsDeadlineMissed")
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

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.BacklogItem", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.Project", "Project")
                        .WithMany("BacklogItems")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.DivisionProjectStatus", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.Project", "Project")
                        .WithMany("DivisionProjectStatuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.DoDReport", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.Metric", "Metric")
                        .WithMany("DoDReports")
                        .HasForeignKey("MetricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectHealthReport.Domains.Domains.Project", "Project")
                        .WithMany("DoDReports")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.Project", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.ProjectStateType", "ProjectStateType")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectStateTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.ProjectAccess", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.Project", null)
                        .WithMany("ProjectAccesses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.QualityReport", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.Project", "Project")
                        .WithMany("QualityReports")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.Status", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.Project", "Project")
                        .WithMany("Statuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.Threshold", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.Metric", "Metric")
                        .WithMany("Thresholds")
                        .HasForeignKey("MetricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectHealthReport.Domains.Domains.MetricStatus", "MetricStatus")
                        .WithMany("Thresholds")
                        .HasForeignKey("MetricStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectHealthReport.Domains.Domains.WeeklyReportStatus", b =>
                {
                    b.HasOne("ProjectHealthReport.Domains.Domains.Project", "Project")
                        .WithMany("WeeklyReportStatuses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
