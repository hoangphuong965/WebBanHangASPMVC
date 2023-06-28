using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private ApplicationDbContext db = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ActionResult Index()
        {
            var users = (from user in db.Users
                         select new
                         {
                             UserName = user.UserName,
                             FullName = user.FullName,
                             Email = user.Email,
                             RoleNames = (from userRole in user.Roles
                                          join role in db.Roles on userRole.RoleId equals role.Id
                                          select role.Name).ToList()
                         }
                        ).ToList().Select(p => new Users_in_Role_ViewModel()
                        {
                            UserName = p.UserName,
                            FullName = p.FullName,
                            Email = p.Email,
                            Role = string.Join(",", p.RoleNames)
                        });
            return View(users);
        }

        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    Phone = model.Phone
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var checkEmail = UserManager.FindByEmail(model.Email);
            if (checkEmail == null)
            {
                ModelState.AddModelError("logginError", "Email hoặc mật khẩu không hợp lệ");
                return View();
            }
            var user = UserManager.Find(checkEmail.UserName, model.Password);
            if (user != null)
            {
                this.LoginUser(UserManager, user);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("logginError", "Email hoặc mật khẩu không hợp lệ");
            }
            return View();
        }

       
        [AllowAnonymous]
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(Microsoft.AspNet.Identity.IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public void LoginUser(ApplicationUserManager userManager ,ApplicationUser user)
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            // tạo identity dựa trên người dùng hiện tại (để bỏ vào trong cookie mà xác thực)
            var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            // AuthenticationProperties lưu trữ các giá trị-userIdentity trạng thái về authentication session.
            authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
        }
    }
}