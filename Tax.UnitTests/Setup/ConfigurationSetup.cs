using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax.UnitTests.Setup
{
    public class ConfigurationSetup
    {
        public IConfiguration CreateConfiguration()
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("configurationTest.json")
                .Build();

            return configuration;
        }
    }
}
