using System.ComponentModel.DataAnnotations;

namespace PalletLoading.Models
{
    public class ContainerFillRule
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
