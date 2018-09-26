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
    [Migration("20180926110955_UpdateStateTableToNotHaveREPrefixAfterItJoinedRESchema")]
    partial class UpdateStateTableToNotHaveREPrefixAfterItJoinedRESchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PASRI.API.Core.Domain.Birth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("birth_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("birth_city")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("CountryId")
                        .HasColumnName("country_id")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Date")
                        .HasColumnName("birth_date")
                        .HasColumnType("date");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.Property<int>("PersonId")
                        .HasColumnName("person_id");

                    b.Property<int>("StateProvinceId")
                        .HasColumnName("state_province_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.HasIndex("StateProvinceId");

                    b.ToTable("BIRTH","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("person_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnName("effect_date")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MiddleName")
                        .HasColumnName("middle_name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.Property<int?>("SuffixId")
                        .HasColumnName("suffix_id");

                    b.HasKey("Id");

                    b.HasIndex("SuffixId");

                    b.ToTable("PERSON","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceBloodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("blood_type_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(3)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(11)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("RE_BLOOD_TYPE","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceCountry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("country_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(44)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("COUNTRY","RE");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceEthnicGroupDemographic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ethnic_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(28)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("RE_ETHNIC","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceEyeColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("eye_color_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("RE_EYE_COLOR","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceGenderDemographic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("gender_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(1)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(7)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("RE_GENDER","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceHairColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("hair_color_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("RE_HAIR_COLOR","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceNameSuffix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("suffix_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("RE_SUFFIX","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceRaceDemographic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("race_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(1)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(237)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("RE_RACE","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceReligionDemographic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("religion_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(253)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("RE_RELIGION","PERSON");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.ReferenceStateProvince", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("state_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("char(2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("varchar(35)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnName("modified_by")
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("STATE","RE");
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.Birth", b =>
                {
                    b.HasOne("PASRI.API.Core.Domain.ReferenceCountry", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PASRI.API.Core.Domain.Person", "Person")
                        .WithOne("Birth")
                        .HasForeignKey("PASRI.API.Core.Domain.Birth", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PASRI.API.Core.Domain.ReferenceStateProvince", "StateProvince")
                        .WithMany()
                        .HasForeignKey("StateProvinceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PASRI.API.Core.Domain.Person", b =>
                {
                    b.HasOne("PASRI.API.Core.Domain.ReferenceNameSuffix", "Suffix")
                        .WithMany()
                        .HasForeignKey("SuffixId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
