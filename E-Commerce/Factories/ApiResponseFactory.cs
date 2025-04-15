using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrors(ActionContext context)
        {
            //Get All Errors in ModelState
            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                .Select(error=> new ValidationError
                {
                    Field=error.Key,//id
                    Errors=error.Value.Errors.Select(e=>e.ErrorMessage)
                });
            //Create Custom Response
            var response = new ValidationErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "There Is A problem With Validation",
                Errors = errors
            };
            //Return
            return new BadRequestObjectResult(response);
        }
    }
}
