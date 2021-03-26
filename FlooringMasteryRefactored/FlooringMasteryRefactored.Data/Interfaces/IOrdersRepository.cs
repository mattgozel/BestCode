using FlooringMasteryRefactored.Models.QueriesModels;
using FlooringMasteryRefactored.Models.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryRefactored.Data.Interfaces
{
    public interface IOrdersRepository
    {
        IEnumerable<DisplayOrdersModel> GetAll(string dateAdded);
        Orders DisplayPreliminaryOrder(string customerName, string state, string productName, decimal area);
        void Insert(Orders order);
        void Update(Orders order);
        void Delete(int orderNumber);
        Orders GetById(int id);
        IEnumerable<Orders> GetOrders();
    }
}
