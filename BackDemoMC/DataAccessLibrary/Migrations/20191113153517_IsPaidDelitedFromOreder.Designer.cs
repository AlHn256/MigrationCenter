﻿// <auto-generated />
using System;
using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiDemoMC.Migrations
{
    [DbContext(typeof(MC_ContextDb))]
    [Migration("20191113153517_IsPaidDelitedFromOreder")]
    partial class IsPaidDelitedFromOreder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLibrary.Migrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelite");

                    b.Property<string>("Name");

                    b.Property<string>("PassNumber");

                    b.Property<string>("PassSerri");

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

            modelBuilder.Entity("DataAccessLibrary.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.Property<int?>("UrAgentId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UrAgentId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DataAccessLibrary.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MigrantId");

                    b.Property<int>("OrderId");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("MigrantId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ServiceId");

                    b.ToTable("OrderItem");
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
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Jurperson"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Agent"
                        });
                });

            modelBuilder.Entity("DataAccessLibrary.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cost");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Service");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 400,
                            IsDelete = false,
                            Name = "Оплата НДФЛ"
                        },
                        new
                        {
                            Id = 2,
                            Cost = 600,
                            IsDelete = false,
                            Name = "Мед. страхование"
                        },
                        new
                        {
                            Id = 3,
                            Cost = 1100,
                            IsDelete = false,
                            Name = "Оформление ВНЖ"
                        },
                        new
                        {
                            Id = 4,
                            Cost = 1000,
                            IsDelete = false,
                            Name = "Мед.Освидетельствование"
                        },
                        new
                        {
                            Id = 5,
                            Cost = 800,
                            IsDelete = false,
                            Name = "Экзамен русского языка"
                        },
                        new
                        {
                            Id = 6,
                            Cost = 600,
                            IsDelete = false,
                            Name = "Базовый экзамен русского языка"
                        },
                        new
                        {
                            Id = 7,
                            Cost = 600,
                            IsDelete = false,
                            Name = "Заключение труд. договора"
                        });
                });

            modelBuilder.Entity("DataAccessLibrary.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Mail");

                    b.Property<string>("Name");

                    b.Property<string>("Pass");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("DataAccessLibrary.UserMigrant", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("MigrantId");

                    b.HasKey("UserId", "MigrantId");

                    b.HasIndex("MigrantId");

                    b.ToTable("UserMigrant");
                });

            modelBuilder.Entity("DataAccessLibrary.Admin", b =>
                {
                    b.HasBaseType("DataAccessLibrary.User");

                    b.ToTable("Admin");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDelete = false,
                            Name = "admin",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("DataAccessLibrary.Agent", b =>
                {
                    b.HasBaseType("DataAccessLibrary.User");

                    b.Property<string>("Phone");

                    b.ToTable("Agent");

                    b.HasDiscriminator().HasValue("Agent");
                });

            modelBuilder.Entity("DataAccessLibrary.Buhgalter", b =>
                {
                    b.HasBaseType("DataAccessLibrary.User");

                    b.Property<DateTime>("EndDateLicens");

                    b.Property<string>("LicensNumber");

                    b.Property<int>("Qualification");

                    b.Property<DateTime>("StartDateLicens");

                    b.ToTable("Buhgalter");

                    b.HasDiscriminator().HasValue("Buhgalter");
                });

            modelBuilder.Entity("DataAccessLibrary.UrAgent", b =>
                {
                    b.HasBaseType("DataAccessLibrary.User");

                    b.Property<string>("Bank_Kor_Sch");

                    b.Property<string>("Bank_Rasch_Sch");

                    b.Property<string>("Bik_Bank");

                    b.Property<string>("FIO");

                    b.Property<string>("GradationFIO");

                    b.Property<string>("INN");

                    b.Property<string>("KPP");

                    b.Property<string>("Kor_Sch");

                    b.Property<string>("Main_Document");

                    b.Property<string>("Phone")
                        .HasColumnName("UrAgent_Phone");

                    b.Property<string>("Rasch_Sch");

                    b.Property<string>("Ur_Adress");

                    b.ToTable("UrAgent");

                    b.HasDiscriminator().HasValue("UrAgent");
                });

            modelBuilder.Entity("DataAccessLibrary.Order", b =>
                {
                    b.HasOne("DataAccessLibrary.UrAgent")
                        .WithMany("Orders")
                        .HasForeignKey("UrAgentId");

                    b.HasOne("DataAccessLibrary.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLibrary.OrderItem", b =>
                {
                    b.HasOne("DataAccessLibrary.Migrant", "Migrant")
                        .WithMany()
                        .HasForeignKey("MigrantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccessLibrary.Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccessLibrary.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLibrary.UserMigrant", b =>
                {
                    b.HasOne("DataAccessLibrary.Migrant", "Migrant")
                        .WithMany("UserMigrantes")
                        .HasForeignKey("MigrantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccessLibrary.UrAgent", "UrAgent")
                        .WithMany("UserMigrantes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
