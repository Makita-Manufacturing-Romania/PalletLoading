﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PalletLoading.Data;

namespace PalletLoading.Migrations
{
    [DbContext(typeof(PalletLoadingContext))]
    [Migration("20210531110006_AddingPalletKey")]
    partial class AddingPalletKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PalletLoading.Models.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContainerTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PalletId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContainerTypeId");

                    b.HasIndex("PalletId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("PalletLoading.Models.ContainerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ContainerTypes");
                });

            modelBuilder.Entity("PalletLoading.Models.Pallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PalletTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PalletTypeId");

                    b.ToTable("Pallets");
                });

            modelBuilder.Entity("PalletLoading.Models.PalletType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PalletTypes");
                });

            modelBuilder.Entity("PalletLoading.Models.Container", b =>
                {
                    b.HasOne("PalletLoading.Models.ContainerType", "ContainerType")
                        .WithMany("Containers")
                        .HasForeignKey("ContainerTypeId");

                    b.HasOne("PalletLoading.Models.Pallet", "Pallet")
                        .WithMany("Containers")
                        .HasForeignKey("PalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContainerType");

                    b.Navigation("Pallet");
                });

            modelBuilder.Entity("PalletLoading.Models.Pallet", b =>
                {
                    b.HasOne("PalletLoading.Models.PalletType", "PalletType")
                        .WithMany("Pallets")
                        .HasForeignKey("PalletTypeId");

                    b.Navigation("PalletType");
                });

            modelBuilder.Entity("PalletLoading.Models.ContainerType", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("PalletLoading.Models.Pallet", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("PalletLoading.Models.PalletType", b =>
                {
                    b.Navigation("Pallets");
                });
#pragma warning restore 612, 618
        }
    }
}
