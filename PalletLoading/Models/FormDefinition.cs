using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PalletLoading.Models
{
    public class FormDefinition
    {
        [Key]
        public int Id { get; set; }
        public string PDF_Name { get; set; }
        public string DocRefNumber { get; set; }
        public string Department { get; set; }
        [NotMapped]
        public int ContainerId { get; set; }
        [NotMapped]
        public string containersList { get; set; }
    }
}
