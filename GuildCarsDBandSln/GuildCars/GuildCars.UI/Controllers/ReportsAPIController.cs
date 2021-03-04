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
    public class ReportsAPIController : ApiController
    {
        [Route("api/reports/sales/{userId=}/{fromDate=}/{toDate=}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string userId, string fromDate, string toDate)
        {
            var repo = ReportsRepositoryFactory.GetRepository();

            try
            {
                var parameters = new ReportSalesSearchParameters()
                {
                    UserId = userId,
                    FromDate = fromDate,
                    ToDate = toDate
                };

                var result = repo.GetAll(parameters);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
