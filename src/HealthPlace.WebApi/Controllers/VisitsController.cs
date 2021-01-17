using System;
using HealthPlace.Logic.Exceptions;
using HealthPlace.Logic.Managers;
using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;
using HealthPlace.WebApi.Helpers;
using HealthPlace.WebApi.Resources.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlace.WebApi.Controllers
{
    [Route("api/visits")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        [Authorize]
        [HttpPost("new")]
        public IActionResult New(VisitResource visit)
        {
            try
            {
                VisitManager visitMng = new VisitManager();
                visitMng.Insert(visit.ToVisit());
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
        [HttpPost("update")]
        public IActionResult Update(VisitResource visit)
        {
            try
            {
                VisitManager visitMng = new VisitManager();
                var visitDb = visitMng.GetRecordById(visit.Id);
                if (visitDb == null)
                    return BadRequest("Invalid visit id!");
                visitDb.CheckIn = visit.CheckIn;
                visitDb.CheckOut = visit.CheckOut;
                
                if (visitDb.Visitor.Id != visit.VisitorId)
                {
                    VisitorManager visitorMng = new VisitorManager();
                    Visitor visitor = visitorMng.GetRecordById(visit.VisitorId);
                    if (visitor == null)
                        return BadRequest("Invalid visitor id!");
                    visitDb.Visitor = visitor;
                }

                visitMng.Update(visitDb);
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
                VisitManager visitMng = new VisitManager();
                var visitorDb = visitMng.GetRecordById(id);

                if (visitorDb == null)
                    return BadRequest("Invalid visit id!");

                visitMng.Delete(id);
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