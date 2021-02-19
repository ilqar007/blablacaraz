using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.Localization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BlaBlaCarAz.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces.IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces.IEmailSender emailSender,
            IStringLocalizer<SharedResource> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _localizer = localizer;
            SharedResource.EmailRequired = localizer[nameof(SharedResource.EmailRequired)];
            SharedResource.PasswordRequired = localizer[nameof(SharedResource.PasswordRequired)];
            SharedResource.PhoneNumberRequired = localizer[nameof(SharedResource.PhoneNumberRequired)];
            SharedResource.PasswordLength = localizer[nameof(SharedResource.PasswordLength)];
            SharedResource.PasswordCompare = localizer[nameof(SharedResource.PasswordCompare)];
            SharedResource.NameLastNameRequired = localizer[nameof(SharedResource.NameLastNameRequired)];
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceName =nameof(SharedResource.EmailRequired), ErrorMessageResourceType = typeof(SharedResource))]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessageResourceName = nameof(SharedResource.PhoneNumberRequired), ErrorMessageResourceType = typeof(SharedResource))]
            [Phone]
            [Display(Name = "PhoneNumber")]
            public string PhoneNumber { get; set; }


            [Required(ErrorMessageResourceName = nameof(SharedResource.PasswordRequired), ErrorMessageResourceType = typeof(SharedResource))]
            [StringLength(100, MinimumLength = 6,ErrorMessageResourceName = nameof(SharedResource.PasswordLength), ErrorMessageResourceType = typeof(SharedResource))]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password",  ErrorMessageResourceName = nameof(SharedResource.PasswordCompare), ErrorMessageResourceType = typeof(SharedResource))]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessageResourceName = nameof(SharedResource.NameLastNameRequired), ErrorMessageResourceType = typeof(SharedResource))]
            public string NameLastName { get; set; }
            public string BirthDate { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                var user = new AppUser { UserName = Input.Email, Email = Input.Email, PhoneNumber = Input.PhoneNumber, NameLastName = Input.NameLastName,RegistrationDate=DateTime.Now };
                DateTime birthDate;
                if (DateTime.TryParseExact(Input.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
                    user.Birthdate = birthDate;
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, _localizer[nameof(SharedResource.ConfirmEmail)],
                        string.Format(_localizer[nameof(SharedResource.ConfirmEmailBody)], HtmlEncoder.Default.Encode(callbackUrl)));

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
