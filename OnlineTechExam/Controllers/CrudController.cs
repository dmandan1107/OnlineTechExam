using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineTechExamAPI.Model;

namespace OnlineTechExamAPI.Controllers
{
    [ApiController]
    [Route("api/crud")]
    public class CrudController : ControllerBase
    {
        private readonly IUserProfileRepository _repository;

        public CrudController(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetAsync()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<UserProfile>> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserProfile>> CreateAsync([FromBody] UserProfile user)
        {
            if (user == null) return BadRequest();

            var result = await _repository.CreateAsync(user);

            return result;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserProfile user)
        {
            if (user == null) return BadRequest();

            var result = await _repository.UpdateAsync(user);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            bool deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
