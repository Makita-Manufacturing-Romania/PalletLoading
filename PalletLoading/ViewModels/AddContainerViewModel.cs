using PalletLoading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.ViewModels
{
    public class AddContainerViewModel
    {
        public Container Container { get; set; }
        public List<Pallet> Pallets { get; set; }
        public Pallet Pallet { get; set; }
        public ContainerType Type { get; set; }
        public List<SwitchedPallet> SwitchedPallets { get; set; }
        public List<ContainerAT> ContainerAT { get; set; }
        public List<ImportDataPalletsLP> idplp { get; set; }
    }
}
