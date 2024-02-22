using System.ComponentModel.DataAnnotations;

namespace PalletLoading.Models
{
    public class SecuringLoad
    {
        [Key]
        public int Id { get; set; }
        public int ContainerId { get; set; }
        public bool Chingi { get; set; }
        public bool Coltare { get; set; }
        public bool BareFixare { get; set; }
        public bool Absorgel { get; set; }
        public bool SaciProtectie { get; set; }
    }
}
