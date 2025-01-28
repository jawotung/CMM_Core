using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class GenericResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }

        public GenericResponse(bool success, string message, int statusCode, T data = default)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }
    }

    public class ReturnStatus
    {
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
    }
    public class ReturnStatusData<T>(T data) : ReturnStatus
    {
        public T Data { get; set; } = data;
    }
    public class ReturnStatusList<T>(List<T> data) : ReturnStatus
    {
        public List<T> Data { get; set; } = data;
    }
    public class ReturnDownload
    {
        [JsonPropertyName("DataBase64")]
        public string DataBase64 { get; set; } = "";
        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = "";
    }
}
