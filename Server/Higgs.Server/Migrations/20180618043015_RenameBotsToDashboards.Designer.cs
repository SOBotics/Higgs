﻿// <auto-generated />
using Higgs.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Higgs.Server.Migrations
{
    [DbContext(typeof(HiggsDbContext))]
    [Migration("20180618043015_RenameBotsToDashboards")]
    partial class RenameBotsToDashboards
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Higgs.Server.Data.Models.DbConflictException", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DashboardId");

                    b.Property<bool>("IsConflict");

                    b.Property<int?>("ReportId");

                    b.Property<int?>("RequiredFeedback");

                    b.Property<bool>("RequiresAdmin");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.HasIndex("ReportId");

                    b.ToTable("ConflictExceptions");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbConflictExceptionFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConflictExceptionId");

                    b.Property<int>("FeedbackId");

                    b.HasKey("Id");

                    b.HasIndex("ConflictExceptionId");

                    b.HasIndex("FeedbackId");

                    b.ToTable("ConflictExceptionFeedbacks");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbDashboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BotName");

                    b.Property<string>("DashboardName");

                    b.Property<string>("Description");

                    b.Property<string>("FavIcon");

                    b.Property<string>("Homepage");

                    b.Property<string>("LogoUrl");

                    b.Property<int>("OwnerAccountId");

                    b.Property<int>("RequiredFeedback");

                    b.Property<int>("RequiredFeedbackConflicted");

                    b.Property<string>("Secret");

                    b.Property<string>("TabTitle");

                    b.HasKey("Id");

                    b.HasIndex("OwnerAccountId");

                    b.ToTable("Bots");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbDashboardScope", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DashboardId");

                    b.Property<string>("ScopeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.HasIndex("ScopeName");

                    b.ToTable("BotScopes");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Colour");

                    b.Property<int>("DashboardId");

                    b.Property<string>("Icon");

                    b.Property<bool>("IsActionable");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<byte[]>("Contents");

                    b.Property<string>("FileName");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BotId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BotId");

                    b.ToTable("Reasons");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorName");

                    b.Property<int?>("AuthorReputation");

                    b.Property<bool>("Conflicted");

                    b.Property<DateTime?>("ContentCreationDate");

                    b.Property<int?>("ContentId");

                    b.Property<string>("ContentSite");

                    b.Property<string>("ContentType");

                    b.Property<string>("ContentUrl");

                    b.Property<int>("DashboardId");

                    b.Property<DateTime?>("DetectedDate");

                    b.Property<double?>("DetectionScore");

                    b.Property<int>("RequiredFeedback");

                    b.Property<int>("RequiredFeedbackConflicted");

                    b.Property<bool>("RequiresReview");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReportAllowedFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FeedbackId");

                    b.Property<int>("ReportId");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportAllowedFeedbacks");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReportAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ReportId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportAttributes");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReportFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FeedbackId");

                    b.Property<int?>("InvalidatedByUserId");

                    b.Property<DateTime?>("InvalidatedDate");

                    b.Property<string>("InvalidationReason");

                    b.Property<int>("ReportId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("InvalidatedByUserId");

                    b.HasIndex("ReportId");

                    b.HasIndex("UserId");

                    b.ToTable("ReportFeedbacks");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReportReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Confidence");

                    b.Property<int>("ReasonId");

                    b.Property<int>("ReportId");

                    b.Property<bool>("Tripped");

                    b.HasKey("Id");

                    b.HasIndex("ReasonId");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportReasons");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbScope", b =>
                {
                    b.Property<string>("Name");

                    b.Property<string>("Description");

                    b.HasKey("Name");

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbUser", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<string>("Name");

                    b.HasKey("AccountId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbUserScope", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ScopeName")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ScopeName");

                    b.HasIndex("UserId");

                    b.ToTable("UserScopes");
                });

            modelBuilder.Entity("Higgs.Server.Models.Requests.Admin.DbContentFragment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<int>("ReportId");

                    b.Property<string>("RequiredScope");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ContentFragments");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbConflictException", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbDashboard", "Dashboard")
                        .WithMany("ConflictExceptions")
                        .HasForeignKey("DashboardId");

                    b.HasOne("Higgs.Server.Data.Models.DbReport", "Report")
                        .WithMany("ConflictExceptions")
                        .HasForeignKey("ReportId");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbConflictExceptionFeedback", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbConflictException", "ConflictException")
                        .WithMany("ConflictExceptionFeedbacks")
                        .HasForeignKey("ConflictExceptionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbFeedback", "Feedback")
                        .WithMany()
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbDashboard", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbUser", "OwnerAccount")
                        .WithMany("OwnedBots")
                        .HasForeignKey("OwnerAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbDashboardScope", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbDashboard", "Dashboard")
                        .WithMany("Scopes")
                        .HasForeignKey("DashboardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbScope", "Scope")
                        .WithMany()
                        .HasForeignKey("ScopeName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbFeedback", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbDashboard", "Dashboard")
                        .WithMany("Feedbacks")
                        .HasForeignKey("DashboardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReason", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbDashboard", "Bot")
                        .WithMany()
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReport", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbDashboard", "Dashboard")
                        .WithMany("Reports")
                        .HasForeignKey("DashboardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReportAllowedFeedback", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbFeedback", "Feedback")
                        .WithMany()
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbReport", "Report")
                        .WithMany("AllowedFeedback")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReportAttribute", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbReport", "Report")
                        .WithMany("Attributes")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReportFeedback", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbFeedback", "Feedback")
                        .WithMany("ReportFeedbacks")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbUser", "InvalidatedBy")
                        .WithMany("ReportInvalidations")
                        .HasForeignKey("InvalidatedByUserId");

                    b.HasOne("Higgs.Server.Data.Models.DbReport", "Report")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbUser", "User")
                        .WithMany("ReportFeedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReportReason", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbReason", "Reason")
                        .WithMany("ReportReasons")
                        .HasForeignKey("ReasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbReport", "Report")
                        .WithMany("Reasons")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbUserScope", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbScope", "Scope")
                        .WithMany()
                        .HasForeignKey("ScopeName")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbUser", "User")
                        .WithMany("UserScopes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Models.Requests.Admin.DbContentFragment", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbReport", "Report")
                        .WithMany("ContentFragments")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
