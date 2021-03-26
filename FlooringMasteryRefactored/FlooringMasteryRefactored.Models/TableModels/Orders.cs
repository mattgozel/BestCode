using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Models.TableModels
{
    public class Orders
    {
        public int OrderNumber { get; set; }
        public string DateAdded { get; set; }
        public string CustomerName { get; set; }
        public string StateAbbreviation { get; set; }
        public int ProductId { get; set; }
        public decimal Area { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
