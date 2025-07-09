using BulletinBoard.BusinessLogic.Services.Interfaces;
using BulletinBoard.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Controllers;

[Route("[controller]")]
public class AnnouncementsController : Controller
{
    private readonly IAnnouncementService _service;
    public AnnouncementsController(IAnnouncementService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _service.GetAll();
        return View(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var item = await _service.GetById(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        ViewBag.CategoryList = new SelectList(new[]
        {
        "Household appliances",
        "Computer technology",
        "Smartphones",
        "Other"
    });
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(Announcement model)
    {
        ViewBag.CategoryList = new SelectList(new[]
        {
            "Household appliances",
            "Computer technology",
            "Smartphones",
            "Other"
        });

        if (!ModelState.IsValid) return View(model);
        await _service.Create(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var item = await _service.GetById(id);
        if (item == null) return NotFound();

        var categories = new[]
        {
            "Household appliances",
            "Computer technology",
            "Smartphones",
            "Other"
        };

        ViewBag.CategoryList = new SelectList(categories, item.Category);

        return View(item);
    }

    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(Announcement model)
    {
        var categories = new[]
        {
            "Household appliances",
            "Computer technology",
            "Smartphones",
            "Other"
        };

        ViewBag.CategoryList = new SelectList(categories, model.Category);

        if (!ModelState.IsValid)
            return View(model);

        await _service.Update(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
