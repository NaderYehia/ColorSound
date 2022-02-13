using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grad_Proj.Entites;
using Grad_Proj.Infrastructure;

namespace Grad_Proj.Controllers
{
    public class AuthorToAuthorsController : Controller
    {
        private readonly ThemesShopDbContext _context;

        public AuthorToAuthorsController(ThemesShopDbContext context)
        {
            _context = context;
        }

        // GET: AuthorToAuthors
        public async Task<IActionResult> Index()
        {
            var themesShopDbContext = _context.AuthorToAuthor.Include(a => a.Author).Include(a => a.Follower);
            return View(await themesShopDbContext.ToListAsync());
        }

        // GET: AuthorToAuthors/Details/5
        public async Task<IActionResult> Details(int? followerId,int? authorId)
        {
            if (followerId == null || authorId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var authorToAuthor = await _context.AuthorToAuthor
                .Include(a => a.Author)
                .Include(a => a.Follower)
                .FirstOrDefaultAsync(m => m.AuthorId == authorId && m.FollowerId==followerId);
            if (authorToAuthor == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(authorToAuthor);
        }

        // GET: AuthorToAuthors/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id");
            ViewData["FollowerId"] = new SelectList(_context.Authors, "Id", "Id");
            return View();
        }

        // POST: AuthorToAuthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,FollowerId,Since")] AuthorToAuthor authorToAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorToAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorToAuthor.AuthorId);
            ViewData["FollowerId"] = new SelectList(_context.Authors, "Id", "Id", authorToAuthor.FollowerId);
            return View(authorToAuthor);
        }

        // GET: AuthorToAuthors/Edit/5
        public async Task<IActionResult> Edit(int? followerId, int? authorId)
        {
            if (followerId == null || authorId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var authorToAuthor = await _context.AuthorToAuthor.Where(m => m.AuthorId == authorId && m.FollowerId == followerId).FirstOrDefaultAsync();
            if (authorToAuthor == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorToAuthor.AuthorId);
            ViewData["FollowerId"] = new SelectList(_context.Authors, "Id", "Id", authorToAuthor.FollowerId);
            return View(authorToAuthor);
        }

        // POST: AuthorToAuthors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("AuthorId,FollowerId,Since")] AuthorToAuthor authorToAuthor)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorToAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorToAuthorExists(authorToAuthor.AuthorId))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", authorToAuthor.AuthorId);
            ViewData["FollowerId"] = new SelectList(_context.Authors, "Id", "Id", authorToAuthor.FollowerId);
            return View(authorToAuthor);
        }

        // GET: AuthorToAuthors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var authorToAuthor = await _context.AuthorToAuthor
                .Include(a => a.Author)
                .Include(a => a.Follower)
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (authorToAuthor == null)
            {
                return NotFound();
            }

            return View(authorToAuthor);
        }

        // POST: AuthorToAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var authorToAuthor = await _context.AuthorToAuthor.FindAsync(id);
            _context.AuthorToAuthor.Remove(authorToAuthor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorToAuthorExists(int? id)
        {
            return _context.AuthorToAuthor.Any(e => e.AuthorId == id);
        }
    }
}
