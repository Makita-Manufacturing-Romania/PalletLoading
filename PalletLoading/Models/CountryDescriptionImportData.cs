using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Models
{
    public class CountryDescriptionImportData
    {
        [Key]
        [StringLength(3)]
        public string CCODE{ get; set; }
        [StringLength(30)]
        public string CNAME{ get; set; }
        [StringLength(30)]
        public string CADDR1{ get; set; }
        [StringLength(30)]
        public string CADDR2{ get; set; }
        [StringLength(30)]
        public string CADDR3{ get; set; }
        [StringLength(10)]
        public string CPSTCD{ get; set; }
        [StringLength(20)]
        public string CREPES{ get; set; }
        [StringLength(15)]
        public string CTEL{ get; set; }
        [StringLength(15)]
        public string CFAX{ get; set; }
        [StringLength(2)]
        public string CPAYT{ get; set; }
        public decimal CLEAD{ get; set; }
        [StringLength(17)]
        public string CVATNO{ get; set; }
        public decimal CPAYUS{ get; set; }
        [StringLength(1)]
        public string CDUTFR{ get; set; }
        [StringLength(30)]
        public string CNAME2{ get; set; }
        [StringLength(30)]
        public string CADR12{ get; set; }
        [StringLength(30)]
        public string CADR22{ get; set; }
        [StringLength(30)]
        public string CADR32{ get; set; }
        [StringLength(30)]
        public string CDLVN{ get; set; }
        [StringLength(30)]
        public string CDAD1{ get; set; }
        [StringLength(30)]
        public string CDAD2{ get; set; }
        [StringLength(30)]
        public string CDAD3{ get; set; }
        [StringLength(30)]
        public string CDLVN2{ get; set; }
        [StringLength(30)]
        public string CDAD12{ get; set; }
        [StringLength(30)]
        public string CDAD22{ get; set; }
        [StringLength(30)]
        public string CDAD32{ get; set; }
        [StringLength(1)]
        public string CBNKCD{ get; set; }
        [StringLength(1)]
        public string CCOOL{ get; set; }
        [StringLength(1)]
        public string CEUREG{ get; set; }
        [StringLength(20)]
        public string CCNAME{ get; set; }
        [StringLength(1)]
        public string CAWGT{ get; set; }
        [StringLength(1)]
        public string CPRLI{ get; set; }
        public int CCURC{ get; set; }
        [StringLength(2)]
        public string CTOD{ get; set; }
    }
}
