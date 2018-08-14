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
    public class PhotosController : ControllerBase
    {
        static string ControllerName = "PhotosController";
        private readonly cap01devContext _context;

        static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        public PhotosController(cap01devContext context)
        {
            _context = context;
        }

        // GET: api/Photos
        [HttpGet]
        [Produces("application/json")]
        public JsonResult GetPhotos()
        {
            try
            {
                Logger.Trace($"{ControllerName} GetPhotos IN");
                DbSet<Photo> result = _context.Photos;


                return new JsonResult(result.ToArray<Photo>()) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetPhotos {ex}");
                string result = "fail";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetPhotos OUT");
            }
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<JsonResult> GetPhoto([FromRoute] int id)
        {
            try
            {
                Logger.Trace($"{ControllerName} GetPhotos(id={id}) IN");

                if (!ModelState.IsValid)
                {
                    string result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                var photo = await _context.Photos.FindAsync(id);

                if (photo == null)
                {
                    return new JsonResult(photo) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                }

                return new JsonResult(photo) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} GetPhotos(id={id}) {ex}");
                string result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetPhotos(id={id}) OUT");
            }
        }

        // PUT: api/Photos/5
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<JsonResult> PutPhoto([FromRoute] int id, [FromBody] Photo photo)
        {
            string result = "not valid";
            try
            {
                Logger.Trace($"{ControllerName} GetPhotos(id={id}) IN");

                if (!ModelState.IsValid)
                {
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                if (id != photo.ID)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                _context.Entry(photo).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(id))
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
                Logger.Error($"Error in {ControllerName} GetPhotos(id={id}) {ex}");
                result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} GetPhotos(id={id}) OUT");
            }
            result = "no content";
            return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NoContent };
        }

        // POST: api/Photos
        [HttpPost]
        [Produces("application/json")]
        public async Task<JsonResult> PostPhoto([FromBody] Photo photo)
        {
            string result = string.Empty ;
            try
            {
                Logger.Trace($"{ControllerName} PostPhoto(photo={photo}) IN");

                if (!ModelState.IsValid)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                _context.Photos.Add(photo);
                await _context.SaveChangesAsync();
                return null;
                //return CreatedAtAction("GetPhoto", new { id = photo.ID }, photo);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} PostPhoto(photo={photo}) {ex}");
                result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} PostPhoto(photo={photo}) OUT");
            }
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<JsonResult> DeletePhoto([FromRoute] int id)
        {
            string result = string.Empty;
            try
            {
                Logger.Trace($"{ControllerName} DeletePhoto(id={id}) IN");

                if (!ModelState.IsValid)
                {
                    result = "not valid";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.BadRequest };
                }

                var photo = await _context.Photos.FindAsync(id);
                if (photo == null)
                {
                    result = "not found";
                    return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.NotFound };
                }

                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();

                result = "ok";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Logger.Error($"Error in {ControllerName} DeletePhoto(id={id}) {ex}");
                result = "internal error";
                return new JsonResult(result) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace($"{ControllerName} DeletePhoto(id={id}) OUT");
            }
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.ID == id);
        }
    }
}