using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Kitchen")]
    public class KitchenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitchenController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> FoodMenu()
        {
            var database = _context.Foods.Include(f => f.Category);
            //ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(await database.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> FoodMenu(string value1)
        {
            var selected = _context.Categories.FirstOrDefault(c => c.CategoryId.ToString() == value1);
            ViewBag.SelectedCategory = selected.CategoryName;

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", value1);
            //List<SelectListItem> dropdownlist = _context.Categories.ToList();
            //var selected = _context.Categories.Where(c=>c.CategoryId.ToString() == value1);
            //ViewBag.Categories = dropdownlist.Items.FindByText(selected).Selected = true;
            if (!string.IsNullOrEmpty(value1))
            {
                var t = _context.Foods.Where(i=>i.CategoryId.ToString() == value1).Include(f=>f.Category);
                return View(await t.ToListAsync());

            }
            return View(await _context.Foods.Include(f => f.Category).ToListAsync());

        }

        [HttpPost]
        public async Task<List<Message>> KitOrderStatus()
        {
            var message = await _context.Messages.ToListAsync();
            //foreach (Message m in message)
            //{
            //    if (m.Items != null)
            //    {
            //        string []items = m.Items.Split(',');
            //        foreach (var item in items)
            //        {
            //            var i = item;
            //            int count = 0;
            //            string t = "";
            //            foreach (var k in items)
            //            {
            //                if (k == i)
            //                {
            //                    count++;
            //                    t = k;
            //                }
            //            }


            //        }
            //    }
            //}
            return message;
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SetOrderStatus(int id, string status)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            message.Status = status;

            //if (id != message.MessageId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!MessageExists(message.MessageId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            //return View(message);
            return null;
        }

        [HttpPost]
        // GET: Foods/Details/5
        public async Task<IActionResult> FoodDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            //return View(food);
            return Ok(food);
        }

        // GET: Events
        public async Task<IActionResult> KitEvents()
        {
            return View(await _context.Events.ToListAsync());
        }

    }
}
