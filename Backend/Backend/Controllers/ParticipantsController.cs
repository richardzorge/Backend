using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using DbModels.Models;

namespace Backend.Controllers
{
    [Route("api_v1/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        static string ControllerName = "ParticipantsController";
        static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }
        private readonly cap01devContext _context;

        public ParticipantsController(cap01devContext context)
        {
            _context = context;
        }

        // GET: api/Participants
        [HttpGet]
        public JsonResult GetParticipants()
        {
            try
            {
                Logger.Trace($"{ControllerName} GetParticipants IN");
                DbSet<Participant> result = _context.Participants;


                return new JsonResult(result.ToArray<Participant>()) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch(Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetParticipants {ex}");
                string result = "fail";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetParticipants OUT");
            }
            
        }

        // GET: api/Participants/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetParticipant([FromRoute] int id)
        {
            try
            {
                Logger.Trace($"{ControllerName} GetParticipant IN");

                if (!ModelState.IsValid)
                {
                    string result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                var participant = await _context.Participants.FindAsync(id);

                if (participant == null)
                {
                    return new JsonResult(participant) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                }

                return new JsonResult(participant){ StatusCode = (int)System.Net.HttpStatusCode.OK};
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetParticipants {ex}");
                string result = "fail";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetParticipants OUT");
            }
            
        }

        // PUT: api/Participants/5
        [HttpPut("{id}")]
        public async Task<JsonResult> PutParticipant([FromRoute] int id, [FromBody] Participant participant)
        {
            string result = "not valid";
            if (!ModelState.IsValid)
            {
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
            }

            if (id != participant.ID)
            {
                result = "bad request";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
            }

            _context.Entry(participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
                {
                    return new JsonResult(participant) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                }
                else
                {
                    throw;
                }
            }

            result = "no content";
            return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NoContent};
        }

        // POST: api/Participants
        [HttpPost]
        public async Task<JsonResult> PostParticipant([FromBody] Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipant", new { id = participant.ID }, participant);
        }

        // DELETE: api/Participants/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteParticipant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var participant = await _context.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();

            return Ok(participant);
        }

        private bool ParticipantExists(int id)
        {
            return _context.Participants.Any(e => e.ID == id);
        }
    }
}