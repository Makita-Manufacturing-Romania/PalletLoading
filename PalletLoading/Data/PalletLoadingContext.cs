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
        public PalletLoadingContext (DbContextOptions<PalletLoadingContext> options)
            : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder
        //        .Entity<ImportData>(
        //            eb =>
        //            {
        //                eb.HasNoKey();
        //            });
        //}
        public DbSet<PalletLoading.Models.Pallet> Pallets { get; set; }

        public DbSet<PalletLoading.Models.Container> Containers { get; set; }
        public DbSet<PalletLoading.Models.ContainerType> ContainerTypes { get; set; }
        public DbSet<PalletLoading.Models.PalletType> PalletTypes { get; set; }
        public DbSet<PalletLoading.Models.Countries> Countries { get; set; }
        public DbSet<PalletLoading.Models.ImportData> ImportData { get; set; }
        public DbSet<PalletLoading.Models.SwitchedPallet> SwitchedPallets { get; set; }
    }
}
