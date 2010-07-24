using System;
using System.Web.Mvc;

namespace FacebookSharp.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class FacebookAuthorizeAttribute : AuthorizeAttribute
    {
    }
}