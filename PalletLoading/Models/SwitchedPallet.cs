using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class SwitchedPallet
    {
        public int Id { get; set; }
        public string FirstPallet { get; set; }
        public string SecondPallet { get; set; }
        public int IdContainer { get; set; }
    }
}
