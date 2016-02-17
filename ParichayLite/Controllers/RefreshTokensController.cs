namespace ParichayLite.Controllers
{
    using ParichayLite.Data;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Mvc;

    [Route("api/RefreshTokens")]
    public class RefreshTokensController : Controller
    {
        private readonly AuthRepository authRepository = null;

        public RefreshTokensController(AuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        //[Authorize(Users = "Admin")]
        [Route("")]
        public IActionResult Get()
        {
            return Ok(authRepository.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        //[AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> Delete(string tokenId)
        {
            var result = await authRepository.RemoveRefreshToken(tokenId);

            if (result)
            {
                return Ok();
            }

            return HttpBadRequest("Token Id does not exist");
        }
    }
}