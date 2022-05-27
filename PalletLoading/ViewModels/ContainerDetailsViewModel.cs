using PalletLoading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.ViewModels
{
    public class ContainerDetailsViewModel
    {
        public Container Container { get; set; }
        public Countries Country { get; set; }
        public ContainerType Type { get; set; }
        public List<Pallet> Pallets { get; set; }
        public List<ModelImportDataPccPallets> ModelImportDataPccPallets { get; set; }
        public Pallet Pallet { get; set; }
    }
}
