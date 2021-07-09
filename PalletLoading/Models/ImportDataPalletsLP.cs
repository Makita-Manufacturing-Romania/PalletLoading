using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class ImportDataPalletsLP
    {
        [Key]
        public int Id { get; set; }
        [StringLength(25)]
        public string CustomerCode250P { get; set; }
        [StringLength(25)]
        public string CustomerCode180P { get; set; }
        public int PICKED250 { get; set; }
        public int PICKED180 { get; set; }
        public int LOADED250 { get; set; }
        public int LOADED180 { get; set; }
    }
}
