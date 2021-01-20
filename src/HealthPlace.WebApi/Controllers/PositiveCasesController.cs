﻿using System;
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

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPositiveCase(Guid id)
        {
            try
            {
                PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
                var positiveCase = positiveCaseMng.GetRecordById(id).ToPositiveCaseResource();
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
        [HttpPost("delete")]
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