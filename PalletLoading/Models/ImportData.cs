using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class ImportData
    {
        [Key]
        public int id { get; set; }
        [StringLength(25)]
        public string container_no { get; set; }
        [StringLength(3)]
        public string consignee_code { get; set; }
        [StringLength(13)]
        public string salse_part { get; set; }
        public decimal serial_from { get; set; }
        public decimal serial_to { get; set; }
        public decimal picking_qty { get; set; }
        public decimal pallet_no { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.###}")]
        public decimal weight  { get; set; }
        public decimal loading_date { get; set; }
        public decimal loading_time { get; set; }
        public decimal IPRVCC{ get; set; }
        public decimal IPRVYY{ get; set; }
        public decimal IPRVMM{ get; set; }
        public decimal IPRVDD{ get; set; }
        public decimal IPSEQ { get; set; }
        [StringLength(255)]
        public string IPPLNO { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal volume { get; set; }
        //public virtual Pallet? Pallet { get; set; }
    }
}
