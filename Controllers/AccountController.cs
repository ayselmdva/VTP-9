using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using VTP_9.Models;
using VTP_9.View_Models;

namespace VTP_9.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser()
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(newUser, "Member");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var confirmationUrl = Url.Action("ConfirmEmail", "Account", new { userId = newUser.Id, token = token }, Request.Scheme);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("maysel353@gmail.com", "fpqzhxrtrphoitbf"),
                EnableSsl = true,
            };
            MailMessage mailMessage = new MailMessage("maysel353@gmail.com", newUser.Email, "Email Confirmation", $@"<a href={confirmationUrl}>Verify Email</a>");
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);

            await _signInManager.SignInAsync(newUser, false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) { return View(); }
            if (loginVM.UserNameOrEmail.Contains("@"))
            {
                var user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    NotFound();
                    return View();
                }
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, true);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
            }
            else
            {
                var user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    NotFound();
                    return View();
                }
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, true);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error");
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}