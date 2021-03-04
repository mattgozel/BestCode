using GuildCars.Models.QueriesModels;
using GuildCars.Models.TableModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Color = GuildCars.Models.TableModels.Color;

namespace GuildCars.UI.Models
{
    public class AdminAddVehicleViewModel : IValidatableObject
    {
        public IEnumerable<Makes> Makes { get; set; }
        public IEnumerable<CarModels> CarModels { get; set; }
        public IEnumerable<CarType> CarTypes { get; set; }
        public IEnumerable<BodyStyle> BodyStyles { get; set; }
        public IEnumerable<Transmission> Transmissions { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Interior> Interiors { get; set; }
        public InventoryDetails InventoryDetails { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if(!InventoryDetails.Year.HasValue)
            {
                errors.Add(new ValidationResult("Year is required"));
            }

            if (!InventoryDetails.Mileage.HasValue)
            {
                errors.Add(new ValidationResult("Mileage is required"));
            }

            if (InventoryDetails.TypeId == 1 && (InventoryDetails.Mileage < 0 || InventoryDetails.Mileage > 1000))
            {
                errors.Add(new ValidationResult("Mileage for new cars must be between 0 and 1000"));
            }

            if (InventoryDetails.TypeId == 2 && InventoryDetails.Mileage < 1000)
            {
                errors.Add(new ValidationResult("Mileage for used cars must be 1000+"));
            }

            if (!InventoryDetails.SalePrice.HasValue)
            {
                errors.Add(new ValidationResult("Sale price is required"));
            }

            if (!InventoryDetails.MSRP.HasValue)
            {
                errors.Add(new ValidationResult("MSRP is required"));
            }

            if (InventoryDetails.SalePrice > InventoryDetails.MSRP)
            {
                errors.Add(new ValidationResult("Sale price must be melow MSRP"));
            }

            if (string.IsNullOrEmpty(InventoryDetails.Vin))
            {
                errors.Add(new ValidationResult("Vin is required"));
            }

            if (string.IsNullOrEmpty(InventoryDetails.Description))
            {
                errors.Add(new ValidationResult("Description is required"));
            }

            if (ImageUpload != null)
            {
                var extensions = new string[] { ".png", ".jpg", ".jpeg" };

                var extention = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extention))
                {
                    errors.Add(new ValidationResult("Image must be in PNG, JPG or JPEG format"));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Image file is required"));
            }

            return errors;
        }
    }
}