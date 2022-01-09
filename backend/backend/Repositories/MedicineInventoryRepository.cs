using backend.DAL;
using backend.DTO.TenderingDTO;
using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class MedicineInventoryRepository : IMedicineInventoryRepository
    {
        private readonly DrugStoreContext _dataContext;

        public MedicineInventoryRepository(DrugStoreContext dataContext) => _dataContext = dataContext;

        public bool CheckMedicineQuantity(MedicineInventory medicineInventory)
        {
            if (_dataContext.MedicineInventory.Any(m => m.MedicineId.Equals(medicineInventory.MedicineId) 
            && m.Quantity >= medicineInventory.Quantity)) return true;
            return false;
        }

        public void Delete(MedicineInventory entity)
        {
            _dataContext.MedicineInventory.Remove(entity);
            _dataContext.SaveChanges();
        }

        public List<MedicineInventory> GetAll()
        {
            return _dataContext.MedicineInventory.ToList();
        }

        public bool Save(MedicineInventory entity)
        {
            _dataContext.MedicineInventory.Add(entity);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(MedicineInventory entity)
        {
            MedicineInventory result = _dataContext.MedicineInventory.SingleOrDefault(m => m.MedicineId.Equals(entity.MedicineId));
            if (result != null)
            {
                result.Quantity = entity.Quantity;
                result.Price = entity.Price;
                if (result.Quantity < 0 || result.Price<0) return false;
                _dataContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ReduceMedicineQuantity(MedicineInventory entity)
        {
            var medicines = GetAll();
            var result = medicines.SingleOrDefault(m => m.MedicineId == entity.MedicineId);
            if (result != null)
            {
                result.Quantity -= entity.Quantity;
                if (result.Quantity < 0) return false;
                _dataContext.SaveChanges();
                return true;
            }
            return false;
        }

        MedicineInventory IMedicineInventoryRepository.FindRequestedMedicineInventory(TenderingItemRequestDTO tenderingItemRequestDTO)
        {
            
                Medicine requiredMedicine = _dataContext.Medicine.Where(m => m.Name.ToLower().Equals(tenderingItemRequestDTO.MedicineName.ToLower()))
                    .Where(m => m.DosageInMilligrams.Equals(tenderingItemRequestDTO.DosageInMilligrams))
                    .SingleOrDefault(m=> m.Manufacturer.Equals(tenderingItemRequestDTO.Manufacturer));
                if(requiredMedicine == null)
                {
                    return null;
                }
                else
                {
                    MedicineInventory requestedMedicineInventory = _dataContext.MedicineInventory.SingleOrDefault(m => m.MedicineId.Equals(requiredMedicine.Id));
                    return requestedMedicineInventory;
                }
            
        }
    }
}
