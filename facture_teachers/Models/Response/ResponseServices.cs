using System.Net;

namespace facture_teachers.Models.Response
{
    public class ResponseServices<T>
    {
        public ResponseServices(HttpStatusCode _code = HttpStatusCode.OK, string _message = null, T _data = default, int _count = 0, dynamic _errors = null)
        {
            ResponseTime = DateTime.UtcNow.AddHours(-5);
            Code = (int)_code;
            Message = _message;
            Data = _data;
            Count = _count;
            Errors = _errors;
        }

        [Newtonsoft.Json.JsonProperty("message")]
        public string Message { get; set; }

        [Newtonsoft.Json.JsonProperty("count")]
        public int Count { get; set; }

        [Newtonsoft.Json.JsonProperty("responseTime")]
        public DateTime ResponseTime { get; set; }

        [Newtonsoft.Json.JsonProperty("data")]
        public T Data { get; set; }

        [Newtonsoft.Json.JsonProperty("code")]
        public int Code { get; set; }
            
        [Newtonsoft.Json.JsonProperty("errors")]
        public dynamic Errors { get; set; }
    }
}

