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
        public async Task<IHttpActionResult> Get()
        {
            var models = await _demoModelService.GetDemoModelsAsync().ConfigureAwait(true);

            return Ok(models);
        }

        // GET: Demo
        public async Task<IHttpActionResult> Get(int id)
        {
            _logger.Info($"receiving item with id {id}");

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
                    return this.BadRequest("Wrong ID");
                }

                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Server error occured while trying to get item with id {id}");
                return this.InternalServerError(ex);
            }
        }

        //public async Task<IHttpActionResult> Post([FromBody]DemoModel model)
        //{
        //    _logger.Info($"adding model with name {model.Name}");

        //    if (model.Name == "bla-bla") {
        //        _logger.Info($"Wrong model name {model.Name} detected");
        //        return this.BadRequest("WrongName");
        //    }

        //    try {
        //        _context.DemoDbModels.Add(model);
        //        await _context.SaveChangesAsync();
        //    } catch (Exception ex) {
        //        _logger.Error(ex, $"Server error occured while trying to add item with name {model.Name}");
        //        return this.InternalServerError();
        //    }
           
        //    return Ok();
        //}
    }
}