using BulletinBoard.BusinessLogic.Services.Interfaces;
using BulletinBoard.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _service;
        public AnnouncementsController(IAnnouncementService service) =>
            _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAll(
            [FromQuery] string? category,
            [FromQuery] string? subCategory)
        {
            var items = (await _service.GetAll()).ToList();

            if (!string.IsNullOrEmpty(category))
                items = items.Where(a => a.Category == category).ToList();
            if (!string.IsNullOrEmpty(subCategory))
                items = items.Where(a => a.SubCategory == subCategory).ToList();

            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Announcement>> GetById(Guid id)
        {
            var item = await _service.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Announcement>> Create([FromBody] Announcement model)
        {
            await _service.Create(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.Id },
                model
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Announcement model)
        {
            if (id != model.Id)
                return BadRequest("ID in URL does not match ID in body.");

            var existing = await _service.GetById(id);
            if (existing == null)
                return NotFound();

            await _service.Update(model);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _service.GetById(id);
            if (existing == null)
                return NotFound();

            await _service.Delete(id);
            return NoContent();
        }
    }
}
