using MessageGeneration.Data.JSONLogic;
using MessageGeneration.Models.JSONModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Tests
{
    [TestFixture]
    public class JSONTests
    {
        [Test]
        public void CanLoadMessages()
        {
            var repo = new MessageTemplateRepository();

            var list = repo.GetAll();

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0].MessageId);
            Assert.AreEqual("hotelGreeting", list[0].MessageTitle);
            Assert.AreEqual("Good [time] [firstName], and welcome to [company]! Room [roomNumber] is now ready for you. Enjoy your stay, and let us know if you need anything.", list[0].Message);
        }

        [Test]
        public void CanLoadCompanies()
        {
            var repo = new CompaniesRepository();

            var list = repo.GetAll();

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(3, list[2].Id);
            Assert.AreEqual("The Heartbreak Hotel", list[2].Company);
            Assert.AreEqual("Graceland", list[2].City);
            Assert.AreEqual("US/Central", list[2].Timezone);
        }

        [Test]
        public void CanLoadGuests()
        {
            var repo = new GuestRepository();

            var list = repo.GetAll();

            Assert.AreEqual(6, list.Count);
            Assert.AreEqual(4, list[3].Id);
            Assert.AreEqual("Melisa", list[3].FirstName);
            Assert.AreEqual("Preston", list[3].LastName);
            Assert.AreEqual(417, list[3].Reservation.RoomNumber);
            Assert.AreEqual(1486614763L, list[3].Reservation.StartTimestamp);
            Assert.AreEqual(1486832543L, list[3].Reservation.EndTimestamp);
        }

        [Test]
        public void CanCreateMessage()
        {
            var messageRepo = new MessageTemplateRepository();
            var companyRepo = new CompaniesRepository();
            var guestRepo = new GuestRepository();

            var messageList = messageRepo.GetAll();
            var companyList = companyRepo.GetAll();
            var guestList = guestRepo.GetAll();

            var message = messageRepo.CreateMessage(messageList[0], guestList[0], companyList[0]);

            if (message.Contains("afternoon"))
            {
                Assert.AreEqual("Good afternoon Candy, and welcome to Hotel California! Room 529 is now ready for you. Enjoy your stay, and let us know if you need anything.", message);
            }

            if (message.Contains("morning"))
            {
                Assert.AreEqual("Good morning Candy, and welcome to Hotel California! Room 529 is now ready for you. Enjoy your stay, and let us know if you need anything.", message);
            }

            if (message.Contains("evening"))
            {
                Assert.AreEqual("Good evening Candy, and welcome to Hotel California! Room 529 is now ready for you. Enjoy your stay, and let us know if you need anything.", message);
            }
        }

        [Test]
        public void CanCreateReservationConfirmation()
        {
            var messageRepo = new MessageTemplateRepository();
            var companyRepo = new CompaniesRepository();
            var guestRepo = new GuestRepository();

            var messageList = messageRepo.GetAll();
            var companyList = companyRepo.GetAll();
            var guestList = guestRepo.GetAll();

            var message = messageRepo.CreateMessage(messageList[1], guestList[0], companyList[0]);

            if (message.Contains("afternoon"))
            {
                Assert.AreEqual("Good afternoon Candy Pace, your reservation at Hotel California is confirmed! Your check in date and time is 1/1/0001 12:02:28 AM. We look forward to seeing you!", message);
            }

            if (message.Contains("morning"))
            {
                Assert.AreEqual("Good morning Candy Pace, your reservation at Hotel California is confirmed! Your check in date and time is 1/1/0001 12:02:28 AM. We look forward to seeing you!", message);
            }

            if (message.Contains("evening"))
            {
                Assert.AreEqual("Good evening Candy Pace, your reservation at Hotel California is confirmed! Your check in date and time is 1/1/0001 12:02:28 AM. We look forward to seeing you!", message);
            }
        }

        [Test]
        public void CanCreateCheckOutConfirmation()
        {
            var messageRepo = new MessageTemplateRepository();
            var companyRepo = new CompaniesRepository();
            var guestRepo = new GuestRepository();

            var messageList = messageRepo.GetAll();
            var companyList = companyRepo.GetAll();
            var guestList = guestRepo.GetAll();

            var message = messageRepo.CreateMessage(messageList[2], guestList[0], companyList[0]);

            if (message.Contains("afternoon"))
            {
                Assert.AreEqual("Good afternoon Candy Pace, your check out date and time is 1/1/0001 12:02:28 AM. Thank you for staying with us!", message);

                if (message.Contains("morning"))
                {
                    Assert.AreEqual("Good morning Candy Pace, your check out date and time is 1/1/0001 12:02:28 AM. Thank you for staying with us!", message);
                }

                if (message.Contains("evening"))
                {
                    Assert.AreEqual("Good evening Candy Pace, your check out date and time is 1/1/0001 12:02:28 AM. Thank you for staying with us!", message);
                }
            }
        }

        [Test]
        public void CanCreateCustomTemplate()
        {
            var messageRepo = new MessageTemplateRepository();
            var companyRepo = new CompaniesRepository();
            var guestRepo = new GuestRepository();

            var companyList = companyRepo.GetAll();
            var guestList = guestRepo.GetAll();

            string customMessage = "Does this work? [firstName] [time] [lastName] [company] [roomNumber] [checkIn] [checkOut]";

            var customMessageModel = new MessageTemplateModel();

            customMessageModel.Message = customMessage;

            var message = messageRepo.CreateMessage(customMessageModel, guestList[0], companyList[0]);

            if (message.Contains("afternoon"))
            {
                Assert.AreEqual("Does this work? Candy afternoon Pace Hotel California 529 1/1/0001 12:02:28 AM 1/1/0001 12:02:28 AM", message);

                if (message.Contains("morning"))
                {
                    Assert.AreEqual("Does this work? Candy morning Pace Hotel California 529 1/1/0001 12:02:28 AM 1/1/0001 12:02:28 AM", message);
                }

                if (message.Contains("evening"))
                {
                    Assert.AreEqual("Does this work? Candy evening Pace Hotel California 529 1/1/0001 12:02:28 AM 1/1/0001 12:02:28 AM", message);
                }
            }
        }
    }
}
