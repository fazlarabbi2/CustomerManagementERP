using CustomerManagementERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementERP.Controllers
{
    public class CustomerDeliveryAddressesController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerDeliveryAddressesController(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var deliveryAddress = await _context.deliveryAddresses
                .Include(c => c.Customer)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = await _context.deliveryAddresses.CountAsync();

            return View(deliveryAddress);
        }

        //Create: Get- Display the form
        public async Task<IActionResult> Create()
        {
            ViewBag.customers = await _context.customers.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,DeliveryAddress,ContactPerson,ContactPhone")] CustomerDeliveryAddress deliveryAddress)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(deliveryAddress);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.customers = await _context.customers.ToListAsync();
            return View(deliveryAddress);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAddress = await _context.customers.FindAsync(id);

            if (deliveryAddress == null)
            {
                return NotFound();
            }

            ViewBag.customers = await _context.customers.ToListAsync();
            return View(deliveryAddress);
        }

        private bool DeliveryAddressExists(int id)
        {
            return _context.deliveryAddresses.Any(e => e.CustomerDeliveryAddressId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("CustomerDeliveryAddressId,CustomerId,DeliveryAddress,ContactPerson,ContactPhone")] CustomerDeliveryAddress deliveryAddress)
        {
            if (id != deliveryAddress.CustomerDeliveryAddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryAddressExists(deliveryAddress.CustomerDeliveryAddressId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.customers = await _context.customers.ToListAsync();
            return View(deliveryAddress);
        }


        // DELETE: GET - Display confirmation page
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAddress = await _context.deliveryAddresses
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(
                m => m.CustomerDeliveryAddressId == id);

            if (deliveryAddress == null)
            {
                return NotFound();
            }

            return View(deliveryAddress);
        }


    }
}
