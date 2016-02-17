namespace ParichayLite.Controllers
{
    using ParichayLite.Data;
    using ParichayLite.Domain.Models;
    using Microsoft.AspNet.Identity;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Mvc;

    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly AuthRepository authRepository = null;

        public AccountController(AuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        // POST api/Account/Register
        //[AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }


            Domain.Models.UserModel umodel = new Domain.Models.UserModel() { UserName = userModel.UserName, Password = userModel.Password, ConfirmPassword = userModel.ConfirmPassword };
            var result = await authRepository.RegisterUser(umodel);

            var errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        //public IHttpActionResult Get() {

        //    var result =  new
        //    {
        //        IP = HttpContext.Current.Request.UserHostAddress,
        //        HostName =    HttpContext.Current.Request.UserHostName,
        //        Url = HttpContext.Current.Request.Url.Host,
        //        XOriginalURL = HttpContext.Current.Request.Headers.GetValues("X-Original-URL"),
        //        HeaderKeys = HttpContext.Current.Request.Headers.AllKeys,
        //        Origin = HttpContext.Current.Request.Headers.GetValues("Origin")
        //    };

        //    return Ok(result);
        //}

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return HttpBadRequest();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return HttpBadRequest();
                }

                return HttpBadRequest(ModelState);
            }

            return null;
        }
    }
}