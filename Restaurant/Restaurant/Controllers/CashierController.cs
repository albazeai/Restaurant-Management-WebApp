using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Cashier")]
    public class CashierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashierController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewModel views = new ViewModel();
            views.Tables = await _context.Tables.Where(t => t.TableItems != null).ToListAsync();
            //var tables = await _context.Tables.Where(t => t.TableItems != null).ToListAsync();


            return View(views);
        }
        public async Task<IActionResult> AddOrderAsync()
        {
            ViewBag.DrinkItems = await _context.Drinks.Include(d => d.Category).ToListAsync();
            ViewModel views = new ViewModel();
            views.Drinks = await _context.Drinks.Include(d => d.Category).ToListAsync();
            views.Foods = await _context.Foods.Include(d => d.Category).ToListAsync();
            views.Tables = await _context.Tables.ToListAsync();

            return View(views);

            //var database = _context.Drinks.Include(d => d.Category);
            //return View(await database.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(Table table)
        {

            // add order to table items 

            //if (id != table.TableId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    //var seelctedTable = await _context.Tables.FindAsync(table.TableId);
                    //if (seelctedTable == null)
                    //{
                    //    return NotFound();
                    //}
                    _context.Update(table);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return null;
        }

        public async Task<IActionResult> GetUsedTables()
        {
            var usedTables = await _context.Tables.Where(t => t.TableItems != "").ToListAsync();
            // context.Students.Where(s => s.FirstName == GetName()).ToList();
            return View(usedTables);
            //return View();

        }

        [HttpPost]
        public async Task<Table> TableDetails(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var table = await _context.Tables
                .FirstOrDefaultAsync(m => m.TableId == id);
            //table.TableItems = null;
            //table.Total = 0;
            //_context.Update(table);
            //await _context.SaveChangesAsync();
            //if (table == null)
            //{
            //    return NotFound();
            //}

            //return View(table);
            return table;
        }


        [HttpPost]
        public async Task<Table> ClearTable(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var table = await _context.Tables
                 .FirstOrDefaultAsync(m => m.TableId == id);
            table.TableItems = null;
            table.Total = 0;
            _context.Update(table);
            await _context.SaveChangesAsync();
            //if (table == null)
            //{
            //    return NotFound();
            //}

            //return View(table);
            return null;
        }

        /// <summary>
        /// Add message when the order is contain a food item to be shared with the kitchen. 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Boolean> AddMessage(Message message)
        {

            if (ModelState.IsValid)
            {
                string items = message.Items;
                string name = "";
                string foodItems = "";
                foreach (char c in items)
                {
                    bool isLetter = Char.IsLetter(c);
                    if (isLetter)
                    {
                        name += c;
                    }
                    else if (c == '-')
                    {
                        name += c;
                    }
                    else if (c == ',')
                    {
                        var item = _context.Foods.FirstOrDefault(x => x.FoodName == name.Trim());
                        if (item != null)
                        {
                            foodItems += name + ", ";
                        }
                        name = "";
                    }

                }
                message.Items = foodItems;
                _context.Add(message);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // GET: Messages
        public async Task<IActionResult> CashOrderStatus()
        {
            return View(await _context.Messages.Where(x => x.Status != "Picked Up").ToListAsync());
        }


        [HttpPost]
        public IActionResult GetMessages()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var messages = _context.Messages.ToList();

            //table.TableItems = null;
            //table.Total = 0;
            //_context.Update(table);
            //await _context.SaveChangesAsync();
            //if (table == null)
            //{
            //    return NotFound();
            //}

            //return View(table);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMessage(int? id)
        {
            if (id != null)
            {
                var message = await _context.Messages.FindAsync(id);
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return null;
        }

        // GET: Events
        public async Task<IActionResult> CashEvents()
        {
            return View(await _context.Events.ToListAsync());
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

        /******************************************         Important Need to be Modified for the 'Total' accuracy ****************************/
        // note : process the total of each item separatly! ex: Water $1.00 
        [HttpPost]
        public async Task<IActionResult> SeparatePays( int id, string paidItems, double total)
        {
            if (id == 0 || paidItems == null)
            {
                return NotFound();
            }
            try
            {
                var table = _context.Tables.FirstOrDefault(x => x.TableId == id && x.TableItems.Trim() != null);
                var tableTotal = table.Total;
                if (table != null)
                {
                    string[] currentItems = paidItems.Split(',');
                    string[] existItems = table.TableItems.Split(',');

                    for (int i = 0; i < currentItems.Length; i++)
                    {
                        string item = currentItems[i].Trim();
                        //var exist = _context.Tables.FirstOrDefault(x => x. == id && x.TableItems.Trim() != null);
                        for (int c = 0; c < existItems.Length; c++)
                        {
                            if (existItems[c] != null && existItems[c].Trim().Equals(item))
                            {

                                // clear
                                string k = existItems[c].Trim();
                                existItems[c] = null;
                                currentItems[i] = null;
                                item = "";
                                string val = "";
                                foreach (char ch in k)
                                {
                                    if (char.IsDigit(ch) || ch.Equals('.'))
                                    {
                                        val += ch;
                                    }
                                }

                                if (double.TryParse(val, out double price))
                                {
                                    tableTotal -= price;
                                }


                            }
                        }
                    }

                    string updateItems = "";
                    foreach (var item in existItems)
                    {
                        if (item != null)
                        {
                            updateItems += item + ',';
                        }
                    }


                    try
                    {
                        table.Total = tableTotal;
                        table.TableItems = updateItems;
                        if (table.Total <= 0 || table.TableItems.Trim() == "")
                        {
                            table.Total = 0;
                            table.TableItems = null;
                        }
                        _context.Update(table);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception)
            {

            }

            ViewBag.ErrorMessage = "The Process Cannot be completed! Please Try Again.";
            return RedirectToAction(nameof(Index));
        }
    }
}
