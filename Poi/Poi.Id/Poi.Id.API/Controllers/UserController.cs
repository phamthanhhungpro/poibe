using Microsoft.AspNetCore.Mvc;
using Poi.Id.InfraModel.DataAccess;

namespace Poi.Id.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
             
        }
        // GET api/user
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement logic to retrieve all users
            return Ok();
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // TODO: Implement logic to retrieve user by id
            return Ok();
        }

        // POST api/user
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            // TODO: Implement logic to create a new user
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            // TODO: Implement logic to update user by id
            return NoContent();
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // TODO: Implement logic to delete user by id
            return NoContent();
        }
    }
}
