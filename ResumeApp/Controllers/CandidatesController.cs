using Microsoft.AspNetCore.Mvc;
using ResumeApp.Application.DTOs;
using ResumeApp.Application.Interfaces;

namespace ResumeApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _candidateService.GetAllAsync();
            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var candidate = await _candidateService.GetByIdAsync(id);
            if (candidate == null) return NotFound();
            return Ok(candidate);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CandidateDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var created = await _candidateService.CreateAsync(dto);
        //    return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CandidateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _candidateService.UpdateAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _candidateService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWithFile()
        {
            var form = await Request.ReadFormAsync();

            var firstName = form["firstName"];
            var lastName = form["lastName"];
            var email = form["email"];
            var mobile = form["mobile"];
            var degreeId = int.TryParse(form["degreeId"], out var parsedId) ? parsedId : (int?)null;

            var file = form.Files.GetFile("cvFile");
            byte[]? fileBytes = null;

            if (file != null && file.Length > 0)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
                return BadRequest("FirstName, LastName, and Email are required.");

            var dto = new CandidateDto
            {
                FirstName = firstName!,
                LastName = lastName!,
                Email = email!,
                Mobile = mobile,
                DegreeId = degreeId,
                CV = fileBytes
            };

            var result = await _candidateService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}
