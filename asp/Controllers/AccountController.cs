using asp.DataContext;
using asp.Models;
using asp.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace asp.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)//(파라미터)  , 뷰모델 대신 User가 들어가면 UserName은 null이라 오류가 뜸
                                                                        // 그래서 뷰모델을 만들어서 사용
        {
            if(ModelState.IsValid)
            {
                using(var db = new NoteDbcontext()) 
                {   // => : A go to B
                    var user = db.Users
                        .FirstOrDefault(u => u.UserId.Equals(model.UserId) && 
                                             u.UserPassword.Equals(model.UserPassword));//유저에서 처음 혹은 기본값을 출력을 하겠다는 뜻
                    if(user != null)
                    {
                        //HttpContext.Session.SetInt32(key, value); key는 특정 세션을 식별하는 식별자
                                                                     //value는 실제 데이터 값
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo); //로그인 하면 userno가 등록이 됨
                        return RedirectToAction("index", "Home");
                    }
                
                }
                ModelState.AddModelError(string.Empty, "로그인 실패");
            }
            return View(model); //아닐 때  model
        }
        public IActionResult Logout()
        {
            //HttpContext.Session.Clear(); --> 존재하는 모든것 삭제
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            return RedirectToAction("Index", "Home");

        }


        /// <summary>
        /// 회원가입
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 회원가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid) //모두 입력을 받았는지 안받았는지 확인
            {
                using (var db = new NoteDbcontext()) //using :데이터베이스 입출력 할때 데이터를 닫을떄는
                                                     //connection을 열고 끝나면 connection을 닫아야한다.
                                                     //데이버테이스 자원을 쓰고 나면 반드시 닫아야 한다. 메모리 누수가 없기 떄문
                                                     //그래서 using을 사용 -> 자동으로 닫아짐

                {
                    db.Users.Add(model); //메모리상으로 추가
                    db.SaveChanges();    //sql에 저장
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
