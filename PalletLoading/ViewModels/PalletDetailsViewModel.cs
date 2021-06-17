using PalletLoading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.ViewModels
{
    public class PalletDetailsViewModel
    {
        public Pallet Pallet { get; set; }
        public PalletType PalletType { get; set; }
    }
}
