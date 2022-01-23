using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Model
{
    public class EmailConfiguration
    {
        public readonly string _from = string.Empty;
        public readonly string _smtp = string.Empty;
        public readonly string _port = string.Empty;
        public readonly string _username = string.Empty;
        public readonly string _password = string.Empty;

        public EmailConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            _from = root.GetSection("EmailConfiguration").GetSection("From").Value;
            _smtp = root.GetSection("EmailConfiguration").GetSection("SmtpServer").Value;
            _port = root.GetSection("EmailConfiguration").GetSection("Port").Value;
            _username = root.GetSection("EmailConfiguration").GetSection("Username").Value;
            _password = root.GetSection("EmailConfiguration").GetSection("Password").Value;


            var appSetting = root.GetSection("ApplicationSettings");
        }

        public string From { get => _from; }
        public string SmtpServer { get => _smtp; }
        public int Port { get => Int32.Parse(_port); }
        public string UserName { get => _username; }
        public string Password { get => _password; }
    }
}
