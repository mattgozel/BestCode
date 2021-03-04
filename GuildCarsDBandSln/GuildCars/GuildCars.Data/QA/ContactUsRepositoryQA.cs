using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class ContactUsRepositoryQA : IContactUsRepository
    {
        private static List<ContactUs> _contactUs;

        static ContactUsRepositoryQA()
        {
            _contactUs = new List<ContactUs>()
            {
                new ContactUs { ContactUsId = 1, Name = "Fred", Email = "fred@fred.com", Phone = "12", Message = "Yo momma" },
                new ContactUs { ContactUsId = 2, Name = "Bob", Email = "bob@bob.com", Phone = null, Message = "I don't even own a phone"}
            };
        }

        public IEnumerable<ContactUs> GetAll()
        {
            return _contactUs;
        }

        public void Insert(ContactUs contact)
        {
            var contactList = GetAll();

            var maxId = contactList.Max(m => m.ContactUsId);

            contact.ContactUsId = maxId + 1;

            _contactUs.Add(contact);
        }
    }
}
