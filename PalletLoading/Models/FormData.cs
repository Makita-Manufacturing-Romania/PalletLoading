using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PalletLoading.Models
{
    public class FormData
    {
        [Key]
        public int id { get; set; }
        public string rampaNr { get; set; } = null;
        public DateTime? oraIntrare { get; set; }
        public DateTime? start { get; set; }
        public DateTime? stop { get; set; }
        public DateTime? oraIesire { get; set; }
        public string nrCamion { get; set; } = null;
        public string nrContainer { get; set; } = null;
        public int? nrPaletiPickPeTemp { get; set; }
        public string nrPalScanner { get; set; } = null;
        public string nrAviz { get; set; } = null;
        public string nrSigiliu { get; set; } = null;
        public string numeStivuitorist { get; set; } = null;
        [NotMapped]
        public int ContainerId { get; set; }
    }
}
