using FlooringMasteryRefactored.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlooringMasteryRefactored.UI.Models
{
    public class EditViewModel
    {
        public IEnumerable<Orders> Orders { get; set; }
    }
}