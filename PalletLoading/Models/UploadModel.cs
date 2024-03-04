using System.ComponentModel.DataAnnotations;

namespace PalletLoading.Models
{
    public class UploadModel
    {
        [Key]
        public int id { get; set; }
        public int ContainerId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
    }
}
