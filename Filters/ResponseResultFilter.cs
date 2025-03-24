using booking_system.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace booking_system.Filters;

public class ResponseResultFilter: IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            if (objectResult.Value is ApiResponse) return;

            context.Result = new ObjectResult(new ApiResponse
            {
                Success = true,
                Data = objectResult.Value,
                Message = "Success",
            })
            {
                StatusCode = objectResult.StatusCode,
            };
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        
    }
}