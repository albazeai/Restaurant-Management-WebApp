using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Title,Description,StartDate,ReservationRequired")] Event @event, List<IFormFile> EventImage)
        {
            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Today;  // get current date
                DateTime newResDate = @event.StartDate.Date;
                int checkDate = DateTime.Compare(today, newResDate);
                if (checkDate <= 0)  // if current date less then or equle the today's date:
                {
                    // Read and save the uploaded image
                    foreach (var item in EventImage)
                    {
                        if (item.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await item.CopyToAsync(stream);
                                @event.EventImage = stream.ToArray();
                            }
                        }
                    }
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Date! You have entered Old Date.";
                }

            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Title,Description,StartDate,ReservationRequired")] Event @event, List<IFormFile> EventImage)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime today = DateTime.Today;  // get current date
                    DateTime newResDate = @event.StartDate.Date;
                    int checkDate = DateTime.Compare(today, newResDate);
                    if (checkDate <= 0)  // if current date less then or equle the today's date:
                    {
                        // Read and save the uploaded image
                        if (EventImage.Count() > 0)
                        {
                            foreach (var item in EventImage)
                            {
                                if (item.Length > 0)
                                {
                                    using (var stream = new MemoryStream())
                                    {
                                        await item.CopyToAsync(stream);
                                        @event.EventImage = stream.ToArray();
                                    }
                                }
                            }
                        }
                        else
                        {
                            var eventImage = await _context.Events.FindAsync(id);
                            @event.EventImage = eventImage.EventImage;
                        }

                        var event2 = _context.Events.First(e=>e.EventId == @event.EventId);
                        _context.Entry(event2).CurrentValues.SetValues(@event);
                        await _context.SaveChangesAsync();

                        //_context.Update(@event);
                        //await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Date! You have entered Old Date.";
                        return View(@event);
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        //throw;
                    }
                }

            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
