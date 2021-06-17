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

        public DbSet<PalletLoading.Models.Pallet> Pallets { get; set; }

        public DbSet<PalletLoading.Models.Container> Containers { get; set; }
        public DbSet<PalletLoading.Models.ContainerType> ContainerTypes { get; set; }
        public DbSet<PalletLoading.Models.PalletType> PalletTypes { get; set; }
        public DbSet<PalletLoading.Models.Countries> Countries { get; set; }
    }
}
