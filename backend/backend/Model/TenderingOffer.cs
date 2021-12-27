using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class TenderingOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenderingOfferId { get; set; }
        public List<TenderingOfferItem> tenderingOfferItems { get; set; }

        public TenderingOffer() 
        {
            this.tenderingOfferItems = new List<TenderingOfferItem>();
        }

        public double GetPriceForAllAvailable()
        {
            double PriceForAllAvailable = 0;
            foreach(TenderingOfferItem ti in tenderingOfferItems)
            {
                PriceForAllAvailable += ti.GetPriceForAllAvailable();
            }
            return PriceForAllAvailable;

        }
        public double GetPriceForAllRequired()
        {
            double PriceForAllRequired = 0;
            foreach(TenderingOfferItem ti in tenderingOfferItems)
            {
                PriceForAllRequired += ti.GetPriceForAllRequired();
            }
            return PriceForAllRequired;
        }
        public int GetTotalMissing()
        {
            int totalMissing =0;
            foreach(TenderingOfferItem ti in tenderingOfferItems)
            {
                totalMissing += ti.MissingQuantity;
            }
            return totalMissing;
        }

    }
}
