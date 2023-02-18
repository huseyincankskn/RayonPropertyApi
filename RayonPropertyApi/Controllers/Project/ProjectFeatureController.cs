using Business.Abstract;
using Business.Attributes;
using Business.Concrete;
using Core.Entities.Exceptions;
using DataAccess.Migrations;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Drawing.Printing;

namespace RayonPropertyApi.Controllers.Project
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ProjectFeatureController : ControllerBase
    {
        private readonly IProjectFeatureService _projectFeatureService;

        public ProjectFeatureController(IProjectFeatureService projectFeatureService)
        {
            _projectFeatureService = projectFeatureService;
        }

        [EnableQuery(EnsureStableOrdering = false, PageSize = 100)]
        [ProducesResponseType(typeof(ProjectFeaturesVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [RayonPropertyAuthorize]
        [HttpGet("GetList")]
        public IActionResult Get()
        {
            var result = _projectFeatureService.GetListQueryableOdata();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [ProducesResponseType(typeof(ProjectFeaturesVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [RayonPropertyAuthorize]
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(short id)
        {
            var result = _projectFeatureService.GetById(id);

            if (result.Data == null)
            {
                throw new NotFoundException(id);
            }
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [ProducesResponseType(typeof(FeatureVm), 200)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 401)]
        [HttpGet("GetFeaturesWithProjectId/{id}")]
        public IActionResult GetFeaturesWithProjectId(Guid id)
        {
            var result = _projectFeatureService.GetFeaturesWithProjectId(id);

            if (result.Data == null)
            {
                throw new NotFoundException(id);
            }
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost]
        [RayonPropertyAuthorize]
        public IActionResult AddProject(ProjectFeatureDto projectDto)
        {
            var result = _projectFeatureService.AddProject(projectDto);
            return Ok(result);
        }
        [HttpPut]
        [RayonPropertyAuthorize]
        public IActionResult Put(ProjectFeatureDto dto)
        {
            var result = _projectFeatureService.Update(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{id}")]
        [RayonPropertyAuthorize]
        public IActionResult Delete(short id)
        {
            var result = _projectFeatureService.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
