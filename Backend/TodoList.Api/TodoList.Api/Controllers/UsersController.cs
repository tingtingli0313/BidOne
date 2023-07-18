using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.ApiModels;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService context, ILogger<UsersController> logger)
        {
            _userService = context;
            _logger = logger;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var results = (await _userService.GetAllUsersAsync()).Value.OrderBy(x=> x.FirstName);
            return Ok(results);
        }

        // GET: api/users/...
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _userService.GetUserByIdAsyc(id);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }

        //PUT: api/users/... 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            var updateItem = new User(user.FirstName, user.LastName);
            updateItem.Id = id;
            await _userService.UpdateAsync(updateItem);
            return NoContent();
        }

        // POST: api/users 
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] CreateUserDTO request)
        {
            var newItem = new User(request.FirstName, request.LastName);

            var createUser = await _userService.AddAsync(newItem);
            if (!createUser.IsSuccess)
            {
                return BadRequest(createUser.Errors.First());
            }

            var result = new UserDTO()
            {
              Id = createUser.Value.Id,
              FirstName = createUser.Value.FirstName,
              LastName = createUser.Value.LastName,
            };
            return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
        }
    }
}
