using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class ContainerAT
    {
        public int Id { get; set; }
        public virtual Container Container { get; set; }
        public int ContainerId { get; set; }
        public virtual Countries Country  { get; set; }
        public int CountryId { get; set; }
        public string ContainerName{ get; set; }
    }
}
