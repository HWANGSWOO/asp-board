using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp.DataContext;
using asp.Models;
using System.Net.Http;

namespace asp.Controllers
{
    public class NotesController : Controller
    {
        private readonly NoteDbcontext _context;

        public NotesController(NoteDbcontext context)
        {
            _context = context;
        }

        //GET: Notes
        public IActionResult Index(int page = 1)
        {
            var notes = from p in _context.Notes
                        select p;

            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1; //페이지를 기본값으로 설정
            }
            int limit = 5;
            int start = (int)(page - 1) * limit;
            int totalProduct = notes.Count();
            ViewBag.totalProduct = totalProduct;
            ViewBag.pageCurrent = page;
            float numberPage = (float)totalProduct / limit;
            ViewBag.numberPage = (int)Math.Ceiling(numberPage);


            var dataProduct = notes.OrderByDescending(p => p.NoteTitle).Skip(start).Take(limit);

            return View(dataProduct.ToList());

        }

        //public async Task<IActionResult> Index()
        //{

        //    var noteDbcontext = _context.Notes.Include(n => n.User);
        //    return View(await noteDbcontext.ToListAsync());
        //}

        public JsonResult PageTest(int page)
        {
            var notes = from p in _context.Notes
                        select p;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1; //페이지를 기본값으로 설정
            }

            int limit = 5;
            int start = (int)(page - 1) * limit;
            int totalProduct = notes.Count();
            ViewBag.totalProduct = totalProduct;
            ViewBag.pageCurrent = page;
            float numberPage = (float)totalProduct / limit;
            ViewBag.numberPage = (int)Math.Ceiling(numberPage);


            var dataProduct = notes.OrderByDescending(p => p.NoteTitle).Skip(start).Take(limit);


            return Json(dataProduct.ToList());
        }

        public JsonResult AjaxTest(string searchString)
        {
            List<Note> notes;

            if (searchString != "" && searchString != null)
            {
                notes = _context.Notes.Where(p => p.NoteContents.Contains(searchString)).ToList();
                
            }
            else
            {

                notes = _context.Notes.ToList();

            }
           
            return Json(notes);

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
            ViewData["UserNo"] = new SelectList(_context.Users, "UserNo", "UserId");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
