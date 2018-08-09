using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System.ComponentModel.DataAnnotations;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Backend.Controllers
{
    [Route("ca/manage")]
    public class AdminsController : Controller
    {
        cap01devContext db = null;

        static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        public AdminsController(cap01devContext context)
        {
            
            db = context;
        }

        [Produces("application/json")]
        [HttpPost("new")]
        public JsonResult create([FromBody] string content)
        {
            try
            {
                JsonResult result = null;
                Logger.Trace("AdminsController.Creater IN");
                HttpRequest Request = ControllerContext.HttpContext.Request;
                PathString st = Request.PathBase;
                if (!Request.ContentType.Contains("application/json"))
                {
                    result = new JsonResult(ResponseData.CONFLICT_409("Wrong content type. Content type must be 'application/json'"));
                    result.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                    return result;
                }
                if(Request.Body.Length>1024)
                {
                    result = new JsonResult(ResponseData.CONFLICT_409("Wrong content type. Content type must be 'application/json'"));
                    result.StatusCode = (int)System.Net.HttpStatusCode.Conflict;
                    return result;
                }

                RequestData requestData = RequestData.Deserialize(Request.Body);
                
                string Email = requestData.GetValue("email") as string;
                string Comment = requestData.GetValue("comment") as string;
                string FullName = requestData.GetValue("full_name") as string;
                string passwordHash = requestData.GetValue("password_hash") as string;

                Logger.Debug($"AdminsController.Creater Email = {Email}");
                Logger.Debug($"AdminsController.Creater FullName = {FullName}");
                Logger.Debug($"AdminsController.Creater Comment = {Comment}");

                Admins item = new Admins()
                {
                    Email = Email,
                    Comment = Comment,
                    FullName = FullName,
                    PasswordHash = passwordHash
                };

                Admins.Add(item,db);
                Logger.Debug("AdminsController.Creater Inserted");               
                return new JsonResult(ResponseData.CREATED_201("Created", "")) { StatusCode = (int)System.Net.HttpStatusCode.Created};
            }
            catch(ValidationException ex)
            {
                Logger.Error(ex, "Error in Admins.create: ValidateError");
                return  new JsonResult(ResponseData.CONFLICT_409(ex.Message) ) { StatusCode = (int)System.Net.HttpStatusCode.Conflict};
            }
            catch(Exception ex)
            {
                Logger.Error(ex, "Error in Admins.create");
                return new JsonResult(ResponseData.INTERNAL_SERVER_ERROR_500()) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace("AdminsController.Creater OUT");
            }
        }

        //[Authorize()]
        [Produces("application/json")]
        [HttpPost("delete")]
        public JsonResult delete([FromBody] string content)
        {
            try
            {

                Logger.Trace("AdminsController.delete IN");
                HttpRequest Request = ControllerContext.HttpContext.Request;

                if (!Request.ContentType.Contains("application/json"))
                {
                    return new JsonResult(ResponseData.CONFLICT_409("Wrong content type. Content type must be 'application/json'")) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
                }
                if (Request.Body.Length > 1000)
                {
                    return new JsonResult(ResponseData.CONFLICT_409("Big content. The data length must be less than 1000")) { StatusCode = (int)System.Net.HttpStatusCode.Conflict};
                }



                RequestData requestData = RequestData.Deserialize(Request.Body);
                string Email = requestData.GetValue("email") as string;
                Logger.Debug($"AdminsController.delete Email = {Email}");
                Admins.Delete(Email,db);
                Logger.Debug("AdminsController.delete deleted");
                return new JsonResult(ResponseData.OK_200()) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (ValidationException ex)
            {
                Logger.Error(ex, "Error in Admins.delete: ValidateError");
                return new JsonResult(ResponseData.CONFLICT_409(ex.Message)) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in Admins.delete");
                return new JsonResult(ResponseData.INTERNAL_SERVER_ERROR_500()) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace("AdminsController.delete OUT");
            }
        }

        //[Authorize()]
        [Produces("application/json")]
        [HttpPatch("shutdown")]
        public JsonResult disable([FromBody] string content)
        {
            try
            {
                Logger.Trace("AdminsController.disable IN");
                HttpRequest Request = ControllerContext.HttpContext.Request;

                if (!Request.ContentType.Contains("application/json"))
                {
                    return new JsonResult(ResponseData.CONFLICT_409("Wrong content type. Content type must be 'application/json'")) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
                }
                if (Request.Body.Length > 1000)
                {
                    return new JsonResult(ResponseData.CONFLICT_409("Big content. The data length must be less than 1000")) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
                }
                RequestData requestData = RequestData.Deserialize(Request.Body);
                string Email = requestData.GetValue("email") as string;
                Logger.Debug($"AdminsController.disable Email = {Email}");
                Admins.Disable(Email,db);
                Logger.Debug("AdminsController.disable deleted");
                return new JsonResult(ResponseData.OK_200()) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (ValidationException ex)
            {
                Logger.Error(ex, "Error in Admins.disable: ValidateError");
                return new JsonResult(ResponseData.CONFLICT_409(ex.Message)) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in Admins.disable");
                return new JsonResult(ResponseData.INTERNAL_SERVER_ERROR_500()) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace("AdminsController.disable OUT");
            }
        }


        //[Authorize()]
        [Produces("application/json")]
        [HttpPatch("poweron")]
        public JsonResult enable([FromBody] string content)
        {
            try
            {
                Logger.Trace("AdminsController.enable IN");
                HttpRequest Request = ControllerContext.HttpContext.Request;

                if (!Request.ContentType.Contains("application/json"))
                {
                    return new JsonResult(ResponseData.CONFLICT_409("Wrong content type. Content type must be 'application/json'")) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
                }
                if (Request.Body.Length > 1000)
                {
                    return new JsonResult(ResponseData.CONFLICT_409("Big content. The data length must be less than 1000")) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
                }
                RequestData requestData = RequestData.Deserialize(Request.Body);
                string Email = requestData.GetValue("email") as string;
                Logger.Debug($"AdminsController.enable Email = {Email}");
                Admins.Enable(Email,db);
                Logger.Debug("AdminsController.enable deleted");
                return new JsonResult(ResponseData.OK_200()) { StatusCode = (int)System.Net.HttpStatusCode.OK };
            }
            catch (ValidationException ex)
            {
                Logger.Error(ex, "Error in Admins.enable: ValidateError");
                return new JsonResult(ResponseData.CONFLICT_409(ex.Message)) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in Admins.enable");
                return new JsonResult(ResponseData.INTERNAL_SERVER_ERROR_500()) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
            finally
            {
                Logger.Trace("AdminsController.enable OUT");
            }
        }


        //[Authorize()]
        [Produces("application/json")]
        [HttpGet("list")]
        public JsonResult list([FromBody] string content)
        {
            try
            {
                Logger.Trace("AdminsController.list IN");
                HttpRequest Request = ControllerContext.HttpContext.Request;

                if (!Request.ContentType.Contains("application/json"))
                {
                    return new JsonResult(ResponseData.CONFLICT_409("Wrong content type. Content type must be 'application/json'")) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
                }
                if (Request.Body.Length > 1000)
                {
                    return new JsonResult(ResponseData.CONFLICT_409("Big content. The data length must be less than 1000")) { StatusCode = (int)System.Net.HttpStatusCode.Conflict };
                }

                List<Admins> data = Admins.All(db);
                foreach(Admins item in data)
                {
                    item.Prepare();
                }
                return new JsonResult(ResponseData.OK_200().AddData(data)) { StatusCode = (int)System.Net.HttpStatusCode.OK, ContentType="application/json" };
            }
            catch (ValidationException ex)
            {
                Logger.Error(ex, "Error in Admins.list: ValidateError");
                return new JsonResult(ResponseData.CONFLICT_409(ex.Message)) { StatusCode = (int)System.Net.HttpStatusCode.Conflict, ContentType = "application/json" };
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in Admins.list");
                return new JsonResult(ResponseData.INTERNAL_SERVER_ERROR_500()) { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError, ContentType = "application/json" };
            }
            finally
            {
                Logger.Trace("AdminsController.list OUT");
            }
        }
    }
}
