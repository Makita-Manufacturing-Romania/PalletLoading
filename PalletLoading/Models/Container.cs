using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        //public string? operatorName { get; set; }
        //public string? forklifterName { get; set; }
        //public string? tlName { get; set; }
        //public string? svName { get; set; }
        public int? SecuringLoadId { get; set; }
        public int? FormDataId { get; set; }

        public string issuer_Name { get; set; }
        public string checkerTL_Name { get; set; }
        public string checkerSV_Name { get; set; }
        public string approval_Name { get; set; }
        [NotMapped]
        public bool dataCheck { get; set; }
        [NotMapped]
        public bool signatureCheck { get; set; }
        [NotMapped]
        public bool fileCheck { get; set; }

        public DateTime? issuer_Signature_Timestamp { get; set; }
        public DateTime? checkerTL_Signature_Timestamp { get; set; }
        public DateTime? checkerSV_Signature_Timestamp { get; set; }
        public DateTime? approval_Signature_Timestamp { get; set; }
    }
}