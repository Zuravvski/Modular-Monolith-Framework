using System.Net;

namespace Zuravvski.Infrastructure.Exceptions.Abstractions
{
    public record ExceptionResponse(object Response, HttpStatusCode StatusCode = HttpStatusCode.BadRequest);
    public record Error(string Code, string Message);
    public record ErrorsResponse(params Error[] Errors);
}
