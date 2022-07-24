﻿namespace DentallApp.Helpers;

public class Response : ResponseBase
{
    public object Data { get; set; }

    public Response()
    {
        
    }

    public Response(string message) : base(message)
    {

    }
}
