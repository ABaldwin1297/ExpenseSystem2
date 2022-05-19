using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseSystem2.Models;

namespace ExpenseSystem2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
          if (_context.Expenses == null)
          {
              return NotFound();
          }
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
          if (_context.Expenses == null)
          {
              return NotFound();
          }
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }


        [HttpGet("ApprovedExpenses")]
        public async Task<ActionResult<IEnumerable<Expense>>> ApprovedExpenses(Expense expense) {
            if (expense == null) {
                return NotFound();
            }
            return await _context.Expenses.Include(x => x.Desc)
                                          .Where(x => x.Status == "Approved")
                                          .ToListAsync();
           
        }


        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveExpense(int Id, Expense expense) {

            if(expense == null) {
                return NotFound();
            }
            var prevstatus = expense.Status;
            if (prevstatus == "Approved") {
                return Ok(); 
            }
            if (prevstatus != "Approved") {
                expense.Status = "Approved";
            }
            await _context.SaveChangesAsync();
            return await PutExpense(Id, expense);
        }



        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectExpense(int Id, Expense expense) {

            if (expense == null) {
                return NotFound();
            }
            var prevstatus = expense.Status;
            if (prevstatus == "Rejected") {
                return Ok();
            }
            if (prevstatus != "Rejected") {
                expense.Status = "Rejected";
            }
            await _context.SaveChangesAsync();
            return await PutExpense(Id, expense);
        }


        [HttpPut("Review/{id}")]
        public async Task<IActionResult> ReviewExpense(int id, Expense expense) {
            if(expense.Total <= 75) {
                expense.Status = "Approved";
            }
            if(expense.Total > 75) {
                expense.Status = "Review";
            }
            await _context.SaveChangesAsync();
            return await PutExpense(id, expense);
            
        }

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
          if (_context.Expenses == null)
          {
              return Problem("Entity set 'AppDbContext.Expenses'  is null.");
          }
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if (_context.Expenses == null)
            {
                return NotFound();
            }
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return (_context.Expenses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
