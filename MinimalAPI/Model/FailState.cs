using System;
namespace MinimalAPI.Model
{
    public class FailState
    {
        public bool Success { get; set; } = false;

        public string StatusCode { get; set; } = "404";

        public string Data { get; set; } = string.Empty;

        public string Message { get; set; } = "Sorgu Başarısız.";
    }
}

