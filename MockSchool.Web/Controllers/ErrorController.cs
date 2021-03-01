using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MockSchool.Web.Controllers
{
    /// <summary>
    /// 处理异常信息
    /// </summary>
    public class ErrorController : Controller
    {
        private ILogger logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            //ViewBag.Path = statusCodeResult.OriginalPath + statusCodeResult.OriginalQueryString;

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，您访问的页面不存在";
                    //ViewBag.Path = statusCodeResult.OriginalPath;
                    //ViewBag.QueryString = statusCodeResult.OriginalQueryString;
                    break;
                case 500:
                    ViewBag.ErrorMessage = "抱歉，页面发生了错误";
                    break;
            }

            return View("NotFound");
        }


        [Route("/Error")]
        public IActionResult Error()
        {
            var exceptionHanlderPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionHanlderPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHanlderPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHanlderPathFeature.Error.StackTrace;

            logger.LogError($"路径{exceptionHanlderPathFeature.Path}产生了一个错误{exceptionHanlderPathFeature.Error}");

            return View();
        }
    }
}
