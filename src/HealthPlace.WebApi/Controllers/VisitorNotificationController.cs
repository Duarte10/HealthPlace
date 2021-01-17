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
    [Route("api/visitor-notifications")]
    [ApiController]
    public class VisitorNotificationController : ControllerBase
    {
        [Authorize]
        [HttpPost("new")]
        public IActionResult New(VisitorNotificationResource visitorNotification)
        {
            try
            {
                VisitorNotificationManager visitorNotificationMng = new VisitorNotificationManager();
                visitorNotificationMng.Insert(visitorNotification.ToVisitorNotification());
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
        public IActionResult Update(VisitorNotificationResource visitorNotification)
        {
            try
            {
                VisitorNotificationManager visitorNotificationMng = new VisitorNotificationManager();
                var visitorNotificationDb = visitorNotificationMng.GetRecordById(visitorNotification.Id);
                if (visitorNotificationDb == null)
                    return BadRequest("Invalid visitor notification id!");

                visitorNotificationDb.SentDate = visitorNotification.SentDate;

                if (visitorNotificationDb.Visitor.Id != visitorNotification.VisitorId)
                {
                    VisitorManager visitorMng = new VisitorManager();
                    Visitor visitor = visitorMng.GetRecordById(visitorNotification.VisitorId);
                    if (visitor == null)
                        return BadRequest("Invalid visitor id!");
                    visitorNotificationDb.Visitor = visitor;
                }

                if (visitorNotificationDb.PositiveCase.Id != visitorNotification.PositiveCaseId)
                {
                    PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
                    PositiveCase positiveCase = positiveCaseMng.GetRecordById(visitorNotification.PositiveCaseId);
                    if (positiveCase == null)
                        return BadRequest("Invalid positive case id!");
                    visitorNotificationDb.PositiveCase = positiveCase;
                }

                visitorNotificationMng.Update(visitorNotificationDb);
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
                VisitorNotificationManager visitorNotificationMng = new VisitorNotificationManager();
                var visitorNotificationDb = visitorNotificationMng.GetRecordById(id);

                if (visitorNotificationDb == null)
                    return BadRequest("Invalid positive case id!");

                visitorNotificationMng.Delete(id);
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