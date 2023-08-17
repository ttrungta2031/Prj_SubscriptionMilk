using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionMilk.Models;
using SubscriptionMilk.Services;

namespace SubscriptionMilk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SubcriptionMilkContext _context;

        private readonly IGetallService _service;
        public AccountsController(SubcriptionMilkContext context, IGetallService service)
        {
            _context = context;
            _service = service;
        }
        // GET: api/Accounts
        [HttpGet]
        public IActionResult GetAllList(string search)
        {
            try
            {
                var result = _service.GetAllUser(search);
                var totalitems = result.Count();

                return Ok(new { StatusCode = 200, Message = "Load successful", data = result, totalitems });
            }
            catch (Exception e)
            {

                return StatusCode(409, new { StatusCode = 409, Message = e.Message });

            }
        }

        // GET: api/Accounts/5
        [HttpGet("getbyid")]
        public async Task<ActionResult> Getaccount(int id)
        {
            var all = _context.Accounts.AsQueryable();

            all = _context.Accounts.Where(us => us.Id.Equals(id));
            var result = all.ToList();

            return Ok(new { StatusCode = 200, Message = "Load successful", data = result });
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutAccount(Account acc)
        {

            try
            {
                var account = await _context.Accounts.FindAsync(acc.Id);
                if (account == null)
                {
                    return NotFound();
                }
                account.Email = acc.Email;
                account.Password = acc.Password;
                account.Fullname = acc.Fullname;
                account.Phone = acc.Phone;
                account.Gender = acc.Gender;
                account.Address = acc.Address;
                account.StationId = acc.StationId;
                account.Avatar = acc.Avatar;
                account.IsAdmin = acc.IsAdmin;


                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                return Ok(new { StatusCode = 201, Message = "Update Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Account>> PostAccount(Account acc)
        {
            try
            {
                var account = new Account();
                {
                    account.Id = acc.Id;
                    account.Email = acc.Email;
                    account.Password = acc.Password;
                    account.Fullname = acc.Fullname;
                    account.Phone = acc.Phone;
                    account.Gender = acc.Gender;
                    account.Address = acc.Address;
                    account.StationId = acc.StationId;
                    account.Avatar = acc.Avatar;
                    account.IsAdmin = acc.IsAdmin;
                }
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();


                return Ok(new { StatusCode = 201, Message = "Add Successfull" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return StatusCode(409, new { StatusCode = 409, Message = e.Message });
            }
        }


        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccounts(int id)
        {
            var acc = await _context.Accounts.FindAsync(id);
            if (acc == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(acc);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Content = "The Accounts was deleted successfully!!" });
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
