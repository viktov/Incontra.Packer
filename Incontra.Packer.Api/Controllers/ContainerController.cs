using Incontra.Packer.Api.Models;
using Incontra.Packer.Data.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Incontra.Packer.Api.Controllers
{
    public class ContainerController : ApiController
    {
        private IPackerService _packerService;
        private ModelFactory _modelFactory;
        protected ModelFactory ModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory();
                }
                return _modelFactory;
            }
        }

        public ContainerController(IPackerService packerService)
        {
            _packerService = packerService;
        }

        //// GET api/container
        //public string Get()
        //{
        //    return "OK";
        //}

        // GET api/container/5
        //[HttpGet]
        //public HttpResponseMessage Get(int id)
        //{
            
        //}

        //// POST api/container
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/container/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/container/5
        //public void Delete(int id)
        //{
        //}
    }
}
