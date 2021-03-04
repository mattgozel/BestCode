using GuildCars.Data.Factory;
using GuildCars.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class InventoryAPIController : ApiController
    {
        [Route("api/inventory/new/{minPrice=}/{maxPrice=}/{minYear=}/{maxYear=}/{makeModelYear=}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult New(int? minPrice, int? maxPrice, int? minYear, int? maxYear, string makeModelYear)
        {
            var repo = InventoryDetailsRepositoryFactory.GetRepository();

            try
            {
                var parameters = new InventorySearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear, 
                    MakeModelYear = makeModelYear
                };
                
                var result = repo.SearchNew(parameters);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/inventory/used/{minPrice=}/{maxPrice=}/{minYear=}/{maxYear=}/{makeModelYear=}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Used(int? minPrice, int? maxPrice, int? minYear, int? maxYear, string makeModelYear)
        {
            var repo = InventoryDetailsRepositoryFactory.GetRepository();

            try
            {
                var parameters = new InventorySearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    MinYear = minYear,
                    MaxYear = maxYear,
                    MakeModelYear = makeModelYear
                };

                var result = repo.SearchUsed(parameters);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
