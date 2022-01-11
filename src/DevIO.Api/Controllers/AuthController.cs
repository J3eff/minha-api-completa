using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DevIO.Api.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DevIO.Api.Controllers
{
    [Route("api/conta")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(INotificador notificador,
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager) : base(notificador)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser 
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };


            return CustomResponse(registerUser);
        }

    }
}
