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
    public class SchoolsController : ControllerBase
    {
        static string ControllerName = "SchoolsController";
        private readonly cap01devContext _context;

        static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        public SchoolsController(cap01devContext context)
        {
            _context = context;
        }

        // GET: api/Schools
        [HttpGet]
        public JsonResult GetSchools()
        {
            try
            {
                Logger.Trace($"{ControllerName} GetSchools IN");
                DbSet<School> result = _context.Schools;


                return new JsonResult(result.ToArray<School>()) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetSchools {ex}");
                string result = "fail";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetSchools OUT");
            }
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetSchool([FromRoute] int id)
        {
            try
            {
                Logger.Trace($"{ControllerName} GetSchool(id={id}) IN");

                if (!ModelState.IsValid)
                {
                    string result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                var school = await _context.Schools.FindAsync(id);

                if (school == null)
                {
                    return new JsonResult(school) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                }

                return new JsonResult(school) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetSchool(id={id}) {ex}");
                string result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetSchool(id={id}) OUT");
            }
        }

        // PUT: api/Schools/5
        [HttpPut("{id}")]
        public async Task<JsonResult> PutSchool([FromRoute] int id, [FromBody] School school)
        {
            string result = "not valid";
            try
            {
                Logger.Trace($"{ControllerName} PutSchool(id={id}) IN");

                if (!ModelState.IsValid)
                {
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                if (id != school.ID)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                _context.Entry(school).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(id))
                    {
                        result = "not found";
                        return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} PutSchool(id={id}) {ex}");
                result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} PutSchool(id={id}) OUT");
            }
            result = "no content";
            return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NoContent };
        }

        // POST: api/Schools
        [HttpPost]
        public async Task<JsonResult> PostSchool([FromBody] School school)
        {
            string result = string.Empty;
            try
            {
                Logger.Trace($"{ControllerName} PostSchool(photo={school}) IN");

                if (!ModelState.IsValid)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                _context.Schools.Add(school);
                await _context.SaveChangesAsync();
                return null;
                //return CreatedAtAction("GetSchool", new { id = school.ID }, school);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} PostSchool(photo={school}) {ex}");
                result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} PostSchool(photo={school}) OUT");
            }
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteSchool([FromRoute] int id)
        {
            string result = string.Empty;
            try
            {
                Logger.Trace($"{ControllerName} DeleteSchool(id={id}) IN");

                if (!ModelState.IsValid)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                var school = await _context.Schools.FindAsync(id);
                if (school == null)
                {
                    result = "not found";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                }

                _context.Schools.Remove(school);
                await _context.SaveChangesAsync();

                result = "ok";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} DeleteSchool(id={id}) {ex}");
                result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} DeleteSchool(id={id}) OUT");
            }
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.ID == id);
        }
    }
}