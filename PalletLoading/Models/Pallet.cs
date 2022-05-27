using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class Pallet
    {
        public int Id { get; set; }
        [Display(Name = "Pallet")]
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public virtual PalletType PalletType { get; set; }
        public int Container2Id { get; set; }
        public virtual Container Container2 { get; set; }

        public int OrderNo { get; set; }
        public int? Row  { get; set; }
        public int? Column { get; set; }
        public int? PalletImportDataId { get; set; }
        public virtual ImportData PalletImportData { get; set; }
        public int? PalletImportDataHistoryId { get; set; }
        public virtual ImportDataHistory PalletImportDataHistory { get; set; }
        public DateTime DataInsert { get; set; }
        public string CreatedBy { get; set; }
    }
}
