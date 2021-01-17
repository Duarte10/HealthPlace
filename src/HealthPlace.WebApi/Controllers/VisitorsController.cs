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
        [HttpPost("new")]
        public IActionResult New(VisitorResource visitor)
        {
            try
            {
                VisitorManager visitorMng = new VisitorManager();
                visitorMng.Insert(visitor.ToVisitor());
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
                visitorMng.Update(visitorDb);
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

        [Authorize]
        [HttpPost("delete")]
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