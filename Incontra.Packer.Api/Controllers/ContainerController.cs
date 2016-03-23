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
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var container = _packerService.GetContainerByID(id);
                var boxes = _packerService.GetBoxByContainerID(id);
                var pContainer = ModelFactory.Create(container);
                var pBoxes = new List<InputBox>();
                pContainer.boxes = pBoxes;
                foreach (var box in boxes)
                {
                    pBoxes.Add(ModelFactory.Create(box));
                }
                var pContainers = new List<InputContainer> { pContainer };
                var packingResponse = new PackResponse();
                packingResponse.containers = pContainers;
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                var settings = new JsonSerializerSettings()
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        };
                response.Content = new StringContent(
                    JsonConvert.SerializeObject(packingResponse, settings), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception e)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(e.Message, Encoding.UTF8, "application/json");
                return response;
            }
        }

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
