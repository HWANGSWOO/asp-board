using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp.DataContext;
using asp.Models;
using Microsoft.AspNetCore.Http;

namespace asp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NoteDbcontext _context;

        public NoteController(NoteDbcontext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var noteDbcontext = _context.Notes.Include(n => n.User);
            return View(await noteDbcontext.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NoteNo == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            ViewData["UserNo"] = new SelectList(_context.Users, "UserNo", "UserId");
            return View();
        }

        // POST: Notes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteNo,NoteTitle,NoteContents,UserNo")] Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserNo"] = new SelectList(_context.Users, "UserNo", "UserId", note.UserNo);
            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            ViewData["UserNo"] = new SelectList(_context.Users, "UserNo", "UserId", note.UserNo);
            return View(note);
        }

        // POST: Notes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteNo,NoteTitle,NoteContents,UserNo")] Note note)
        {
            if (id != note.NoteNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.NoteNo))
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
            ViewData["UserNo"] = new SelectList(_context.Users, "UserNo", "UserId", note.UserNo);
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NoteNo == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.NoteNo == id);
        }
    }
}
