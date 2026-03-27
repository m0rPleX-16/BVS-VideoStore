using Microsoft.AspNetCore.Mvc;
using BVS.Models;
using BVS.Data;

    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context) => _context = context;
        public IActionResult Index()
        {
            return View(_context.Customers.ToList());
        }

        //ADD
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Customer c)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(c);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c); 
        }

        //EDIT
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer c)
        {
            _context.Customers.Update(c);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE (confirmation)
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Individual customer report
        public IActionResult IndividualReport(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            var model = new BVS.Models.IndividualCustomerReportViewModel
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.FullName,
                Contact = customer.Contact,
                CurrentRentals = _context.Rentals
                    .Where(r => r.CustomerId == id && r.Status == "Rented")
                    .Select(r => r.Video.Title)
                    .ToList(),
                PastRentals = _context.Rentals
                    .Where(r => r.CustomerId == id && r.Status == "Returned")
                    .Select(r => r.Video.Title)
                    .ToList()
            };

            return View(model);
        }
    }

