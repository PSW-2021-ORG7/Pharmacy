using backend.DTO;
using backend.Model;


namespace backend.Repositories.Interfaces
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        public bool MedicineExists(MedicineQuantityCheck DTO);
        public Medicine getByName(string name);
    }
}
