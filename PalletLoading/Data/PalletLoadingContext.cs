using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Models;

namespace PalletLoading.Data
{
    public class PalletLoadingContext : DbContext
    {
        public PalletLoadingContext(DbContextOptions<PalletLoadingContext> options)
            : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ImportData>().HasOne(c => c.PCPallet).WithOne(c => c.ImportDataId);
        //    //modelBuilder.Entity<Pallet>().HasMany(c => c.PCPallet).WithOne()
        //    //   .HasForeignKey(con => con.ImportDataId);
        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<PalletLoading.Models.Pallet> Pallets { get; set; }

        public DbSet<PalletLoading.Models.Container> Containers { get; set; }
        public DbSet<PalletLoading.Models.ContainerType> ContainerTypes { get; set; }
        public DbSet<PalletLoading.Models.PalletType> PalletTypes { get; set; }
        public DbSet<PalletLoading.Models.Countries> Countries { get; set; }
        public DbSet<PalletLoading.Models.ImportData> ImportData { get; set; }
        public DbSet<PalletLoading.Models.ImportDataHistory> ImportDataHistory { get; set; }
        public DbSet<PalletLoading.Models.SwitchedPallet> SwitchedPallets { get; set; }
        public DbSet<PalletLoading.Models.ContainerAT> ContainerATs { get; set; }
        public DbSet<PalletLoading.Models.ImportDataPalletsLP> ImportDataPalletsLP { get; set; }
        public DbSet<PalletLoading.Models.User> User { get; set; }
        public DbSet<PalletLoading.Models.UserRight> UserRight { get; set; }
        public DbSet<PalletLoading.Models.CountryDescriptionImportData> CountryDescriptionImportData { get; set; }
        public DbSet<PalletLoading.Models.CmrData> CmrDatas { get; set; }
        public DbSet<PalletLoading.Models.PartCenterPallets> PartCenterPallets { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<LoadingType> LoadingTypes { get; set; }
        public DbSet<SecuringLoad> SecuringLoads { get; set; }
        public DbSet<FormDefinition> FormDefinitions { get; set; }
        public DbSet<FormData> FormDatas { get; set; }
        public DbSet<UploadModel> UploadModelTabel { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<ContainerFillRule> ContainerFillRules {get; set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Countries>()
                .HasOne(u => u.Type)
                .WithMany(r => r.Countries)
                .HasForeignKey(u => u.TypeId);
        }
    }
}
