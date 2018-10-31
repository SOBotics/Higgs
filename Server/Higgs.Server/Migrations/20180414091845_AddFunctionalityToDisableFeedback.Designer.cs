﻿// <auto-generated />
using Higgs.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace Higgs.Server.Migrations
{
    [DbContext(typeof(HiggsDbContext))]
    [Migration("20180414091845_AddFunctionalityToDisableFeedback")]
    partial class AddFunctionalityToDisableFeedback
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Higgs.Server.Data.Models.DbBot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DashboardName");

                    b.Property<string>("Description");

                    b.Property<string>("FavIcon");

                    b.Property<string>("Homepage");

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Secret");

                    b.Property<string>("TabTitle");

                    b.HasKey("Id");

                    b.ToTable("Bots");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbBotScope", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BotId");

                    b.Property<string>("ScopeName");

                    b.HasKey("Id");

                    b.HasIndex("BotId");

                    b.HasIndex("ScopeName");

                    b.ToTable("BotScopes");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BotId");

                    b.Property<string>("Colour");

                    b.Property<string>("Icon");

                    b.Property<bool>("IsActionable");

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BotId");

                    b.ToTable("Feedbacks");
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

                    b.Property<int>("BotId");

                    b.Property<DateTime?>("ContentCreationDate");

                    b.Property<int?>("ContentId");

                    b.Property<string>("ContentSite");

                    b.Property<string>("ContentType");

                    b.Property<string>("ContentUrl");

                    b.Property<DateTime?>("DetectedDate");

                    b.Property<double?>("DetectionScore");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BotId");

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

                    b.Property<int>("ReportId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

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

                    b.Property<string>("ScopeName");

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

            modelBuilder.Entity("Higgs.Server.Data.Models.DbBotScope", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbBot", "Bot")
                        .WithMany("BotScopes")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbScope", "Scope")
                        .WithMany()
                        .HasForeignKey("ScopeName");
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbFeedback", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbBot", "Bot")
                        .WithMany("Feedbacks")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReason", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbBot", "Bot")
                        .WithMany()
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Higgs.Server.Data.Models.DbReport", b =>
                {
                    b.HasOne("Higgs.Server.Data.Models.DbBot", "Bot")
                        .WithMany()
                        .HasForeignKey("BotId")
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
                        .WithMany()
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbReport", "Report")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Higgs.Server.Data.Models.DbUser", "User")
                        .WithMany()
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
                        .HasForeignKey("ScopeName");

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
