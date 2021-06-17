using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name="Pallet name")]
        public virtual Pallet Pallet { get; set; }
        public int? PalletId { get; set; }
        [Display(Name="Container type")]
        public virtual ContainerType ContainerType { get; set; }
        public int? TypeId { get; set; }
        public virtual Countries Country { get; set; }
        public int? CountryId { get; set; }
        public int NoOfRows { get; set; }
        public int NoOfColumns { get; set; }
    }
}