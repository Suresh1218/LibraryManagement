using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class LibraryApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(LoginModel model)
        {

            return Ok();
        }

    }
}
