using AutoMapper;
using ExchangeRate.Core.Interfaces;
using ExchangeRate.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExchangeRate.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;


        public AccountController(IMapper mapper,
            ILogger<AccountController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                var model = new AccountLoginModel
                {
                    ReturnUrl = returnUrl
                };
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginModel model)
        {
            if (await _accountService.CheckPassword(model.Email, model.Password))
            {
                var userId = (await _accountService.GetUserIdByEmailAsync(model.Email))
                    .GetValueOrDefault();

                var roleClaims =
                    (await _accountService.GetRolesAsync(userId))
                    .Select(rn => new Claim(ClaimTypes.Role, rn));
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.Email)
                };
                claims.AddRange(roleClaims);
                var claimsIdentity = new ClaimsIdentity(claims, authenticationType: "Cookies");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return Redirect(model.ReturnUrl ?? "/");
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Register(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                var model = new AccountRegisterModel
                {
                    ReturnUrl = returnUrl
                };
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _accountService.CheckUserWithThatEmailIsExistAsync(model.Email))
                {
                    var userId = await _accountService.CreateUserAsync(model.Email, model.Name);
                    await _accountService.SetRoleAsync(userId, "User");
                    await _accountService.SetPasswordAsync(userId, model.Password);

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, model.Email),
                        new Claim(ClaimTypes.Role, "User")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, authenticationType: "Cookies");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return Redirect(model.ReturnUrl ?? "/");
                }
                else
                {
                    ModelState.TryAddModelError("UserAlreadyExist", "User is already exist");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("access-denied")]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }



    }
}
