using backend.DAL;
using backend.DTO;
using backend.Model;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfDocument = PdfSharp.Pdf.PdfDocument;

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
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                PdfDocument document = new PdfDocument();
                document.Info.Title = medicine.Name + "_" + medicine.DosageInMilligrams;
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 10, XFontStyle.Regular);
                XFont fontTitle = new XFont("Verdana", 20, XFontStyle.Bold);

                gfx.DrawString("Medicine specification", fontTitle, XBrushes.Goldenrod, 5.0, 25.0);
                gfx.DrawString("Medicine name:", font, XBrushes.Goldenrod, 5.0, 40.0);
                gfx.DrawString(medicine.Name, font, XBrushes.Black,
                                        new XRect(200.0, 35.0, 0.0, 0.0),
                                        XStringFormats.Center);

                gfx.DrawString("Description: ", font, XBrushes.Goldenrod, 5.0, 60.0);
                gfx.DrawString(medicine.Description, font, XBrushes.Black,
                                        new XRect(200.0, 55.0, 0.0, 0.0),
                                        XStringFormats.Center);

                gfx.DrawString("Way of consumption: ", font, XBrushes.Goldenrod, 5.0, 80.0);
                gfx.DrawString(medicine.WayOfConsumption, font, XBrushes.Black,
                                        new XRect(200.0, 75.0, 0.0, 0.0),
                                        XStringFormats.Center);

                gfx.DrawString("Potential dangers: ", font, XBrushes.Goldenrod, 5.0, 100.0);
                gfx.DrawString(medicine.PotentialDangers, font, XBrushes.Black,
                                        new XRect(200.0, 95.0, 0.0, 0.0),
                                        XStringFormats.Center);

                string filename = medicine.Name.Replace(" ", "") + "_" + medicine.DosageInMilligrams + ".pdf";
                string path = Path.Combine(Environment.CurrentDirectory, @"Output\", filename);
                document.Save(path);
                upload(filename);
                return filename;
            }
            catch (Exception e)
            {
                return "";
            }
            
        }

        public void upload(string fileName)
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.56.1", "tester", "password")))
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
