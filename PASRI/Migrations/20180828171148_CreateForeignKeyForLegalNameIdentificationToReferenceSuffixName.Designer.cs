﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PASRI.API.Persistence;

namespace PASRI.API.Migrations
{
    [DbContext(typeof(PasriDbContext))]
    [Migration("20180828171148_CreateForeignKeyForLegalNameIdentificationToReferenceSuffixName")]
    partial class CreateForeignKeyForLegalNameIdentificationToReferenceSuffixName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PASRI.Core.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("PASRI.Core.Domain.PersonIdentification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonIdentification");
                });

            modelBuilder.Entity("PASRI.Core.Domain.PersonLegalNameIdentification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EffectiveDate");

                    b.Property<string>("First")
                        .HasMaxLength(255);

                    b.Property<string>("Full")
                        .HasMaxLength(255);

                    b.Property<string>("Last")
                        .HasMaxLength(255);

                    b.Property<string>("Middle")
                        .HasMaxLength(255);

                    b.Property<int>("PersonNameIdentificationId");

                    b.Property<string>("ReferenceSuffixNameCode")
                        .HasMaxLength(4);

                    b.HasKey("Id");

                    b.HasIndex("PersonNameIdentificationId");

                    b.HasIndex("ReferenceSuffixNameCode")
                        .IsUnique()
                        .HasFilter("[ReferenceSuffixNameCode] IS NOT NULL");

                    b.ToTable("PersonLegalNameIdentification");
                });

            modelBuilder.Entity("PASRI.Core.Domain.PersonNameIdentification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoDServicePersonDocumentID");

                    b.Property<int>("PersonIdentificationId");

                    b.HasKey("Id");

                    b.HasIndex("PersonIdentificationId");

                    b.ToTable("PersonNameIdentification");
                });

            modelBuilder.Entity("PASRI.Core.Domain.ReferenceSuffixName", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(4);

                    b.Property<string>("DisplayText");

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Code");

                    b.ToTable("ReferenceSuffixName");
                });

            modelBuilder.Entity("PASRI.Core.Domain.PersonIdentification", b =>
                {
                    b.HasOne("PASRI.Core.Domain.Person", "Person")
                        .WithMany("PersonIdentifications")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PASRI.Core.Domain.PersonLegalNameIdentification", b =>
                {
                    b.HasOne("PASRI.Core.Domain.PersonNameIdentification", "PersonNameIdentification")
                        .WithMany("PersonLegalNameIdentifications")
                        .HasForeignKey("PersonNameIdentificationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PASRI.Core.Domain.ReferenceSuffixName", "ReferenceSuffixName")
                        .WithOne("PersonLegalNameIdentification")
                        .HasForeignKey("PASRI.Core.Domain.PersonLegalNameIdentification", "ReferenceSuffixNameCode")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("PASRI.Core.Domain.PersonNameIdentification", b =>
                {
                    b.HasOne("PASRI.Core.Domain.PersonIdentification", "PersonIdentification")
                        .WithMany("PersonNameIdentifications")
                        .HasForeignKey("PersonIdentificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
