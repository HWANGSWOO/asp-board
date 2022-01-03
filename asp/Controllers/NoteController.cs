﻿using asp.DataContext;
using asp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace asp.Controllers
{
    
    public class NoteController : Controller
    {
        /// <summary>
        /// 게시판 리스트
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY")==null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
                using (var db = new NoteDbcontext())
            {
                var list = new List<Note>(); //리스트 선언
                //var list = db.Notes.ToList(); //노트테이블 안에 있는 모든 리스트를 출력하려면 Tolist
                return View(list);
            }
        }
        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Add(Note model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            model.NoteNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString()); //userno가 null이면 안되서 
            if (ModelState.IsValid)
            {
                using (var db = new NoteDbcontext())
                {
                    db.Notes.Add(model);
                    if(db.SaveChanges()>0)  //SaveChanges -> commit, 성공한 개수 
                    {
                        return Redirect("Index"); // 뒤에 Note 적혀있는 것과 같음
                    }   
                }
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            }
            return View(model);
        }



        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <returns></returns>

        public IActionResult Edit()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <returns></returns>

        public IActionResult Delete()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}