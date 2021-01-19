using System;
using System.Collections.Generic;
using System.Linq;
using HealthPlace.Logic.Exceptions;
using HealthPlace.Logic.Managers;
using HealthPlace.WebApi.ApiResources;
using HealthPlace.WebApi.Helpers;
using HealthPlace.WebApi.Resources.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlace.WebApi.Controllers
{
    [Route("api/visitors")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                VisitorManager visitorMng = new VisitorManager();
                List<VisitorResource> visitors = visitorMng.GetAllRecords().Select(v => v.ToVisitorResource()).ToList();
                return Ok(visitors);
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem();
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetVisitor(Guid id)
        {
            try
            {
                VisitorManager visitorMng = new VisitorManager();
                VisitorResource visitor = visitorMng.GetRecordById(id).ToVisitorResource();
                return Ok(visitor);
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem();
            }
        }

        [Authorize]
        [HttpPost("new")]
        public IActionResult New(VisitorResource visitor)
        {
            try
            {
                VisitorManager visitorMng = new VisitorManager();
                var mappedVisitor = visitor.ToVisitor();
                mappedVisitor.CreatedBy = ((UserResource)HttpContext.Items["User"]).Email;
                visitorMng.Insert(mappedVisitor);
                return Ok();
            }
            catch(EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem();
            }
        }

        [Authorize]
        [HttpPost("update")]
        public IActionResult Update(VisitorResource visitor)
        {
            try
            {
                VisitorManager visitorMng = new VisitorManager();
                var visitorDb = visitorMng.GetRecordById(visitor.Id);
                if (visitorDb == null)
                    return BadRequest("Invalid visitor id!");
                visitorDb.Name = visitor.Name;
                visitorDb.Email = visitor.Email;
                visitorDb.Mobile = visitor.Mobile;
                visitorDb.UpdatedBy = ((UserResource)HttpContext.Items["User"]).Email;
                visitorMng.Update(visitorDb);
                return Ok();
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                VisitorManager visitorMng = new VisitorManager();
                var visitorDb = visitorMng.GetRecordById(id);

                if (visitorDb == null)
                    return BadRequest("Invalid visitor id!");

                visitorMng.Delete(id);
                return Ok();
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem();
            }
        }
    }
}