using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class Pallet
    {
        public int Id { get; set; }
        [Display(Name = "Pallet")]
        public string Name { get; set; }
        public virtual ICollection<Container> Containers { get; set; }
        public int? TypeId { get; set; }
        public virtual PalletType PalletType { get; set; }
        public int Container2Id { get; set; }
        public int OrderNo { get; set; }
        public int? Row  { get; set; }
        public int? Column { get; set; }

        //Campuri utile:
        /*
            -sales part
            -serial no
            -qty
            -pallet no
            -weight
            -loading date
            -loading time

            *consignee code (tara)
            *conainer no
         */
    }
}
