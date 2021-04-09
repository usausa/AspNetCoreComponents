namespace Example.Web.Infrastructure.Filters
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Example.Models.Paging;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Smart.AspNetCore;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class PageCorrectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ViewResult view)
            {
                var paged = view.Model as IPageOver ??
                            view.ViewData.Values.FirstOrDefault(x => x is IPageOver) as IPageOver;
                if (paged == null)
                {
                    return;
                }

                if (paged.IsOver)
                {
                    context.Result = new RedirectResult(
                        String.Concat(
                            context.HttpContext.Request.Path,
                            context.HttpContext.Request.QueryString.Replace("Page", paged.TotalPage.ToString(CultureInfo.InvariantCulture))));
                }
            }
        }
    }
}
