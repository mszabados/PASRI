﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PASRI.API.Persistence;

namespace PASRI.API.Migrations
{
    [DbContext(typeof(PasriDbContext))]
    [Migration("20180920151538_DropExistingPersonAndIdentificationTables")]
    partial class DropExistingPersonAndIdentificationTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceBloodType", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceTypeBlood");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceCountry", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceCountry");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceEthnicGroupDemographic", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceEthnicGroupDemographic");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceEyeColor", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceEyeColor");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceGenderDemographic", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(1)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceGenderDemographic");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceHairColor", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceHairColor");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceRaceDemographic", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(1)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceRaceDemographic");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceReligionDemographic", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceReligionDemographic");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceState", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceState");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceSuffixName", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(4)");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Code");

                    b.ToTable("ReferenceSuffixName");
                });
#pragma warning restore 612, 618
        }
    }
}
