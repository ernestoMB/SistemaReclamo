using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



namespace SistemaReclamo.Filters
{
    

    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            if (session.GetInt32("IdEmpleado") == null)
            {
                context.Result = new RedirectToActionResult("LoginDC", "Login", null);
            }
            base.OnActionExecuting(context);
        }
    }

}
