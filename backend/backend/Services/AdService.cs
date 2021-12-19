using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace backend.Services
{
    public class AdService
    {
        private IAdRepository adRepository;

        public AdService(IAdRepository adRepository)
        {
            this.adRepository = adRepository;
        }

        public Boolean Save(Ad ad)
        {
            if (ad.ValidDates())
                if (adRepository.Save(ad)) return true;
                else throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);
            return false;
        }

        public List<Ad> GetAll()
        {
            return adRepository.GetAll();
        }
    }
}
