using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BibliotecaVirtual.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido), AllowEmptyStrings = false)]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido), AllowEmptyStrings = false)]
            [Display(Name = "Sobrenome")]
            public string Sobrenome { get; set; }

            [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido), AllowEmptyStrings = false)]
            [CPFValidation]
            [Display(Name = "CPF")]
            public string CPF { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Required(ErrorMessageResourceType = typeof(Criticas), ErrorMessageResourceName = nameof(Criticas.Campo_Requerido), AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "A {0} precisa de {2} e no máximo {1} caracteres.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "As senhas são diferentes.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            
            if (ModelState.IsValid == true)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    #region Usuário da aplicação

                    var usuarioViewModel = new UserViewModel()
                    {
                        UsuarioIdentityId = user.Id,
                        Nome = Input.Nome,
                        Sobrenome = Input.Sobrenome,
                        CPF = Input.CPF,
                    };

                    var usuario = await _userService.AddUser(usuarioViewModel);

                    #endregion

                    if (usuario.UserId > 0)
                    {
                        _logger.LogInformation("Conta criada com sucesso.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmail(Input.Email, "Confirme o seu e-mail",
                            $"Seja bem-vindo, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clique aqui</a> para retornar para a aplicação.");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, _userService.GetModelErrors());
                        //Deletar o usuário por conta da falha ao criar o usuário da aplicação.
                        await _userManager.DeleteAsync(user);
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
