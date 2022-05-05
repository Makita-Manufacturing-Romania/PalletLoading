using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class ContainerType
    {
        public int Id { get; set; }
        [Display(Name = "Type")]
        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal volume  { get; set; }
        public virtual ICollection<Container> Containers { get; set; }
    }
}
