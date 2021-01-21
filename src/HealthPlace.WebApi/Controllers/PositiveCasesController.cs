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
    [Route("api/positive-cases")]
    [ApiController]
    public class PositiveCaseController : ControllerBase
    {
        /// <summary>
        /// Retrieves all the positive cases.
        /// </summary>
        /// <returns>Positive cases</returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
                var result = positiveCaseMng.GetAllRecords().Select(p => p.ToPositiveCaseResource());
                return Ok(result);
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
        /// Gets the positive case overview.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The positive case overview</returns>
        [Authorize]
        [HttpGet("{id}/overview")]
        public IActionResult GetPositiveCaseOverview(Guid id)
        {
            try
            {
                PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
                VisitManager visitMng = new VisitManager();
                VisitorManager visitorMng = new VisitorManager();
                VisitorNotificationManager notificationMng = new VisitorNotificationManager();

                var positiveCase = positiveCaseMng.GetRecordById(id).ToPositiveCaseResource();
                var result = new PositiveCaseOverviewResource(positiveCase);

                // load visitor name (needed for the autocomplete component)
                result.VisitorName = visitorMng.GetRecordById(positiveCase.VisitorId).Name;

                // Set date to the last minute of the day to include all the visits from that day on the retrieved visits
                result.VisitDate = result.VisitDate.Date.AddHours(23).AddMinutes(59);
                var positiveCaseVisitsBefore = visitMng.GetUserVisitsBeforeDate(result.VisitorId, result.VisitDate, 2).ToList();

                // Get colliding visits
                var collidingVisits = visitMng.GetCollidingVisits(positiveCaseVisitsBefore);


                foreach(var collidingVisit in collidingVisits)
                {
                    result.CollidingVisits.Add(new AffectedVisitsResource()
                    {
                        VisitId = collidingVisit.Visitor.Id,
                        VisitDate = collidingVisit.CheckIn,
                        NotificationSent = notificationMng.GetRecordsByVisitorId(collidingVisit.Visitor.Id).Any(v => v.PositiveCase.Id == id),
                        VisitorId = collidingVisit.Visitor.Id,
                        VisitorName = visitorMng.GetRecordById(collidingVisit.Visitor.Id).Name
                    });
                }
                result.AllUsersNotified = result.CollidingVisits.Any(c => !c.NotificationSent);

                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Gets the positive case.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The positive case</returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPositiveCase(Guid id)
        {
            try
            {
                PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
                VisitManager visitMng = new VisitManager();
                VisitorNotificationManager notificationMng = new VisitorNotificationManager();
                var positiveCase = positiveCaseMng.GetRecordById(id).ToPositiveCaseResource();

                // load visitor name (needed for the autocomplete component)
                VisitorManager visitorMng = new VisitorManager();
                var visitor = visitorMng.GetRecordById(positiveCase.VisitorId);
                positiveCase.VisitorName = visitor.Name;

                #region Determine if all users with colliding visits were notified

                // Set date to the last minute of the day to include all the visits from that day on the retrieved visits
                DateTime visitDate = positiveCase.VisitDate.Date.AddHours(23).AddMinutes(59);
                var positiveCaseVisitsBefore = visitMng.GetUserVisitsBeforeDate(positiveCase.VisitorId, visitDate, 2).ToList();

                // Get colliding visits
                var collidingVisits = visitMng.GetCollidingVisits(positiveCaseVisitsBefore);

                positiveCase.AllUsersNotified = true;

                foreach (var collidingVisit in collidingVisits)
                {
                    bool notificationSent = notificationMng.GetRecordsByVisitorId(collidingVisit.Visitor.Id).Any(v => v.PositiveCase.Id == id);
                    if (!notificationSent)
                    {
                        positiveCase.AllUsersNotified = false;
                        break;
                    }

                }

                #endregion

                return Ok(positiveCase);
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
        /// Inserts a new positive case.
        /// </summary>
        /// <param name="positiveCase">The positive case.</param>
        /// <returns>HttpResponse</returns>
        [Authorize]
        [HttpPost("new")]
        public IActionResult New(PositiveCaseResource positiveCase)
        {
            try
            {
                PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
                positiveCaseMng.Insert(positiveCase.ToPositiveCase());
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
        /// Updates the specified positive case.
        /// </summary>
        /// <param name="positiveCase">The positive case.</param>
        /// <returns>HttpResponse</returns>
        [Authorize]
        [HttpPost("update")]
        public IActionResult Update(PositiveCaseResource positiveCase)
        {
            try
            {
                PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
                var positiveCaseDb = positiveCaseMng.GetRecordById(positiveCase.Id);
                if (positiveCaseDb == null)
                    return BadRequest("Invalid positive case id!");

                positiveCaseDb.VisitDate = positiveCase.VisitDate;

                if (positiveCaseDb.Visitor.Id != positiveCase.VisitorId)
                {
                    VisitorManager visitorMng = new VisitorManager();
                    Visitor visitor = visitorMng.GetRecordById(positiveCase.VisitorId);
                    if (visitor == null)
                        return BadRequest("Invalid visitor id!");
                    positiveCaseDb.Visitor = visitor;
                }

                positiveCaseMng.Update(positiveCaseDb);
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
        /// Deletes the specified positive case.
        /// </summary>
        /// <param name="id">The positive case.</param>
        /// <returns>HttpResponse</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
                var positiveCaseDb = positiveCaseMng.GetRecordById(id);

                if (positiveCaseDb == null)
                    return BadRequest("Invalid positive case id!");

                positiveCaseMng.Delete(id);
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