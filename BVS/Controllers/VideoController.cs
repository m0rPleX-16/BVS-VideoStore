using Microsoft.AspNetCore.Mvc;
using BVS.Data;
using BVS.Models;

public class VideoController : Controller
{
    private readonly ApplicationDbContext _context;

    public VideoController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Videos.ToList());
    }

    //ADD
    public IActionResult Create()
    {
        ViewBag.Categories = Enum.GetValues(typeof(CategoryType));
        return View();
    }

    [HttpPost]
    public IActionResult Create(Video v)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = Enum.GetValues(typeof(CategoryType));
            return View(v);
        }

        if (v.AvailableQuantity > v.TotalQuantity)
        {
            ModelState.AddModelError(nameof(v.AvailableQuantity), "Available quantity cannot exceed total quantity.");
            ViewBag.Categories = Enum.GetValues(typeof(CategoryType));
            return View(v);
        }

        // If AvailableQuantity not set, initialize to TotalQuantity
        if (v.AvailableQuantity == 0)
        {
            v.AvailableQuantity = v.TotalQuantity;
        }

        _context.Videos.Add(v);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    //EDIT
    public IActionResult Edit(int id)
    {
        var video = _context.Videos.Find(id);
        ViewBag.Categories = Enum.GetValues(typeof(CategoryType));
        return View(video);
    }

    [HttpPost]
    public IActionResult Edit(Video v)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = Enum.GetValues(typeof(CategoryType));
            return View(v);
        }

        if (v.AvailableQuantity > v.TotalQuantity)
        {
            ModelState.AddModelError(nameof(v.AvailableQuantity), "Available quantity cannot exceed total quantity.");
            ViewBag.Categories = Enum.GetValues(typeof(CategoryType));
            return View(v);
        }

        _context.Videos.Update(v);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    //DELETE
    public IActionResult Delete(int id)
    {
        var video = _context.Videos.Find(id);
        _context.Videos.Remove(video);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Report()
    {
        var data = _context.Videos
            .OrderBy(v => v.Title)
            .ToList();

        return View(data);
    }
}