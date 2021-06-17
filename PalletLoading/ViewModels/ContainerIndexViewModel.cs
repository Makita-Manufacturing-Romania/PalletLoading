using PalletLoading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.ViewModels
{
    public class ContainerIndexViewModel
    {
        public List<Container> Container { get; set; }
        public List<ContainerType> ContainerType { get; set; }
        public List<Countries> Countries { get; set; }
    }
}
