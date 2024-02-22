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

        [Display(Name="Container type")]
        public virtual ContainerType Type { get; set; }
        [Display(Name ="Type")]
        public int TypeId { get; set; }
        public virtual Countries Country { get; set; }
        [Display(Name="Country")]
        public int? CountryId { get; set; }
        public int NoOfRows { get; set; }
        public int NoOfColumns { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<ContainerAT> ContainerAT { get; set; }
        public virtual List<Pallet> Pallets { get; set; }

        public static implicit operator List<object>(Container v)
        {
            throw new NotImplementedException();
        }
        public int? CmrId { get; set; }

        public int? LoadingTypeId { get; set; }
        public int? FormTypeId { get; set; }
        public bool? securingLoadConfirm { get; set; }
        public bool? loadingTypeConfirm { get; set; }
        public bool? formDefinitionConfirm { get; set; }

        public string? operatorName { get; set; }
        public string? forklifterName { get; set; }
        public string? tlName { get; set; }
        public string? svName { get; set; }
    }
}