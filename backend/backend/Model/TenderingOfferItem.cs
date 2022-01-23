using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class TenderingOfferItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenderingOfferItemId{ get; set; }
        public string MedicineName { get; set; }
        public int MedicineDosage { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        public int MissingQuantity { get; set; }
        public double PriceForSingleEntity { get; set; }
        public TenderingOfferItem() { }
        public double GetPriceForAllAvailable()
        {
            return AvailableQuantity * PriceForSingleEntity;
        }
        public double GetPriceForAllRequired()
        {
            return (AvailableQuantity + MissingQuantity) * PriceForSingleEntity;
        }
    }
}
