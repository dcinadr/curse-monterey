using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Curse.Monterey.WebService.Contracts;

namespace Curse.Monterey.WebService.Controllers
{
    //[RoutePrefix("api/user")]
    class UserController : ApiController
    {
        //[HttpPost]
        //[Route("user")]
        public void Post([FromBody]User user)
        {


           // return Ok();
        }
    }
}
