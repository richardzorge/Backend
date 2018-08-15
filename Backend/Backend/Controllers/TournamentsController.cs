using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using DbModels;

namespace Backend.Controllers
{
    [Route("api_v1/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        static string ControllerName = "TournamentsController";
        private readonly cap01devContext _context;

        static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        public TournamentsController(cap01devContext context)
        {
            _context = context;
        }

        // GET: api/Tournaments/"201808102200+0300"
        [HttpGet]
        public JsonResult GetTournaments([FromRoute] string last_date_time)
        {
            try
            {
                Logger.Trace($"{ControllerName} GetTournaments IN");
                DbSet<Tournament> result = _context.Tournaments;
                

                return new JsonResult(result.ToArray<Tournament>()) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetTournaments {ex}");
                string result = "fail";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetTournaments OUT");
            }
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetTournament([FromRoute] int id)
        {
            string result = string.Empty;
            try
            {
                Logger.Trace($"{ControllerName} GetTournament(id={id}) IN");
                if (!ModelState.IsValid)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                var tournament = await _context.Tournaments.FindAsync(id);

                if (tournament == null)
                {
                    result = "not found";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                }
                return new JsonResult(tournament) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetTournament(id={id}) {ex}");
                result = "fail";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetTournament(id={id}) OUT");
            }
        }

        // PUT: api/Tournaments/5
        [HttpPut("{id}")]
        public async Task<JsonResult> PutTournament([FromRoute] int id, [FromBody] Tournament tournament)
        {
            string result = string.Empty;
            try
            {
                Logger.Trace($"{ControllerName} GetTournament(id={id}) IN");
                if (!ModelState.IsValid)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                if (id != tournament.ID)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                _context.Entry(tournament).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(id))
                    {
                        result = "not found";
                        return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                    }
                    else
                    {
                        throw;
                    }
                }

                return new JsonResult(tournament) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetTournament(id={id}) {ex}");
                result = "fail";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetTournament(id={id}) OUT");
            }
            result = "no content";
            return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NoContent };
        }

        // POST: api/Tournaments
        [HttpPost]
        public async Task<JsonResult> PostTournament([FromBody] Tournament tournament)
        {
            string result = string.Empty;
            try
            {
                Logger.Trace($"{ControllerName} PostTournament(photo={tournament}) IN");

                if (!ModelState.IsValid)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                _context.Tournaments.Add(tournament);
                await _context.SaveChangesAsync();
                return null;
                //return CreatedAtAction("GetTournament", new { id = tournament.ID }, tournament);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} PostTournament(photo={tournament}) {ex}");
                result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} PostTournament(photo={tournament}) OUT");
            }
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteTournament([FromRoute] int id)
        {
            string result = string.Empty;
            try
            {
                Logger.Trace($"{ControllerName} DeleteTournament(id={id}) IN");

                if (!ModelState.IsValid)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                var tournament = await _context.Tournaments.FindAsync(id);
                if (tournament == null)
                {
                    result = "not found";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                }

                _context.Tournaments.Remove(tournament);
                await _context.SaveChangesAsync();

                result = "ok";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} DeleteTournament(id={id}) {ex}");
                result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} DeleteTournament(id={id}) OUT");
            }
        }

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(e => e.ID == id);
        }
    }
}