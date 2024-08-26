﻿using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<T> where T : class?
{
    private readonly int _code;

    [JsonConstructor]
    public Response()
        => _code = Configuration.DefaultStatusCode;

    public Response(T? data, int code = Configuration.DefaultStatusCode, string?  message = null)
    {
        Data = data;
        _code = code;
        Message = message;
    }
    
    public T? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
}
