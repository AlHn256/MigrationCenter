﻿// <auto-generated />
using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiDemoMC.Migrations
{
    [DbContext(typeof(MC_ContextDb))]
    [Migration("20190925120818_AddForentDelIntParameters")]
    partial class AddForentDelIntParameters
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLibrary.ListsOfMigrantes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ListOfMigrantes");
                });

            modelBuilder.Entity("DataAccessLibrary.Migrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("PassNumber");

                    b.Property<int>("PassSerri");

                    b.Property<string>("Patronymic");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Migrant");
                });

            modelBuilder.Entity("DataAccessLibrary.MigrationCenteres", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Mail");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("URL");

                    b.HasKey("Id");

                    b.ToTable("MigrationCenteres");
                });

            modelBuilder.Entity("DataAccessLibrary.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "jurperson"
                        },
                        new
                        {
                            Id = 3,
                            Name = "agent"
                        });
                });

            modelBuilder.Entity("DataAccessLibrary.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bank_Kor_Sch");

                    b.Property<string>("Bank_Rasch_Sch");

                    b.Property<string>("Bik_Bank");

                    b.Property<string>("FIO");

                    b.Property<string>("GradationFIO");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Mail");

                    b.Property<string>("Main_Document");

                    b.Property<string>("Name");

                    b.Property<string>("Pass");

                    b.Property<string>("Phone");

                    b.Property<int>("RoleId");

                    b.Property<string>("Token");

                    b.Property<string>("Ur_Adress");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDelete = false,
                            Name = "admin",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("DataAccessLibrary.UserMigrant", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("MigrantId");

                    b.Property<byte>("Order");

                    b.HasKey("UserId", "MigrantId");

                    b.HasIndex("MigrantId");

                    b.ToTable("UserMigrant");
                });

            modelBuilder.Entity("DataAccessLibrary.UserMigrant", b =>
                {
                    b.HasOne("DataAccessLibrary.Migrant", "Migrant")
                        .WithMany()
                        .HasForeignKey("MigrantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccessLibrary.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
