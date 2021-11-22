using backend.DTO;
using backend.Model;
using backend.Model.Enum;
using backend.Repositories.Interfaces;
using backend.Services;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PharmacyUnitTests.MedicineTests
{
    public class SearchAndFilterTests
    {
        private static DataFactory _dataFactory = new DataFactory();

        [Theory]
        [MemberData(nameof(SearchData))]
        public void Search_medicine_test(MedicineSearchParams searchParams, List<Medicine> expectedSearchResult)
        {
            MedicineService medicineService = SetUpMedicineServiceWithStubRepository();

            List<Medicine> searchResults = medicineService.MedicineSearchResults(searchParams);

            Assert.Equal(expectedSearchResult, searchResults);
        }

        [Theory]
        [MemberData(nameof(FilterData))]
        public void Filter_medicine_test(MedicineDosageFilter option, List<Medicine> expectedFilterResult)
        {
            MedicineService medicineService = SetUpMedicineServiceWithStubRepository();

            List<Medicine> filterResult = medicineService.MedicineFilterDosageResults(option);

            Assert.Equal(expectedFilterResult, filterResult);
        }

        private MedicineService SetUpMedicineServiceWithStubRepository()
        {
            var stubMedicineRepository = new Mock<IMedicineRepository>();
            var stubMedicineInventoryRepository = new Mock<IMedicineInventoryRepository>();
            stubMedicineRepository.Setup(m => m.GetAll()).Returns(_dataFactory.StubMedicineForSearchAndFilter);

            return new MedicineService(stubMedicineRepository.Object, stubMedicineInventoryRepository.Object);
        }

        public static IEnumerable<object[]> SearchData()
        {
            return new List<object[]>()
            {
                new object[] { new MedicineSearchParams { Name = "Defrinol", Description = "", Manufacturer = "", Ingredient = ""}, new List<Medicine> { _dataFactory.StubMedicineForSearchAndFilter[1] } },
                new object[] { new MedicineSearchParams { Name = "", Description = "", Manufacturer = "", Ingredient = "k" }, new List<Medicine> { _dataFactory.StubMedicineForSearchAndFilter[0], _dataFactory.StubMedicineForSearchAndFilter[2], _dataFactory.StubMedicineForSearchAndFilter[5] } },
                new object[] { new MedicineSearchParams { Name = "", Description = "tik", Manufacturer = "galenika", Ingredient = "" }, new List<Medicine> { _dataFactory.StubMedicineForSearchAndFilter[0], _dataFactory.StubMedicineForSearchAndFilter[2] } },
                new object[] { new MedicineSearchParams { Name = "", Description = "", Manufacturer = "", Ingredient = "" }, new List<Medicine> { _dataFactory.StubMedicineForSearchAndFilter[0], _dataFactory.StubMedicineForSearchAndFilter[1], _dataFactory.StubMedicineForSearchAndFilter[2], _dataFactory.StubMedicineForSearchAndFilter[3], _dataFactory.StubMedicineForSearchAndFilter[4], _dataFactory.StubMedicineForSearchAndFilter[5] } },
                new object[] { new MedicineSearchParams { Name = "fgg", Description = "", Manufacturer = "", Ingredient = ""}, new List<Medicine>() }
            };
        }

        public static IEnumerable<object[]> FilterData()
        {
            return new List<object[]>()
            {
                new object[] { MedicineDosageFilter.to200, new List<Medicine> { _dataFactory.StubMedicineForSearchAndFilter[0], _dataFactory.StubMedicineForSearchAndFilter[1], _dataFactory.StubMedicineForSearchAndFilter[2] } },
                new object[] { MedicineDosageFilter.from200to400, new List<Medicine> { _dataFactory.StubMedicineForSearchAndFilter[2], _dataFactory.StubMedicineForSearchAndFilter[3] } },
                new object[] { MedicineDosageFilter.from400to600, new List<Medicine> { _dataFactory.StubMedicineForSearchAndFilter[4] } },
                new object[] { MedicineDosageFilter.from600, new List<Medicine> { _dataFactory.StubMedicineForSearchAndFilter[5] } }
            };
        }

    }
}
