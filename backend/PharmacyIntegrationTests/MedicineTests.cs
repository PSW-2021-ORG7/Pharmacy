using backend;
using backend.DAL;
using backend.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyIntegrationTests
{
    public class MedicineTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public MedicineTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        public HttpClient createClient ()
        {
            WebApplicationFactoryClientOptions clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost:64677"),
                HandleCookies = true
            };

            var client = _factory.CreateClient(clientOptions);
            client.DefaultRequestHeaders.Add("ApiKey", "XYZX");

            return client;

        }

        public ByteArrayContent createByteArrayContent(object obj)
        {

            var content = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;

        }

        /*
        [Theory]
        [InlineData("/medicine")]
        [InlineData("/inventory")]
        [InlineData("/hospital")]
        [InlineData("/api/feedback")]
        public async Task Get_http_request(string url)
        {
            //Arrange
            var client = createClient();
                    
            //Act
            var response = await client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //Unauthorized acces
        [Theory]
        [InlineData("/medicine")]
        [InlineData("/hospital")]
        [InlineData("/api/feedback")]

        public async Task Get_http_request_401(string url)
        {
            //Arrange
            var client = createClient();
            client.DefaultRequestHeaders.Remove("ApiKey");

            //Act
            var response = await client.GetAsync(url);

            //Assert
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }

        [Theory]
        [InlineData("/medicine/find/brufen/400", "OK")]
        [InlineData("/medicine/find/brufen/600", "OK")]
        [InlineData("/medicine/find/BrUFen/400", "OK")]
        [InlineData("/medicine/find/BrUFen/600", "OK")]
        [InlineData("/medicine/find/I Don't Exist/600", "NotFound")]
        [InlineData("/medicine/find/I Don't Exist Either/6000", "NotFound")]
        [InlineData("/medicine/find/Dimigal/50", "NotFound")]
        [InlineData("/medicine/find/Brufen/60000", "NotFound")]
        public async Task Get_medicine_by_name_and_dose(string url, string expectedStatusCode)
        {

            //Arrange
            var client = createClient();

            //Act
            var response = await client.GetAsync(url);

            //Assert
            //response.EnsureSuccessStatusCode();
            Assert.Equal(expectedStatusCode, response.StatusCode.ToString());

        }
   
        [Theory]
        [InlineData("/inventory/check", "brufen", 400, 1, "OK")]
        [InlineData("/inventory/check", "BRufen", 400, 1, "OK")]
        [InlineData("/inventory/check", "brufen", 600, 1, "OK")]
        [InlineData("/inventory/check", "brufen", 500, 1, "BadRequest")] //Doesn't exist wit hdosage 500
        [InlineData("/inventory/check", "BRufen", 400, 1000, "BadRequest")] //Doesn't exist in such quantity
        [InlineData("/inventory/check", "Panadol", 600, 1, "BadRequest")] //Both medicine and dosage don't exist
        public async Task Check_medicine_quantity(string url, string name, int dosage, int quantity, string expectedStatusCode)
        {

            //Arrange
            var client = createClient();
            MedicineQuantityCheck dto = new MedicineQuantityCheck { Name = name, DosageInMg = dosage, Quantity = quantity };

            var byteContent = createByteArrayContent(dto);
            var result = client.PostAsync(url, byteContent).Result;
            //Act

            //Assert           
            Assert.Equal(expectedStatusCode, result.StatusCode.ToString()); ;

        }
        */


    }
}
