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
    public class GuestRepository : IGuestRepositoryInterface
    {
        public List<GuestModel> GetAll()
        {
            List<GuestModel> guests = new List<GuestModel>();

            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string JsonFolderPath = "JSONFiles/Guests.json";

            using (StreamReader sr = new StreamReader(Path.Combine(filePath, JsonFolderPath)))
            {
                string json = sr.ReadToEnd();

                guests = JsonConvert.DeserializeObject<List<GuestModel>>(json);
            }

            return guests;
        }
    }
}
