using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Usermail { get; set; }
        public int UserRightId { get; set; }
        public virtual UserRight UserRight { get; set; }

        public int? RoleId {  get; set; }
        public Role Role { get; set; }

    }
}
