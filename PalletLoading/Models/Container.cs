using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class Container
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Client name")]
        public string Name { get; set; }
        [Display(Name="Pallet name")]
        public virtual Pallet Pallet { get; set; }
        public int? PalletId { get; set; }
        [Display(Name="Container type")]
        public virtual ContainerType ContainerType { get; set; }
        [Display(Name ="Type")]
        public int? TypeId { get; set; }
        public virtual Countries Country { get; set; }
        [Display(Name="Country")]
        public int? CountryId { get; set; }
        public int NoOfRows { get; set; }
        public int NoOfColumns { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<ContainerAT> ContainerAT { get; set; }

        public static implicit operator List<object>(Container v)
        {
            throw new NotImplementedException();
        }
        public int? CmrId { get; set; }
    }
}