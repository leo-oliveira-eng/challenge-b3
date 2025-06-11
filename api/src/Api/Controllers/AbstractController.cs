using Api.Extensions;
using Funcfy.Monads;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public abstract class AbstractController : ControllerBase
{
    protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<Result<T>>> func)
    {
		try
		{
			var result = await func.Invoke();

			return result.ToActionResult();
		}
		catch (Exception ex)
		{
			return StatusCode(500, new
			{
				Message = "An unexpected error occurred.",
				Details = ex.Message
			});
		}
    }
}
