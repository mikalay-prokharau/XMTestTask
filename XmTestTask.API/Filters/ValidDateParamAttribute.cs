using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using XmTestTask.Core.Helpers;

namespace XmTestTask.API.Filters
{
    public class ValidDateParamAttribute : ActionFilterAttribute
    {
        public string ParamName { get; set; }

        public ValidDateParamAttribute(string paramName)
        {
            ParamName = paramName;
        }

        /// <summary>
        /// Validates unix date input to be hour accuracy 
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey(ParamName) ||
                          !DateHelper.IsUnixDateHasHourAccuracy(context.ActionArguments[ParamName]?.ToString()))
                context.Result = new BadRequestObjectResult($"The {ParamName} should be in unix millisecods format with the hour accuracy ");
        }
    }
}
