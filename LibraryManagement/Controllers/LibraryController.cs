using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryManagement.Controllers
{
    public class LibraryController : ApiController
    {
        public IHttpActionResult Login()
        {

            return BadRequest();
        }

    }
}
