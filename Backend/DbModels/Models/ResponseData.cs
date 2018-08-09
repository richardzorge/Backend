using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace Backend.Models
{
    [DataContract]
    public class ResponseData
    {

        public static ResponseData OK_200( string Message = "Ok",  string NextURI=null, string PrevURI=null)
        {
            
            return new ResponseData() { ResultCode = (int)System.Net.HttpStatusCode.OK, Message = Message, NextURI = NextURI, PrevURI = PrevURI };
        }

        public static ResponseData CREATED_201(string Message, string NextURI, string PrevURI = null)
        {
            return new ResponseData() { ResultCode = (int)System.Net.HttpStatusCode.Created, Message = Message, NextURI = NextURI, PrevURI = PrevURI };
        }

        public static ResponseData BAD_REQUEST_400( string Message, string NextURI = null, string PrevURI = null)
        {
            return new ResponseData() {  ResultCode = (int)System.Net.HttpStatusCode.BadRequest, Message = Message, NextURI = NextURI, PrevURI = PrevURI };
        }

        public static ResponseData CONFLICT_409(string Message, string NextURI = null, string PrevURI = null)
        {
            return new ResponseData() { ResultCode = (int)System.Net.HttpStatusCode.Conflict, Message = Message, NextURI = NextURI, PrevURI = PrevURI };
        }

        public static ResponseData METHOD_NOT_ALLOWED_405(string Message="Method not allowed", string NextURI = null, string PrevURI = null)
        {
            return new ResponseData() { ResultCode = (int)System.Net.HttpStatusCode.MethodNotAllowed, Message = Message, NextURI = NextURI, PrevURI = PrevURI };
        }

        public static ResponseData NOT_FOUND_404( string Message="Not found", string NextURI = null, string PrevURI = null)
        {
            return new ResponseData() {  ResultCode = (int)System.Net.HttpStatusCode.NotFound, Message = Message, NextURI = NextURI, PrevURI = PrevURI };
        }
        public static ResponseData INTERNAL_SERVER_ERROR_500(string Message = "Internal server error", string NextURI = null, string PrevURI = null)
        {
            return new ResponseData() {  ResultCode = (int)System.Net.HttpStatusCode.InternalServerError, Message = Message, NextURI = NextURI, PrevURI = PrevURI };
        }


        [DataMember]
        public int ResultCode { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string NextURI { get; set;}
        [DataMember]
        public string PrevURI { get; set; }
        [DataMember]
        public object Data { get; private set; }

        public ResponseData()
        {
            ResultCode = 200;
            Message = "OK";
            NextURI = string.Empty;
            PrevURI = string.Empty;
            Data = null;
        }

        public ResponseData AddData(object Data)
        {
            this.Data = Data;
            return this;
        }

    }
}
