using System;
namespace MinimalAPI.Model;

    public class SuccessState
    {

    public bool Success { get; set; } = true;

    public string StatusCode { get; set; } = "200";

    public string Data { get; set; } = string.Empty;

    public string Message { get; set; } = "Sorgu Başarılı.";

    }


