using Communication.EmailManager.Abstract;
using Entities.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RayonPropertyApi.Controllers.WebSite
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WebSiteContactController : ControllerBase
    {
        private readonly IEmailManager _emailManager;

        public WebSiteContactController(IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }


        [HttpPost("SendContactRequest")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult SendContactMail(ContactRequestVm contactRequestVm)
        {
            _emailManager.SendContactRequestMail(contactRequestVm);
            return Ok(true);
        }
    }
}
