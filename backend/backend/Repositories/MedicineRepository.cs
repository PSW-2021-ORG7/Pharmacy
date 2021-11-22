using backend.DAL;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly DrugStoreContext _dataContext;

        public MedicineRepository(DrugStoreContext dataContext) => _dataContext = dataContext;

        public bool MedicineExists(MedicineQuantityCheck DTO)
        {
            if (_dataContext.Medicine.Any(m => m.Name.Equals(DTO.Name)  && m.DosageInMilligrams.Equals(DTO.DosageInMg))) return true;
            return false;
        }

        public void Delete(Medicine medicine)
        {
            _dataContext.Medicine.Remove(medicine);
            _dataContext.SaveChanges();
        }

        public List<Medicine> GetAll() { 
            return _dataContext.Medicine.Include(m => m.Ingredients).ToList();
        }


       public Medicine GetByName(string name)
        {
            return _dataContext.Medicine.Include(m => m.Ingredients).SingleOrDefault(m => m.Name == name);
		}
		
        public Medicine GetById(string id)
        {
            return _dataContext.Medicine.SingleOrDefault(m => m.MedicineId.ToString() == id);
        }

        public Medicine GetByNameAndDose(string name, int dose)
        {
            return  _dataContext.Medicine.SingleOrDefault(m => m.Name == name && m.DosageInMilligrams == dose);
            
        }

        public bool RequestSpecification(Medicine medicine)
        {
            string medicineJsonString = JsonConvert.SerializeObject(medicine, Formatting.Indented);
            try
            {
                File.WriteAllText("Output/output.txt", medicineJsonString);
                upload();
                return true;
            }catch (Exception e)
            {
                throw (e);
            }
            
        }

        public void upload()
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.14", "tester", "password")))
            {
                client.Connect();
                string sourceFile = @"C:\Users\Iodum99\Desktop\PSW Projekat\Pharmacy\backend\backend\Output\output.txt";
                using (Stream stream = File.OpenRead(sourceFile))
                {
                    client.UploadFile(stream, @"\public\" + Path.GetFileName(sourceFile));
                }

                client.Disconnect();
            }
        }

        public bool Save(Medicine medicine)
        {
            if (_dataContext.Medicine.Any(m => m.Name == medicine.Name && m.DosageInMilligrams == medicine.DosageInMilligrams)) return false;

            _dataContext.Medicine.Add(medicine);
            _dataContext.SaveChanges();
            return true;
        }

        public bool Update(Medicine medicine)
        {
            bool success = false;
            var result = _dataContext.Medicine.SingleOrDefault(m => m.Id == medicine.Id);
            if (result != null)
            {
                _dataContext.Update(medicine);
                _dataContext.SaveChanges();
                success = true;
            }
            return success;

        }

        public Medicine GetByID(int id)
        {
            return _dataContext.Medicine.Include(m => m.Ingredients).SingleOrDefault(m => m.Id.Equals(id));
        }

        public bool DeleteMedicine(String id)
        {
            var medicine = _dataContext.Medicine.Find(id);
            if (medicine == null)
            {
                return false;
            }

            Delete(medicine);
            return true;
        }
    }
}
