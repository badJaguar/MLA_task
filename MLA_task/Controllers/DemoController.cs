using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MLA_task.BLL.Interface;
using MLA_task.BLL.Interface.Exceptions;
using MLA_task.BLL.Interface.Models;
using NLog;
using Swashbuckle.Swagger.Annotations;

namespace MLA_task.Controllers
{
    [ApiVersion("1.0")]
    [System.Web.Http.RoutePrefix("api/models")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [SwaggerResponseRemoveDefaults]
    public class DemoController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IDemoModelService _demoModelService;

        public DemoController(ILogger logger, IDemoModelService demoModelService)
        {
            _logger = logger;
            _demoModelService = demoModelService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list models.", Type = typeof(DemoModel[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "No models created.", Type = typeof(DemoModel))]
        public async Task<IHttpActionResult> Get()
        {
            var models = await _demoModelService.GetDemoModelsAsync();

            return Ok(models);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a model by id.", Type = typeof(DemoModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong ID")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var model = await _demoModelService.GetDemoModelByIdAsync(id);

                _logger.Info($"item with id {id} has been received.");

                return Ok(model);
            }
            catch (DemoServiceException ex)
            {
                if (ex.Error == DemoServiceException.ErrorType.WrongId) 
                {
                    _logger.Info(ex, $"Wrong ID {id} has been requested");
                }

                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Server error occured while trying to get item with id {id}");
                return this.InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new demo model.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "WrongName")]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Post([System.Web.Http.FromBody]DemoModel model)
        {
            _logger.Info($"adding model with name {model.Name}");

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                _logger.Info($"Wrong model name {model.Name} detected");
            }

            try
            {
                var result =  await _demoModelService.AddDemoModelAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Server error occured while trying to add item with name {model.Name}");
                return this.InternalServerError();
            }

        }
    }
}