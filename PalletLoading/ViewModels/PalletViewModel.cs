using PalletLoading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.ViewModels
{
    public class PalletViewModel
    {
        public List<Pallet> Pallet { get; set; }
        public List<PalletType> PalletType { get; set; }
    }
}
