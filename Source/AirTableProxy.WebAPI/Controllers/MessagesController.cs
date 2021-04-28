using AirTableProxy.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AirTableProxy.WebAPI.Services;
using Microsoft.Extensions.Configuration;

namespace AirTableProxy.WebAPI.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly string _baseId;
        private readonly string _appKey;
        private readonly IAirTableService _airTableService;

        public MessagesController(IConfiguration configuration, IAirTableService airTableService)
        {
            _airTableService = airTableService;
            _baseId = configuration["AirTable:BaseId"];
            _appKey = configuration["AirTable:AppKey"];
        }

        // GET: api/<MessagesController>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ResponseModel>> Get()
        {
            return await _airTableService.GetRecords(_appKey, _baseId);
        }

        // POST api/<MessagesController>
        [HttpPost]
        [Authorize]
        public async Task<ResponseModel> Post([FromBody] RequestModel request)
        {
            return await _airTableService.AddRecord(request, _appKey, _baseId);
        }
    }
}