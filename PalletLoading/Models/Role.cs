using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PalletLoading.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int accessLevel { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
