using Business.Abstract.ContactRequest;
using Business.Attributes;
using Entities.VMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Drawing.Printing;

namespace RayonPropertyApi.Controllers.ContactRequest
{
    [RayonPropertyAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ContactRequestController : ODataController
    {
        private readonly IContactRequestService _contactRequestService;

        public ContactRequestController(IContactRequestService contactRequestService)
        {
            _contactRequestService = contactRequestService;
        }

        [EnableQuery(EnsureStableOrdering = false, PageSize = 100)]
        [ProducesResponseType(typeof(ContactRequestEntityVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetList")]
        public IActionResult Get()
        {
            var result = _contactRequestService.GetListQueryableOdata();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _contactRequestService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
