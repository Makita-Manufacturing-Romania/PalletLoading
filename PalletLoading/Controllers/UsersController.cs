using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PalletLoading.Data;
using PalletLoading.Models;

namespace PalletLoading.Controllers
{
    public class UsersController : MainController
    {

        public UsersController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var palletLoadingContext = _context.User.Include(u => u.UserRight).Include(u => u.Role);
            return View(await palletLoadingContext.ToListAsync());
        }
        public IActionResult UserSearch(string user)
        {

            List<User> ob = new List<User>();

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "UsersSearch";


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar) { Value = user });

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.CommandTimeout = 120;


                using (SqlDataReader rdr = (SqlDataReader)cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        User temp = new User();
                        temp.FullName = rdr["name"].ToString();
                        temp.Username = rdr["userprincipalname"].ToString();

                        ob.Add(temp);
                    }
                }
            }

            return Json(ob);
        }
        public IActionResult CheckUsersExist(string username)
        {
            var checkUser = _context.User.Any(c => c.Username.Equals(username));

            return Json(new { res = checkUser });
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.UserRight)
                .Include(u => u.RoleId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["UserRightId"] = new SelectList(_context.UserRight, "Id", "Right");
            ViewData["UserRole"] = new SelectList(_context.Role, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,FullName,Usermail,UserRightId,UserRole,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {    
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserRightId"] = new SelectList(_context.UserRight, "Id", "Id", user.UserRightId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            string username = this._context.User.Where(c => c.Username.Equals(User.Identity.Name.Replace("MMRMAKITA\\", ""))).Select(c => c.Username).FirstOrDefault();
            int userRole = _context.User
                .Where(l => l.Username == username)
                .Select(l => Convert.ToInt32(l.Role.accessLevel))
                .FirstOrDefault();

            ViewData["UserRightId"] = new SelectList(_context.UserRight, "Id", "Right", user.UserRightId);
            ViewData["UserRole"] = new SelectList(_context.Role.Where(l => l.accessLevel <= userRole), "Id", "Name");
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,FullName,Usermail,UserRightId,UserRole,RoleId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["UserRightId"] = new SelectList(_context.UserRight, "Id", "Id", user.UserRightId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.UserRight)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
