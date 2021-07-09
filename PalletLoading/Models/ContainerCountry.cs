using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class ContainerCountry
    {
        public List<SelectListItem> Countries { get; set; }
        public int[] CountryIds { get; set; }
    }
}
