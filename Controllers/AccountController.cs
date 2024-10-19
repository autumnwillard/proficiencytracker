using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ProficiencyTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace ProficiencyTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(model);
                }

                // Bypass email confirmation
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Account locked out.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Login not allowed. Reason: " + await GetLoginNotAllowedReason(user));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid password.");
                    }
                    Console.WriteLine($"Login failed for user {model.Email}. Result: {result}");
                    return View(model);
                }
            }
            return View(model);
        }

        private async Task<string> GetLoginNotAllowedReason(User user)
        {
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return "Email not confirmed";
            }
            // Add other checks here if needed
            return "Unknown reason";
        }

        [HttpGet]
        [Route("Account/CheckPassword")]
        public async Task<IActionResult> CheckPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Content("User not found");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            return Content($"Password correct: {isPasswordCorrect}");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                Console.WriteLine($"Attempting to create user: {model.Email}");
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Console.WriteLine($"User created successfully: {model.Email}");
                    // Bypass email confirmation
                    await _userManager.ConfirmEmailAsync(user, await _userManager.GenerateEmailConfirmationTokenAsync(user));
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    Console.WriteLine($"User creation error for {model.Email}: {error.Description}");
                }
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Model validation error: {error.ErrorMessage}");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Email}");
            }
            return View(users);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            // Generate the reset password token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Generate reset password link
            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

            // In a real application, you'd send an email here
            // For local testing, we'll just display the reset link
            ViewBag.ResetLink = callbackUrl;

            return View("ForgotPasswordConfirmation");
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string userId)
        {
            var model = new ResetPasswordViewModel { Token = token, UserId = userId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }

}
