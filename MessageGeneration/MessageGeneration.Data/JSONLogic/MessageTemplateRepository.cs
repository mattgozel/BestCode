using MessageGeneration.Data.Interfaces;
using MessageGeneration.Models.JSONModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Data.JSONLogic
{
    public class MessageTemplateRepository : IMessageTemplateRepositoryInterface
    {
        public string CreateMessage(MessageTemplateModel message, GuestModel guest, CompaniesModel company)
        {
            string messagetoSend = message.Message;

            if(messagetoSend.Contains("[firstName]"))
            {
                messagetoSend = messagetoSend.Replace("[firstName]", guest.FirstName);
            }

            if(messagetoSend.Contains("[time]"))
            {
                var timeGreeting = GetTimeOfDayGreeting();
                messagetoSend = messagetoSend.Replace("[time]", timeGreeting);
            }

            if(messagetoSend.Contains("[company]"))
            {
                messagetoSend = messagetoSend.Replace("[company]", company.Company);
            }

            if(messagetoSend.Contains("[roomNumber]"))
            {
                messagetoSend = messagetoSend.Replace("[roomNumber]", guest.Reservation.RoomNumber.ToString());
            }

            if(messagetoSend.Contains("[lastName]"))
            {
                messagetoSend = messagetoSend.Replace("[lastName]", guest.LastName);
            }

            if(messagetoSend.Contains("[checkIn]"))
            {
                var checkInDateTime = new DateTime(guest.Reservation.StartTimestamp).ToString();

                messagetoSend = messagetoSend.Replace("[checkIn]", checkInDateTime);
            }

            if (messagetoSend.Contains("[checkOut]"))
            {
                var checkOutDateTime = new DateTime(guest.Reservation.EndTimestamp).ToString();

                messagetoSend = messagetoSend.Replace("[checkOut]", checkOutDateTime);
            }

            return messagetoSend;
        }

        public List<MessageTemplateModel> GetAll()
        {
            List <MessageTemplateModel> messages = new List<MessageTemplateModel>();

            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string JsonFolderPath = "JSONFiles/MessageTemplates.JSON";

            using (StreamReader sr = new StreamReader(Path.Combine(filePath, JsonFolderPath)))
            {
                string json = sr.ReadToEnd();

                messages = JsonConvert.DeserializeObject<List<MessageTemplateModel>>(json);
            }

            return messages;
        }

        public string GetTimeOfDayGreeting()
        {
            TimeSpan morning = new TimeSpan(0, 0, 0);
            TimeSpan afternoon = new TimeSpan(11, 0, 0);
            TimeSpan evening = new TimeSpan(17, 0, 0);

            TimeSpan now = DateTime.Now.TimeOfDay;

            if(now >= morning && now < afternoon)
            {
                return "morning";
            }

            if(now >= afternoon && now < evening)
            {
                return "afternoon";
            }

            else
            {
                return "evening";
            }

        }
    }
}
