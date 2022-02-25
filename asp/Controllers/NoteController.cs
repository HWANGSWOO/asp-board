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
using jQuery_Ajax_CRUD;

namespace asp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NoteDbcontext _context;

        public NoteController(NoteDbcontext context)
        {
            _context = context;
        }


        public IActionResult Index(int? page,string sortOrder,string SearchText = "")
        {

            var notes = from p in _context.Notes
                        select p;



            if(page > 0)
            {
                page = page;
            }
            else
            {
                page = 1; //페이지를 기본값으로 설정
            }
            int limit = 5;
            int start = (int)(page -1) * limit;
            int totalProduct = notes.Count();
            ViewBag.totalProduct = totalProduct;
            ViewBag.pageCurrent = page;
            float numberPage = (float)totalProduct/ limit;
            ViewBag.numberPage =(int) Math.Ceiling(numberPage);


            var dataProduct = notes.OrderByDescending(p => p.NoteTitle).Skip(start).Take(limit);




            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ContetnsSortParm"] = String.IsNullOrEmpty(sortOrder) ? "contents_desc" : "";


            switch (sortOrder)
            {
                case "name_desc":
                    notes = notes.OrderByDescending(s=>s.NoteTitle);
                    break;
                case "contents_desc":
                    notes = notes.OrderByDescending(s => s.NoteContents);
                    break;
                default:
                    notes=notes.OrderBy(s=>s.NoteTitle);
                    break;
            }



            return View(dataProduct.ToList());

        }

        public JsonResult PageTests(int page)
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


        public JsonResult AjaxTest(string SearchText)
        {

            var notes = from note in _context.Notes
                        select note;
            if (!string.IsNullOrEmpty(SearchText))
            {
                notes = _context.Notes.Where(s => s.NoteContents.Contains(SearchText));
            }
            return Json(notes);

        }
        ////// GET: Notes
        //public async Task<IActionResult> Index()
        //{
        //    var noteDbcontext = _context.Notes.Include(n => n.User);
        //    return View(await noteDbcontext.ToListAsync());
        //}


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
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == 0)
                return View(new Note());

            else
            {
                var note = await _context.Notes.FindAsync(id);
                if (note == null)
                {
                    return NotFound();
                }


                return View(note);
            }
            //if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            //{
            //    //로그인이 안된 상태
            //    return RedirectToAction("Login", "Account");
            //}
            //ViewData["UserNo"] = new SelectList(_context.Users, "UserNo", "UserId");
            //return View();

        }


 
        // POST: Notes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("NoteNo,NoteTitle,NoteContents,UserNo")] Note note)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            note.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());


                if (ModelState.IsValid)
            {
                //INsert
                if (id == 0)
                {
                    _context.Add(note);
                    await _context.SaveChangesAsync();

                }
                // Update
                else
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
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this,"_ViewAll",_context.Notes.ToList())});
                }


                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", note)});
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
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Notes.ToList()) });
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.NoteNo == id);
        }


    }
}


