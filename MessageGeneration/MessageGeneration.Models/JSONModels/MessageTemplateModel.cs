using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Models.JSONModels
{
    public class MessageTemplateModel
    {
        public int MessageId { get; set; }
        public string MessageTitle { get; set; }
        public string Message { get; set; }
    }
}
