using backend.Model;
using backend.Repositories.Interfaces;
using backend.Services;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace PharmacyUnitTests
{
    public class MedicineInventoryTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Change_medicine_quantity(MedicineInventory medicineInventory,bool updated)
        {
            var stubRepository = CreateStubRepository();
            MedicineInventoryService service = new MedicineInventoryService(stubRepository.Object);
            bool result = service.ReduceMedicineQuantity(medicineInventory);
            result.ShouldBe(updated);
        }

        private static Mock<IMedicineInventoryRepository> CreateStubRepository()
        {
            var stubRepository = new Mock<IMedicineInventoryRepository>();

            MedicineInventory medicineInventory = new MedicineInventory(1, 5);
            List<MedicineInventory> medicineInventories = new List<MedicineInventory>();
            medicineInventories.Add(medicineInventory);
            stubRepository.Setup(m => m.GetAll()).Returns(medicineInventories);
            return stubRepository;
        }


        public static IEnumerable<object[]> Data()
        {
            var retval = new List<object[]>();
            retval.Add(new object[] { CreateInventory(1,6), false });
            retval.Add(new object[] { CreateInventory(1,3), true });

            return retval;
        }

        private static object CreateInventory(int v1, int v2)
        {
            return new MedicineInventory(v1, v2);
        }
    }
}
