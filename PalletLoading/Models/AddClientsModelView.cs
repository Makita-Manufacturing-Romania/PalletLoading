using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class AddClientsModelView
    {
        public List<ContainerAT> ContainerATs { get; set; }
        public int idContainer { get; set; }
    }
}
