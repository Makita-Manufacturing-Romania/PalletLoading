using System;
using System.ComponentModel.DataAnnotations;

namespace PalletLoading.Models
{
    public class PartCenterPallets
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Destination { get; set; }
        [Required]
        public decimal Pallet_number { get; set; }
        [Required]
        public decimal Length{ get; set; }
        [Required]
        public decimal Width { get; set; }
        [Required]
        public decimal Height { get; set; }
        [Required]
        public decimal Mass { get; set; }
        [Required]
        public decimal Shift { get; set; }
        public bool Status { get; set; }
        public decimal Volume { get; set; }
        public DateTime InputTimestamp{ get; set; }
        public int?  ImportDataId { get; set; }
        public virtual ImportData  ImportData { get; set; }
        public int? ImportDataHistoryId { get; set; }
        public virtual ImportDataHistory ImportDataHistory { get; set; }
    }
}
