using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Enums
{
    public enum ApiResultStatusCode
    {
        Success = 200,
        ServerError = 500,
        BadRequest = 400,
        NotFound = 404,
        NoContent = 204,
        UnAuthorized = 401
    }
}
