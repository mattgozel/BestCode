using GuildCars.Data.Interfaces;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.ADO
{
    public class ContactUsRepositoryADO : IContactUsRepository
    {
        public IEnumerable<ContactUs> GetAll()
        {
            List<ContactUs> contacts = new List<ContactUs>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactUsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ContactUs currentRow = new ContactUs();

                        currentRow.ContactUsId = (int)dr["ContactUsId"];
                        currentRow.Name = dr["Name"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.Phone = dr["Phone"].ToString();
                        currentRow.Message = dr["Message"].ToString();

                        contacts.Add(currentRow);
                    }
                }
            }

            return contacts;
        }

        public void Insert(ContactUs contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactUsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ContactUsId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Name", contact.Name);

                if (!string.IsNullOrEmpty(contact.Email))
                {
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                }

                if (!string.IsNullOrEmpty(contact.Phone))
                {
                    cmd.Parameters.AddWithValue("@Phone", contact.Email);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@Message", contact.Message);

                cn.Open();

                cmd.ExecuteNonQuery();

                contact.ContactUsId = (int)param.Value;
            }
        }
    }
}
