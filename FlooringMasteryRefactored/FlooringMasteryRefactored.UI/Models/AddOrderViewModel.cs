using FlooringMasteryRefactored.Data.Factories;
using FlooringMasteryRefactored.Models.TableModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlooringMasteryRefactored.UI.Models
{
    public class AddOrderViewModel :IValidatableObject
    {
        public IEnumerable<Products> Products { get; set; }
        public IEnumerable<TaxInfo> Taxes { get; set; }
        public Orders Order { get; set; }
        public string SelectedProductName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var productsRepo = ProductsRepositoryFactory.GetRepository();
            Products = productsRepo.GetAll();

            if(string.IsNullOrEmpty(Order.CustomerName))
            {
                errors.Add(new ValidationResult("Customer name is required"));
            }

            if(Order.Area < 100)
            {
                errors.Add(new ValidationResult("Minimum area is 100"));
            }

            if(string.IsNullOrEmpty(Order.StateAbbreviation))
            {
                errors.Add(new ValidationResult("State is required"));
            }

            if(Order.ProductId < 0 || Order.ProductId > Products.Count())
            {
                errors.Add(new ValidationResult("You must select a valid product"));
            }

            return errors;
        }
    }
}