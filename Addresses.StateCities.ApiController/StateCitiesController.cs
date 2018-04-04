// Copyright © Christopher Dorst. All rights reserved.
// Licensed under the GNU General Public License, Version 3.0. See the LICENSE document in the repository root for license information.

using Addresses.StateCities.DatabaseContext;
using DevOps.Code.DataAccess.Interfaces.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Addresses.StateCities.ApiController
{
    /// <summary>ASP.NET Core web API controller for StateCity entities</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StateCitiesController : ControllerBase
    {
        /// <summary>Represents the application events logger</summary>
        private readonly ILogger<StateCitiesController> _logger;

        /// <summary>Represents repository of StateCity entity data</summary>
        private readonly IRepository<StateCityDbContext, StateCity, int> _repository;

        /// <summary>Constructs an API controller for StateCity entities using the given repository service</summary>
        public StateCitiesController(ILogger<StateCitiesController> logger, IRepository<StateCityDbContext, StateCity, int> repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>Handles HTTP GET requests to access StateCity resources at the given ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StateCity>> Get(int id)
        {
            if (id < 1) return NotFound();
            var resource = await _repository.FindAsync(id);
            if (resource == null) return NotFound();
            return resource;
        }

        /// <summary>Handles HTTP HEAD requests to access StateCity resources at the given ID</summary>
        [HttpHead("{id}")]
        public ActionResult<StateCity> Head(int id) => null;

        /// <summary>Handles HTTP POST requests to save StateCity resources</summary>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<StateCity>> Post(StateCity resource)
        {
            var saved = await _repository.AddAsync(resource);
            return CreatedAtAction(nameof(Get), new { id = saved.GetKey() }, saved);
        }
    }
}
