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
    [Migration("20191204035109_AddStatus_BacklogItems_QualityReports_AdditionalInfoEntities")]
    partial class AddStatus_BacklogItems_QualityReports_AdditionalInfoEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectHealthReport.DataAccess.AdditionalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action");

                    b.Property<string>("Impact");

                    b.Property<string>("Item");

                    b.Property<int?>("ProjectId");

                    b.Property<string>("Status");

                    b.Property<int>("Week");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("AdditionalInfo");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.BacklogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemsAdded");

                    b.Property<int>("ItemsDone");

                    b.Property<int>("ItemsRemaining");

                    b.Property<int?>("ProjectId");

                    b.Property<int>("Sprint");

                    b.Property<int>("StoryPointsAdded");

                    b.Property<int>("StoryPointsDone");

                    b.Property<int>("StoryPointsRemaining");

                    b.Property<int>("Week");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("BacklogItem");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeliveryResponsibleName")
                        .IsRequired();

                    b.Property<string>("Division")
                        .IsRequired();

                    b.Property<string>("JIRALink")
                        .IsRequired();

                    b.Property<string>("KeyAccountManager")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime?>("NextMileStoneDate");

                    b.Property<DateTime?>("ProjectEndDate");

                    b.Property<DateTime>("ProjectStartDate");

                    b.Property<string>("SourceCodeLink")
                        .IsRequired();

                    b.Property<DateTime?>("SprintEndDate");

                    b.Property<DateTime?>("SprintStartDate");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.QualityReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CriticalBugs");

                    b.Property<int>("DoneBugs");

                    b.Property<int>("MajorBugs");

                    b.Property<int>("MinorBugs");

                    b.Property<int?>("ProjectId");

                    b.Property<int>("ReOpenBugs");

                    b.Property<int>("RemainingBugs");

                    b.Property<int>("Week");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("QualityReport");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectId");

                    b.Property<string>("ProjectStatus")
                        .IsRequired();

                    b.Property<string>("StatusColor")
                        .IsRequired();

                    b.Property<int>("Week");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.AdditionalInfo", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("AdditionalInfos")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.BacklogItem", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("BacklogItems")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.QualityReport", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("QualityReports")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("ProjectHealthReport.DataAccess.Status", b =>
                {
                    b.HasOne("ProjectHealthReport.DataAccess.Project", "Project")
                        .WithMany("Statuses")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
