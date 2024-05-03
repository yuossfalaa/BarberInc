using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BarberInc.Models
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public class App_Settings
    {
        private Assembly assembly;
        public Settings appSettings;
        public App_Settings()
        {
            assembly = Assembly.GetExecutingAssembly();
            init_App_Settings();
        }
        private void init_App_Settings()
        {
            string resourceName = "BarberInc.AppSettings.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    appSettings = JsonConvert.DeserializeObject<Settings>(result);

                }
            }
        }
    }
}
