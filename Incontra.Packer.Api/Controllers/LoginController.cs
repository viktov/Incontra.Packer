using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Incontra.Packer.Data.Service;
using Incontra.Packer.Data;
using Incontra.Packer.Api.Models;
using Incontra.Packer.Data.Model;
using Newtonsoft.Json;
using System.Text;
using System.Web.Http.Cors;
using System.Security.Cryptography;
using Incontra.Packer.Api.Helpers;

namespace Incontra.Packer.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private IUserService _userService;
                
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]LoginRequest request)
        {
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                User user = new User();
                user.UserName = request.UserName;
                user.Password = MD5Helper.GetHash(request.Password);                
                if (_userService.IsLoginValid(user))
                {                    
                    response.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                }
                else
                {
                    response.Content = new StringContent("Login or password incorrect", Encoding.UTF8, "application/json");                   
                }                
            }
            catch (Exception e)
            {
                Logger.LogException(e.ToString(), null);
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest);                
                response.Content = new StringContent(e.Message, Encoding.UTF8, "application/json");
                return response;
            }
            return response;
        }
    }
}
