using Dow.SSD.Framework.Common;
using System.Web.Mvc;

namespace Dow.SSD.Framework.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        /// <summary>
        /// Handling exception with your logics -- ctx.Exception
        /// </summary>
        /// <param name="ctx">exception context</param>
        protected override void OnException(ExceptionContext ctx)
        {
            if (HttpContext.IsDebuggingEnabled)
            {
                throw ctx.Exception;
            }
            else
            {
                ExceptionManager.HandleException(ctx.Exception);
            }
        }
    }
}