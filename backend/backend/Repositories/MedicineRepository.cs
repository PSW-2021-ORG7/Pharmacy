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
            if (_dataContext.Medicine.Any(m => m.Name.ToLower().Equals(DTO.Name.ToLower())  && m.DosageInMilligrams.Equals(DTO.DosageInMg))) return true;
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
		
 
        public Medicine GetByNameAndDose(string name, int dose)
        {
            return  _dataContext.Medicine.SingleOrDefault(m => m.Name.ToLower().Equals(name.ToLower()) && m.DosageInMilligrams == dose);
            
        }

        public String RequestSpecification(Medicine medicine)
        {
            string medicineJsonString = JsonConvert.SerializeObject(medicine, Formatting.Indented);
            try
            {
                string fileName = medicine.Name + "_" + medicine.DosageInMilligrams + ".txt";
                File.WriteAllText("Output/" + fileName, medicineJsonString);
                upload(fileName);
                return fileName;
            }catch (Exception e)
            {
                throw (e);
            }
            
        }

        public void upload(string fileName)
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.14", "tester", "password")))
            {
                client.Connect();
                string sourceFile = Path.Combine(Environment.CurrentDirectory, @"Output\", fileName);
                using (Stream stream = File.OpenRead(sourceFile))
                {
                    client.UploadFile(stream, @"\public\" + fileName);
                }

                client.Disconnect();
            }
        }

        public bool Save(Medicine medicine)
        {
            if (_dataContext.Medicine.Any(m => m.Name == medicine.Name 
                                        && m.DosageInMilligrams == medicine.DosageInMilligrams 
                                        && m.Manufacturer.Equals(medicine.Manufacturer))) return false;
            ExcludeIngredientDuplicates(medicine);
            _dataContext.Add(medicine);
            _dataContext.SaveChanges();
            return true;
        }

        private void ExcludeIngredientDuplicates(Medicine medicine)
        {
            var duplicates = _dataContext.Ingredient
                .AsEnumerable()
                .Where(i => medicine.Ingredients.Any(m => m.Name.Equals(i.Name)))
                .ToList();
            foreach (Ingredient duplicate in duplicates)
            {
                var itemToRemove = medicine.Ingredients.Single(m => m.Name == duplicate.Name);
                medicine.Ingredients.Remove(itemToRemove);
            }
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

        public bool DownloadPrescriptionSFTP(String fileName)
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.1.3", "tester", "password")))
            {
                client.Connect();

                string sourceFileServer = @"\public\" + fileName;
                string sourceFileLocal = Path.Combine(Environment.CurrentDirectory, @"Downloads\", fileName);


                using (Stream stream = File.OpenWrite(sourceFileLocal))
                {
                    client.DownloadFile(sourceFileServer, stream);
                }

                client.Disconnect();
            }
            return true;
        }

    }
}
