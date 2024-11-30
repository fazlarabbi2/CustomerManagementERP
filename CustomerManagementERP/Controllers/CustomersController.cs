using CustomerManagementERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementERP.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var customers = await _context.customers
                    .Include(c => c.DeliveryAddresses)
                    .AsNoTracking()
                    .ToListAsync();
                return View(customers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while loading the customers: {ex.Message}";
                return View("Error");
            }
        }

        //Get: Customers/Details/{{id}}
        //[HttpGet("id")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Please select a valid customer.";
                return View(NotFound());
            }

            try
            {
                var book = await _context.customers
                    .FirstOrDefaultAsync(c => c.CustomerId == id);

                if (book == null)
                {
                    TempData["ErrorMessage"] = @"No Customer Available in this ${id}";
                    return View(StatusCode(404));
                }

                return View(book);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while loading the customers: {ex.Message}";
                return View("Error");
            }
        }

        // Post: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Customer customer)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Please select a valid customer.";
                return View(NotFound());
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existCustomer = await _context.customers.FindAsync(id);

                    if (existCustomer == null)
                    {
                        TempData["ErrorMessage"] = @"No Book found ${id} for updating";
                        return View("Error");
                    }

                    // Updating fields that can be edited
                    existCustomer.Address = customer.Address;
                    existCustomer.BuisenesStartDate = customer.BuisenesStartDate;
                    existCustomer.CreditLimit = customer.CreditLimit;

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = @"Succeffully updated the book: ${customer.Name}";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred while loading the customers: {ex.Message}";
                    return View("Error");
                }
            }

            return View();
        }

        // Get: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Customer Id is not provided";
                return View(StatusCode(404));
            }

            try
            {
                var cus = await _context.customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);

                if (cus == null)
                {
                    TempData["ErrorMessage"] = $"No Customer found with ID {id} for deletion.";
                    return View("NotFound");

                }

                return View(cus);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while loading the customers: {ex.Message}";
                return View("Error");
            }
        }


        // Post: customers/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cus = await _context.customers.FindAsync(id);

                if (cus == null)
                {
                    TempData["ErrorMessage"] = "Customer Id is not provided";
                    return View(StatusCode(404));
                }

                _context.Remove(cus);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = @"Successfully deleted the book: ${cus.Name}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while loading the customers: {ex.Message}";
                return View("Error");
            }
        }


        private bool CustomerExist(int id)
        {
            return _context.customers.Any(e => e.CustomerId == id);
        }
    }
}












































