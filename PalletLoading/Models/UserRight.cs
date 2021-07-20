using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class UserRight
    {
        [Key]
        public int Id{ get; set; }
        public string Right{ get; set; }
        public int RightLevel{ get; set; }
    }
}
