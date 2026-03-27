using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BVS.Data;
using BVS.Models;

public class RentalController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var rentals = await _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Video)
            .ToListAsync();

        ViewBag.Customers = await _context.Customers.ToListAsync();
        ViewBag.Videos = await _context.Videos.ToListAsync();

        return View(rentals);
    }

    [HttpPost]
    public async Task<IActionResult> Rent(int customerId, int videoId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
        {
            TempData["Error"] = "Customer not found!";
            return RedirectToAction("Index");
        }

        var video = await _context.Videos.FindAsync(videoId);

        if (video == null)
        {
            TempData["Error"] = "Video not found!";
            return RedirectToAction("Index");
        }

        if (video.AvailableQuantity <= 0)
        {
            TempData["Error"] = "No stock available!";
            return RedirectToAction("Index");
        }

        // Ensure rental days are within allowed range (1..3)
        var days = Math.Clamp(video.RentalDays, 1, 3);

        var rental = new Rental
        {
            CustomerId = customerId,
            VideoId = videoId,
            RentedDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(days),
            Status = "Rented",
            // set price based on category (VCD = 25, DVD = 50)
            Price = video.Category == CategoryType.VCD ? 25m : 50m
        };

        video.AvailableQuantity--;

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Return(int id)
    {
        var rental = await _context.Rentals
            .Include(r => r.Video)
            .FirstOrDefaultAsync(r => r.RentalId == id);

        if (rental == null) return NotFound();

        var today = DateTime.Now;

        if (rental.DueDate != default && today > rental.DueDate)
        {
            int overdue = (today - rental.DueDate).Days;
            rental.Penalty = overdue * 5;
        }

        rental.Status = "Returned";
        rental.ReturnDate = today;

        if (rental.Video != null)
        {
            rental.Video.AvailableQuantity++;
        }
        else
        {
            TempData["Error"] = "Returned rental has no associated video.";
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    // Report: list of videos with In/Out counts ordered alphabetically
    public async Task<IActionResult> VideosReport()
    {
        var data = await _context.Videos
            .OrderBy(v => v.Title)
            .ToListAsync();

        // Use existing view that expects List<Video>
        return View("~/Views/Video/Report.cshtml", data);
    }

    // Report: customer and videos they are currently renting
    public async Task<IActionResult> CustomerRentalsReport()
    {
        var customers = await _context.Customers
            .Select(c => new BVS.Models.CustomerRentalsViewModel
            {
                CustomerId = c.CustomerId,
                CustomerName = c.FullName,
                CurrentRentals = _context.Rentals
                    .Where(r => r.CustomerId == c.CustomerId && r.Status == "Rented")
                    .Select(r => r.Video.Title)
                    .ToList()
            })
            .ToListAsync();

        return View(customers);
    }

    // Simple CSV export for Videos
    public async Task<FileResult> VideosReportCsv()
    {
        var data = await _context.Videos.OrderBy(v => v.Title).ToListAsync();
        var csv = "Title,Category,In,Out\n";
        foreach (var v in data)
        {
            var outCount = v.TotalQuantity - v.AvailableQuantity;
            csv += $"\"{v.Title}\",{v.Category},{v.AvailableQuantity},{outCount}\n";
        }
        var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
        return File(bytes, "text/csv", "videos_report.csv");
    }

    // Simple CSV export for Customer Rentals
    public async Task<FileResult> CustomerRentalsReportCsv()
    {
        var customers = await _context.Customers.ToListAsync();
        var lines = new List<string> { "Customer,CurrentRentals" };
        foreach (var c in customers)
        {
            var rentals = _context.Rentals.Where(r => r.CustomerId == c.CustomerId && r.Status == "Rented").Select(r => r.Video.Title).ToList();
            var joined = string.Join(";", rentals.Select(t => t.Replace(',', ' '))); // avoid commas
            lines.Add($"\"{c.FullName}\",\"{joined}\"");
        }
        var csv = string.Join("\n", lines);
        var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
        return File(bytes, "text/csv", "customer_rentals.csv");
    }
}
