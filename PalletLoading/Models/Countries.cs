using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class Countries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public virtual ICollection<Container> Containers { get; set; }
        public int? TypeId { get; set; }
        public ClientType Type { get; set; }
    }
}
