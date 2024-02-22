using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PalletLoading.Models
{
    public class LoadingType
    {
        [Key]
        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}
