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
        public string Weight { get; set; }
        public string NoOfTools { get; set; }
        public string NoOfSpr { get; set; }
        public string Adress { get; set; }
    }

    //cadr12, cadr22, cadr32 - adresa destinatar
    //ipplno - numar factura  
}
