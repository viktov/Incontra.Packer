using Incontra.Packer.Api.Models;
using Incontra.Packer.Data.Model;
using Incontra.Packer.Core.Model;
using Incontra.Packer.Data.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.IO;
using Newtonsoft.Json.Serialization;
using Incontra.Packer.Data;

namespace Incontra.Packer.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PackController : ApiController
    {
        private ModelFactory _modelFactory;
        private IPackerService _packerService;
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

        private double _executionTime;
        private int _spaceTotal;
        private int _spaceUsed;
        private double _weightTotal;

        public PackController(IPackerService packerService)
        {
            _packerService = packerService;
        }

        [HttpGet]
        public string Get(string id)
        {
            return "OK";
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Models.PackRequest request)
        {
            try
            {
                var containers = GetPackedContainers(request);
                var options = request.options;
                var packResponse = new PackResponse();
                packResponse.containers = GetJsonContainers(containers, request);
                packResponse.executionTime = String.Format("{0}s.", Math.Round(_executionTime,4));
                packResponse.status = HttpStatusCode.OK.ToString();

                if(request.options.showSpaceUsage)
                    packResponse.spaceUsage = String.Format("{0}%", Math.Round((decimal)_spaceUsed / _spaceTotal * 100, 2));
                if(request.options.showTotalWeight)
                    packResponse.weight = String.Format("{0}{1}", Math.Round(_weightTotal,4), request.options.weightUnit);

                packResponse.message = String.Format("{0} boxes packed in {1} container{2}"
                    , GetBoxesCount(request.boxes)
                    , packResponse.containers.Count
                    , packResponse.containers.Count > 1 ? "s" : "");
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(packResponse), Encoding.UTF8, "application/json");
                return response;
            }
            catch(Exception e)
            {                
                //Logger.LogException(e.ToString(), null);
                var packResponse = new PackResponse();
                packResponse.status = HttpStatusCode.BadRequest.ToString();
                packResponse.message = String.Format("{0}", e.ToString());
                var response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(JsonConvert.SerializeObject(packResponse), Encoding.UTF8, "application/json");
                return response;
            }            
        }

        private int GetBoxesCount(List<InputBox> boxes)
        {
            return boxes.Select(b => b.q).Sum();
        }

        private List<Container> GetPackedContainers(Models.PackRequest request)
        {
            var packer = new Core.Packer();            
            var boxes = new List<Box>();
            var containers = new List<Container>();
            var inputContainers = new List<Container>();            

            foreach (var inputContainer in request.containers)
            {
                var container = ModelFactory.Create(inputContainer);
                inputContainers.Add(container);
            }
            packer.InputContainers = inputContainers;
                
            foreach (var inputBox in request.boxes)
            {              
                for (int i = 0; i < inputBox.q; i++)
                {
                    var box = ModelFactory.Create(inputBox);
                    boxes.Add(box);                    
                }
            }

            containers = packer.Pack(boxes);
            _executionTime = packer.LastExecutionTime;            
            return containers;
        }

        private List<InputContainer> GetJsonContainers(List<Container> containers, Models.PackRequest request)
        {
            var packContainers = new List<InputContainer>();
            var calculation = new Data.Model.PackRequest
            {
                UserID = 1,
                RequestDate = DateTime.Now,
                CalculationTime = _executionTime
            };
            //calculation = _packerService.PackRequestInsert(calculation);
            _spaceTotal = 0;
            _spaceUsed = 0;
            foreach (var container in containers)
            {
                _weightTotal += container.Weight;
                _spaceTotal += container.Width * container.Height * container.Depth;
                var packContainer = ModelFactory.Create(container);
                var containerGuid = Guid.NewGuid();
                string contents = containerGuid.ToString();
                var container3dContent = String.Format(@"
                    var containerMesh = new BABYLON.Mesh.CreateBox(""containerMesh"", 1, scene);
                    containerMesh.scaling = new BABYLON.Vector3({0}+0.2, {1}+0.2, {2}+0.2);
                    containerMesh.material = containerMaterial;
                    self.drawEdges(scene, containerMesh, new BABYLON.Color3(0, 0, 0), 0.1);"
                    , container.Width
                    , container.Height
                    , container.Depth                    
                    );

                var spaceUsedInContainer = 0;
                var spaceInContainer = container.Width * container.Height * container.Depth;
                var weightOfBoxesInContainer = 0.0;
                foreach (var box in container.Boxes)
                {
                    _weightTotal += box.Weight;
                    _spaceUsed += box.Width * box.Height * box.Depth;
                    spaceUsedInContainer += box.Width * box.Height * box.Depth;
                    weightOfBoxesInContainer += box.Weight;
                    var packBox = ModelFactory.Create(box);
                    packContainer.boxes.Add(packBox);
                    container3dContent += String.Format(@"
                        var boxMesh = new BABYLON.Mesh.CreateBox(""boxMesh"", 1, scene);
                        boxMesh.scaling = new BABYLON.Vector3({0},{1},{2});
                        boxMesh.position = new BABYLON.Vector3({3}-{6}/2+{0}/2, {4}-{7}/2+{1}/2, {5}-{8}/2+{2}/2);
                        boxMesh.material = self.createBoxMaterial(scene, {9}, {10}, {11}, 1);
                        self.drawEdges(scene, boxMesh, new BABYLON.Color3(0, 0, 0), 0.5);"
                        , box.Width
                        , box.Height
                        , box.Depth
                        , box.X
                        , box.Y
                        , box.Z
                        , container.Width
                        , container.Height
                        , container.Depth 
                        , box.R.ToString().Replace(',', '.')
                        , box.G.ToString().Replace(',', '.')
                        , box.B.ToString().Replace(',', '.')
                        );
                }

                if (request.options.showSpaceUsage)
                    packContainer.spaceUsage = String.Format("{0}%", Math.Round((decimal)spaceUsedInContainer / spaceInContainer * 100, 2));
                if (request.options.showTotalWeight)
                    packContainer.weight = String.Format("{0}{1}", Math.Round(weightOfBoxesInContainer,4), request.options.weightUnit);
                
                var path = System.Web.HttpContext.Current.Server.MapPath("/Content/Visualizers/");
                
                var folder = String.Format("{0}-{1}{2}-{3}{4}"
                    , DateTime.Today.Year
                    , DateTime.Today.Month < 10 ? "0" : String.Empty
                    , DateTime.Today.Month
                    , DateTime.Today.Day < 10 ? "0" : String.Empty
                    , DateTime.Today.Day
                    );
                var container3dFile = String.Format("container.3d.{0}.html", containerGuid);
                var container3dTemplate = File.ReadAllText(Path.Combine(path, "container.3d.html"));
                var cameraDistance = Math.Max(container.Width, Math.Max(container.Height, container.Depth)) * 2;
                container3dContent = String.Format(container3dTemplate, container3dContent, cameraDistance);
                if (!Directory.Exists(Path.Combine(path,folder)))
                    Directory.CreateDirectory(Path.Combine(path, folder));
                File.WriteAllText(Path.Combine(Path.Combine(path, folder), container3dFile), container3dContent);

                var visualizer3D = string.Format(@"<iframe id=""3dp"" width=""300"" height=""300"" style=""border: none;"" src=""http://localhost:53675/{0}""></iframe>"
                    , String.Format("Content/Visualizers/{0}/container.3d.{1}.html"
                        , folder
                        , containerGuid));
                   
                packContainer.view3d = visualizer3D;
                packContainers.Add(packContainer);
            }
            return packContainers;
        }
    }
}