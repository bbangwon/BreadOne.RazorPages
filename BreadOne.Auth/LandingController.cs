using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BreadOne.Auth
{
    [AllowAnonymous]    //익명을 허용
    public class LandingController : Controller
    {
        public IActionResult Index() => Content("누구나 접근 가능");

        [Authorize] //인증된 사용자만 가능
        [Route("/Greeting")]
        public IActionResult Greeting()
        {
            var roleName = HttpContext.User.IsInRole("Administrators") ? "관리자" : "사용자";

            return Content($"<em>{roleName}</em> 님, 반갑습니다.", "text/html", Encoding.Default);

        }
    }

    [Authorize(Roles = "Administrators")]
    public class DashboardController : Controller
    {
        public IActionResult Index() => Content("관리자 님, 반갑습니다.");
    }
}
