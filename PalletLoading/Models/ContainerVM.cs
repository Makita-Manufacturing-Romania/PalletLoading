using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class ContainerVM
    {
        public int Id { get; set; }
        public Container Container { get; set; }
        public ContainerType ContainerType { get; set; }
        public Countries Countries { get; set; }
    }
}
