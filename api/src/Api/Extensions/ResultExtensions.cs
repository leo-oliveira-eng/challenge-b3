using Funcfy.Monads;
using Funcfy.Monads.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

internal static class ResultExtensions
{
    internal static IActionResult ToActionResult<T>(this Result<T> result)
        => result.Failed
            ? result.ToErrorActionResult()
            : new OkObjectResult(result);
}
