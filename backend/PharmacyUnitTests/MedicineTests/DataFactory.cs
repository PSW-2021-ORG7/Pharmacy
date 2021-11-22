using backend.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyUnitTests.MedicineTests
{
    public class DataFactory
    {
        public List<Medicine> StubMedicineForSearchAndFilter { get; set; }

        public DataFactory()
        {
            GetStubMedicineDataForSearchAndFilter();
        }

        public void GetStubMedicineDataForSearchAndFilter()
        {
            StubMedicineForSearchAndFilter =  new List<Medicine>()
            {
                new Medicine { Name = "Brufen", Description = "Lek protiv bolova. Analgetik.", Manufacturer = "Galenika", DosageInMilligrams = 100, Ingredients = new List<Ingredient>{ new Ingredient { Name = "laktoza"}, new Ingredient { Name = "kkk"} }},
                new Medicine { Name = "Defrinol", Description = "Lek protiv bolova i prehlade.", Manufacturer = "Hemofarm", DosageInMilligrams = 150, Ingredients = new List<Ingredient>{ new Ingredient { Name = "grgbb"} }},
                new Medicine { Name = "Amoksicilin", Description = "Antibiotik.", Manufacturer = "Galenika", DosageInMilligrams = 200, Ingredients = new List<Ingredient>{ new Ingredient { Name = "eeeee"}, new Ingredient { Name = "kkk"} }},
                new Medicine { Name = "Nimulid", Description = "Lek protiv bolova u stomaku i glavi. Analgetik.", DosageInMilligrams = 325, Manufacturer = "MXY", Ingredients = new List<Ingredient>{ new Ingredient { Name = "vnidjv"} }},
                new Medicine { Name = "Fervex", Description = "Lek koji sprecava prehladu.", DosageInMilligrams = 450, Manufacturer = "Oasis", Ingredients = new List<Ingredient>{ new Ingredient { Name = "rrrr"} }},
                new Medicine { Name = "Ospamox", Description = "Jak antibiotik.", DosageInMilligrams = 650, Manufacturer = "MXY", Ingredients = new List<Ingredient>{ new Ingredient { Name = "vnidjv"}, new Ingredient { Name = "laktoza"} }}
            };
        }
    }
}
