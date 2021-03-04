﻿using GuildCars.Data.Factory;
using GuildCars.Models.QueriesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class AdminAPIController : ApiController
    {
        [Route("api/admin/vehicles/{minPrice=}/{maxPrice=}/{minYear=}/{maxYear=}/{makeModelYear=}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(int? minPrice, int? maxPrice, int? minYear, int? maxYear, string makeModelYear)
        {
            var repo = SalesRepositoryFactory.GetRepository();

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

                var result = repo.Search(parameters);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/admin/model/{makeId=}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult MakeByModel(int? makeId)
        {
            var repo = AdminRepositoryFactory.GetRepository();

            var result = repo.GetModelByMake(makeId);
            return Ok(result);
        }
    }
}