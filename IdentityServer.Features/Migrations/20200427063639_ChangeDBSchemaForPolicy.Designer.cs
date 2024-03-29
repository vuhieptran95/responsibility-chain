﻿// <auto-generated />
using IdentityServer.Features.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityServer.Features.Migrations
{
    [DbContext(typeof(IdPDbContext))]
    [Migration("20200427063639_ChangeDBSchemaForPolicy")]
    partial class ChangeDBSchemaForPolicy
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdentityServer.Features.Domains.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RedirectedUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Secret")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UserInteractionRequired")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.ClientScope", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ScopeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClientId", "ScopeId");

                    b.HasIndex("ScopeId");

                    b.ToTable("ClientScopes");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.Policy", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.PolicyScope", b =>
                {
                    b.Property<string>("PolicyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ScopeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PolicyId", "ScopeId");

                    b.HasIndex("ScopeId");

                    b.ToTable("PolicyScopes");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.Scope", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScopeProviderId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ScopeProviderId");

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.ScopeProvider", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScopeProviders");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.UserPolicy", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PolicyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PolicyScopePolicyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PolicyScopeScopeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Username", "PolicyId");

                    b.HasIndex("PolicyId");

                    b.HasIndex("PolicyScopePolicyId", "PolicyScopeScopeId");

                    b.ToTable("UserPolicies");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.Users", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.ClientScope", b =>
                {
                    b.HasOne("IdentityServer.Features.Domains.Client", "Client")
                        .WithMany("ClientScopes")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityServer.Features.Domains.Scope", "Scope")
                        .WithMany("ClientScopes")
                        .HasForeignKey("ScopeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.PolicyScope", b =>
                {
                    b.HasOne("IdentityServer.Features.Domains.Policy", "Policy")
                        .WithMany("PolicyScopes")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityServer.Features.Domains.Scope", "Scope")
                        .WithMany("PolicyScopes")
                        .HasForeignKey("ScopeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.Scope", b =>
                {
                    b.HasOne("IdentityServer.Features.Domains.ScopeProvider", "ScopeProvider")
                        .WithMany("Scopes")
                        .HasForeignKey("ScopeProviderId");
                });

            modelBuilder.Entity("IdentityServer.Features.Domains.UserPolicy", b =>
                {
                    b.HasOne("IdentityServer.Features.Domains.Policy", null)
                        .WithMany("UserPolicies")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityServer.Features.Domains.Users", "User")
                        .WithMany("UserPolicies")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityServer.Features.Domains.PolicyScope", "PolicyScope")
                        .WithMany()
                        .HasForeignKey("PolicyScopePolicyId", "PolicyScopeScopeId");
                });
#pragma warning restore 612, 618
        }
    }
}
