using System;
using System.Linq;
using HealthPlace.Logic.Exceptions;
using HealthPlace.Logic.Managers;
using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;
using HealthPlace.WebApi.Helpers;
using HealthPlace.WebApi.Resources.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlace.WebApi.Controllers
{
    /// <summary>
    /// Api Controller for the visits entity
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/visits")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        /// <summary>
        /// Retrieves the visit with the specified id.
        /// </summary>
        /// <returns>The visit</returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetVisit(Guid id)
        {
            try
            {
                VisitManager visitMng = new VisitManager();
                var result = visitMng.GetRecordById(id).ToVisitResource();
                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem();
            }
        }

        /// <summary>
        /// Retrieves all the visits.
        /// </summary>
        /// <returns>The visits</returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                VisitManager visitMng = new VisitManager();
                var result = visitMng.GetAllRecords().Select(v => v.ToVisitResource());
                return Ok(result);
            }
            catch
            {
                return Problem();
            }
        }

        /// <summary>
        /// Creates a new visit.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>HttpResponse</returns>
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

        /// <summary>
        /// Updates the specified visit.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>HttpResponse</returns>
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

        /// <summary>
        /// Deletes the specified visit.
        /// </summary>
        /// <param name="id">The visit id.</param>
        /// <returns>HttpResponse</returns>
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