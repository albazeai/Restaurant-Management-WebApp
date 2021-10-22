﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{

    [Authorize(Roles = "Cashier, Admin")]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _context.Reservations.Include(e => e.Event).Where(e => e.Event.ReservationRequired == true).ToListAsync();
            return View(applicationDbContext);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            var reservation = from r in _context.Reservations.Include(e => e.Event)
                              select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                reservation = reservation.Where(r => r.ConfirmationName.ToUpper().Contains(searchString.ToUpper())
                                       || r.ConfirmationPhone.Trim().Contains(searchString.Trim()));
            }
            return View(await reservation.Include(e => e.Event).OrderBy(e => e.Event.StartDate).ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events.Where(e=>e.ReservationRequired == true), "EventId", "Title");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,People,Attendance,ConfirmationName,ConfirmationPhone,EventId")] Reservation reservation)
        {

            
            if (ModelState.IsValid)
            {
                if (reservation.EventId.ToString() != "")
                {

                    var reserved = await _context.Reservations.Where(e=>e.EventId == reservation.EventId).ToListAsync();  // get all the reservation
                    var tables = await _context.Tables.ToListAsync();      // get the restuarant tables

                    // if the total reservations is less then our restaurant tables, then add the new reservation request.
                    if (reserved.Count < tables.Count)
                    {
                        reservation.Id = Guid.NewGuid();
                        _context.Add(reservation);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index)); ;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Sorry, We are Fully Booked!.";
                    }

                }
                else
                {
                    ViewData["EventId"] = new SelectList(_context.Events.Where(e => e.ReservationRequired == true), "EventId", "Title", reservation.EventId);
                    ViewBag.ErrorMessage = "No Events! Cannot add any reservation at this time.";
                    return View(reservation);
                }
               
            }
            ViewData["EventId"] = new SelectList(_context.Events.Where(e => e.ReservationRequired == true), "EventId", "Title", reservation.EventId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events.Where(e => e.ReservationRequired == true), "EventId", "Title", reservation.EventId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,People,Attendance,ConfirmationName,ConfirmationPhone,EventId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (reservation.EventId.ToString() != null)
                    {
                        _context.Update(reservation);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["EventId"] = new SelectList(_context.Events.Where(e => e.ReservationRequired == true), "EventId", "Title", reservation.EventId);
                        ViewBag.ErrorMessage = "No Events! Cannot add any reservation at this time.";
                        return View(reservation);
                    }
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            ViewData["EventId"] = new SelectList(_context.Events.Where(e => e.ReservationRequired == true), "EventId", "Title", reservation.EventId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

    }
}