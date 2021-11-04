using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class AllergenService
    {
        IAllergenRepository allergenRepository;

        public AllergenService(IAllergenRepository allergenRepository) => this.allergenRepository = allergenRepository;

        public List<Allergen> GetAll() => allergenRepository.GetAll();

        public void Save(Allergen allergen) => allergenRepository.Save(allergen);
    }
}
