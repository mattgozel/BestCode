using MessageGeneration.Data.Interfaces;
using MessageGeneration.Models.JSONModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Data.JSONLogic
{
    public class CompaniesRepository : ICompaniesRepositoryInterface
    {
        public List<CompaniesModel> GetAll()
        {
            List<CompaniesModel> companies = new List<CompaniesModel>();

            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string JsonFolderPath = "JSONFiles/Companies.json";

            using (StreamReader sr = new StreamReader(Path.Combine(filePath, JsonFolderPath)))
            {
                string json = sr.ReadToEnd();

                companies = JsonConvert.DeserializeObject<List<CompaniesModel>>(json);
            }

            return companies;
        }
    }
}
