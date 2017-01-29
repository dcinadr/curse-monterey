using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Curse.Monterey.WebService.Contracts;

namespace Curse.Monterey.WebService.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/user")]
        public IHttpActionResult Post([FromBody]User user)
        {


           return Ok();
        }
    }
}
