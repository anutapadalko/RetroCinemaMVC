using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroCinemaDomain.Model;
using RetroCinemaInfrastructure;

namespace RetroCinemaInfrastructure.Controllers
{
    public class SessionsController : Controller
    {
        private readonly RetroCinemaDbContext _context;

        public SessionsController(RetroCinemaDbContext context)
        {
            _context = context;
        }

        // GET: Sessions
        public async Task<IActionResult> Index(int? id, string? name)
        {
            
            if (id == null) return RedirectToAction("Index", "Movies");

            ViewBag.MovieId = id;
            ViewBag.MovieName = name;

            var sessionsByMovie = _context.Sessions
                .Where(s => s.MovieId == id)
                .Include(s => s.Hall)
                .Include(s => s.Movie);

            return View(await sessionsByMovie.ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create(int id)
        {
            ViewBag.MovieId = id;
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            ViewBag.MovieName = movie?.Title;

            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Name");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,StartTime,BasePrice,HallId,IsDeleted,CreatedAt,UpdatedAt")] Session session)
        {
            session.Id = 0;
            var movie = _context.Movies.FirstOrDefault(m => m.Id == session.MovieId);
            session.Movie = movie;

            var hall = _context.Halls.FirstOrDefault(h => h.Id == session.HallId);
            session.Hall = hall;

            ModelState.Clear();
            TryValidateModel(session);

            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Sessions", new { id = session.MovieId, name = movie?.Title });
            }

            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Name", session.HallId);
            return View(session);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Name", session.HallId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", session.MovieId);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,HallId,StartTime,BasePrice,CreatedAt,UpdatedAt,Id")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
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
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Name", session.HallId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", session.MovieId);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
