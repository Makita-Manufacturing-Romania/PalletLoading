﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PalletLoading.Data;

namespace PalletLoading.Migrations
{
    [DbContext(typeof(PalletLoadingContext))]
    partial class PalletLoadingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoOfColumns")
                        .HasColumnType("int");

                    b.Property<int>("NoOfRows")
                        .HasColumnType("int");

                    b.Property<int?>("PalletId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContainerTypeId");

                    b.HasIndex("CountryId");

                    b.HasIndex("PalletId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("PalletLoading.Models.ContainerAT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContainerId")
                        .HasColumnType("int");

                    b.Property<string>("ContainerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("CountryId");

                    b.ToTable("ContainerATs");
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

            modelBuilder.Entity("PalletLoading.Models.Countries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("PalletLoading.Models.ImportData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("IPRVCC")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IPRVDD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IPRVMM")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IPRVYY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IPSEQ")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("consignee_code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("container_no")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal>("loading_date")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("loading_time")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("pallet_no")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("picking_qty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("salse_part")
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<decimal>("serial_from")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("serial_to")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.ToTable("ImportData");
                });

            modelBuilder.Entity("PalletLoading.Models.ImportDataHistory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("IPRVCC")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IPRVDD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IPRVMM")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IPRVYY")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IPSEQ")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("consignee_code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("container_no")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal>("loading_date")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("loading_time")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("pallet_no")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("picking_qty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("salse_part")
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<decimal>("serial_from")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("serial_to")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.ToTable("ImportDataHistory");
                });

            modelBuilder.Entity("PalletLoading.Models.ImportDataPalletsLP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerCode180P")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("CustomerCode250P")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("LOADED180")
                        .HasColumnType("int");

                    b.Property<int>("LOADED250")
                        .HasColumnType("int");

                    b.Property<int>("PICKED180")
                        .HasColumnType("int");

                    b.Property<int>("PICKED250")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ImportDataPalletsLP");
                });

            modelBuilder.Entity("PalletLoading.Models.Pallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Column")
                        .HasColumnType("int");

                    b.Property<int>("Container2Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNo")
                        .HasColumnType("int");

                    b.Property<int?>("PalletImportDataHistoryId")
                        .HasColumnType("int");

                    b.Property<int?>("PalletImportDataId")
                        .HasColumnType("int");

                    b.Property<int?>("PalletTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("Row")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PalletImportDataHistoryId");

                    b.HasIndex("PalletImportDataId");

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

            modelBuilder.Entity("PalletLoading.Models.SwitchedPallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstPallet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FirstPalletId")
                        .HasColumnType("int");

                    b.Property<int>("IdContainer")
                        .HasColumnType("int");

                    b.Property<string>("SecondPallet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecondPalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SwitchedPallets");
                });

            modelBuilder.Entity("PalletLoading.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRightId")
                        .HasColumnType("int");

                    b.Property<string>("Usermail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserRightId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PalletLoading.Models.UserRight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Right")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RightLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserRight");
                });

            modelBuilder.Entity("PalletLoading.Models.Container", b =>
                {
                    b.HasOne("PalletLoading.Models.ContainerType", "ContainerType")
                        .WithMany("Containers")
                        .HasForeignKey("ContainerTypeId");

                    b.HasOne("PalletLoading.Models.Countries", "Country")
                        .WithMany("Containers")
                        .HasForeignKey("CountryId");

                    b.HasOne("PalletLoading.Models.Pallet", "Pallet")
                        .WithMany("Containers")
                        .HasForeignKey("PalletId");

                    b.Navigation("ContainerType");

                    b.Navigation("Country");

                    b.Navigation("Pallet");
                });

            modelBuilder.Entity("PalletLoading.Models.ContainerAT", b =>
                {
                    b.HasOne("PalletLoading.Models.Container", "Container")
                        .WithMany()
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PalletLoading.Models.Countries", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Container");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("PalletLoading.Models.Pallet", b =>
                {
                    b.HasOne("PalletLoading.Models.ImportDataHistory", "PalletImportDataHistory")
                        .WithMany()
                        .HasForeignKey("PalletImportDataHistoryId");

                    b.HasOne("PalletLoading.Models.ImportData", "PalletImportData")
                        .WithMany()
                        .HasForeignKey("PalletImportDataId");

                    b.HasOne("PalletLoading.Models.PalletType", "PalletType")
                        .WithMany("Pallets")
                        .HasForeignKey("PalletTypeId");

                    b.Navigation("PalletImportData");

                    b.Navigation("PalletImportDataHistory");

                    b.Navigation("PalletType");
                });

            modelBuilder.Entity("PalletLoading.Models.User", b =>
                {
                    b.HasOne("PalletLoading.Models.UserRight", "UserRight")
                        .WithMany()
                        .HasForeignKey("UserRightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRight");
                });

            modelBuilder.Entity("PalletLoading.Models.ContainerType", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("PalletLoading.Models.Countries", b =>
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
