using Microsoft.AspNetCore.Mvc;
using ResumeApp.Application.DTOs;
using ResumeApp.Application.Interfaces;

namespace ResumeApp.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DegreesController : ControllerBase
    {
        private readonly IDegreeService _degreeService;

        public DegreesController(IDegreeService degreeService)
        {
            _degreeService = degreeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var degrees = await _degreeService.GetAllAsync();
            return Ok(degrees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var degree = await _degreeService.GetByIdAsync(id);
            if (degree == null) return NotFound();
            return Ok(degree);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DegreeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _degreeService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DegreeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _degreeService.UpdateAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _degreeService.DeleteIfUnusedAsync(id);
            if (!success)
                return BadRequest("Cannot delete degree because it is associated with one or more candidates.");

            return NoContent();
        }
    }
}
