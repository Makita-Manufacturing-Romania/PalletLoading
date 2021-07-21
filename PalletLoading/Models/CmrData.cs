using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class CmrData
    {
        public int Id { get; set; }
        public string ContainerName { get; set; }
        public string LicensePlate { get; set; }
        public string Driver { get; set; }
        public string SerialNo { get; set; }
        public string PackingList { get; set; }
        public int Weight { get; set; }
        public string NoOfTools { get; set; }
        public string NoOfSpr { get; set; }
    }
}
