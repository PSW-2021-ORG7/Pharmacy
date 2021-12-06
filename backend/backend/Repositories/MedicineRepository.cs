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
            /* string medicineJsonString = JsonConvert.SerializeObject(medicine, Formatting.Indented);
             try
             {

                 string fileName = medicine.Name + "_" + medicine.DosageInMilligrams + ".pdf";
                 File.WriteAllText("Output/" + fileName, medicineJsonString);
                 upload(fileName);
                 return fileName;
             }catch (Exception e)
             {
                 throw (e);
             }*/

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

               string filename = "Output" + Path.DirectorySeparatorChar + medicine.Name.Replace(" ", "") + "_" + medicine.DosageInMilligrams + ".pdf";
               document.Save(filename);
               return filename;
            }
            catch (Exception e)
            {
                throw (e);
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



    }
}
