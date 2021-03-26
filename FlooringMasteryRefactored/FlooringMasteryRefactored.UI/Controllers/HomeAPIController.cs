using FlooringMasteryRefactored.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlooringMasteryRefactored.UI.Controllers
{
    public class HomeAPIController : ApiController
    {
        [Route("api/home/orders/{dateAdded=}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string dateAdded)
        {
            var repo = OrdersRepositoryFactory.GetRepository();

            try
            {
                var result = repo.GetAll(dateAdded);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
