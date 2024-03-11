using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PalletLoading.Models
{
    public class ClientType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Countries> Countries { get; set; }
    }
}
