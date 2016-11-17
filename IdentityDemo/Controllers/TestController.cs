using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace IdentityDemo.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private string Page(string title, IPrincipal user) {
            var content = $"<h1>{title}</h1>";
            content += $"<p>Logged in as user '{user.Identity.Name}'</p>";
            return content;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return Content(Page("Index", User));
        }

        [AllowAnonymous]
        public ActionResult Public()
        {
            return Content(Page("Public", User));
        }

        [OverrideAuthorization]
        [Authorize]
        public ActionResult Member()
        {
            return Content(Page("Member", User));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return Content(Page("Admin", User));
        }

        [Authorize(Roles = "Editor")]
        public ActionResult Editor()
        {
            return Content(Page("Editor", User));
        }

        [Authorize(Roles = "Editor,Admin")]
        public ActionResult AdminOrEditor()
        {
            return Content(Page("AdminOrEditor", User));
        }

        [Authorize(Roles = "Editor")]
        [Authorize(Roles = "Admin")]
        public ActionResult Root()
        {
            return Content(Page("Root", User));
        }

        [Authorize(Users = "jocke@lexicon.se")]
        public ActionResult Jocke()
        {
            return Content(Page("Jocke", User));
        }


    }
}