﻿// <auto-generated />
using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DbModels.Migrations
{
    [DbContext(typeof(cap01devContext))]
    [Migration("20180813192129_ChangeParticipantModel")]
    partial class ChangeParticipantModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Backend.Models.Admins", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Comment")
                        .HasColumnName("comment")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("creationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("character varying(32)");

                    b.Property<string>("FullName")
                        .HasColumnName("fullName")
                        .HasColumnType("character varying(32)")
                        .HasMaxLength(32);

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("isActive")
                        .HasDefaultValueSql("true");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnName("passwordHash")
                        .HasColumnType("character varying(60)")
                        .HasMaxLength(60);

                    b.Property<DateTime>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("updateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("Admins_email_key");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("DbModels.Models.Participant", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("change_dt");

                    b.Property<bool>("is_active");

                    b.Property<string>("name");

                    b.Property<string>("photo_id");

                    b.Property<long>("school_id");

                    b.Property<string>("second_name");

                    b.Property<string>("surname");

                    b.Property<int>("weight");

                    b.Property<int>("years_old");

                    b.HasKey("ID");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("DbModels.Models.Photo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("change_dt");

                    b.Property<string>("path");

                    b.HasKey("ID");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DbModels.Models.School", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address");

                    b.Property<DateTime>("change_dt");

                    b.Property<string>("city");

                    b.Property<string>("description");

                    b.Property<string>("email");

                    b.Property<bool>("is_active");

                    b.Property<string>("name");

                    b.HasKey("ID");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("DbModels.Tournament", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("address");

                    b.Property<DateTime>("change_dt");

                    b.Property<DateTime>("event_date_time");

                    b.Property<bool>("is_active");

                    b.Property<long>("max_years_old");

                    b.Property<long>("min_years_old");

                    b.Property<string>("name");

                    b.Property<long>("start_weight");

                    b.Property<long>("weight_step");

                    b.Property<long>("years_step");

                    b.HasKey("ID");

                    b.ToTable("Tournaments");
                });
#pragma warning restore 612, 618
        }
    }
}
