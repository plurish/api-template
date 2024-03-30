using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Plurish.Common.Types.Output;

namespace Plurish.Template.Api.Filters.ResponseMapping;

/// <summary>
/// Filters responsável por converter um Result para um Response
/// </summary>
internal sealed class ResponseMappingFilter(IMapper mapper) : IAsyncActionFilter
{
    readonly IMapper _mapper = mapper;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ActionExecutedContext result = await next();

        if (result.Exception is not null)
        {
            return;
        }

        var response = result.Result as ObjectResult;

        object? resultObject = response?.Value;

        if (resultObject is not Result)
        {
            return;
        }

        response!.StatusCode = DeterminarStatusCode((Result)resultObject);

        if (response.StatusCode is StatusCodes.Status204NoContent)
        {
            return;
        }

        response.Value = _mapper.Map<Response<object>>(resultObject);
        response.DeclaredType = typeof(Response<object>);
    }

    private static int DeterminarStatusCode(Result res) =>
        res.Reason switch
        {
            ResultReason.Ok => StatusCodes.Status200OK,
            ResultReason.Created => StatusCodes.Status201Created,
            ResultReason.Empty => StatusCodes.Status204NoContent,
            ResultReason.InvalidInput => StatusCodes.Status400BadRequest,
            ResultReason.UnexistentId => StatusCodes.Status404NotFound,
            ResultReason.BusinessLogicViolation => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError,
        };
}