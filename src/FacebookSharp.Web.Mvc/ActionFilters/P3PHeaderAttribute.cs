namespace FacebookSharp.Web.Mvc
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Adds P3P header
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class P3PHeaderAttribute : ActionFilterAttribute
    {
        public P3PHeaderAttribute()
        {
            Value = "CP=\"CAO PSA OUR\"";
        }

        public string Value { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.AddHeader("P3P", Value);
            base.OnActionExecuting(filterContext);
        }
    }
}