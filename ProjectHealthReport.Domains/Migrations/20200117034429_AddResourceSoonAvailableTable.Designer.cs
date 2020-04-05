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
    [Migration("20200117034429_AddResourceSoonAvailableTable")]
    partial class AddResourceSoonAvailableTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionProjectStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.DivisionResourceAvailability", b =>
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ResourceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("YearWeek", "ResourceEmail")
                        .IsUnique();

                    b.ToTable("DivisionResourceAvailability");
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

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Projects");
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
#pragma warning restore 612, 618
        }
    }
}
