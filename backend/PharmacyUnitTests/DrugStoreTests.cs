using backend.Repositories;
using backend.Testing;
using Xunit;

namespace PharmacyUnitTests
{
    public class DrugStoreTests : IClassFixture<DrugStoreSeedDataFixture>
    {
        DrugStoreSeedDataFixture fixture;
        MedicineRepository medicineRepository;
        public DrugStoreTests(DrugStoreSeedDataFixture fixture)
        {
            this.fixture = fixture;
            this.medicineRepository = new MedicineRepository(fixture.context);
        }

        [Fact]
        public void Test1()
        {
            Assert.True(1 == 1);
        }
    }
}
