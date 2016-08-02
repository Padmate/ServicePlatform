using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.API.Controllers
{
    public class ArticlesController:BaseApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}