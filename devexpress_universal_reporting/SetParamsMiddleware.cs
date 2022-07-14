using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace Devexpress_Universal_Reporting
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SetParamsMiddleware
    {
        private readonly RequestDelegate _next;

        public SetParamsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value.Contains("DXXRDV") && httpContext.Request.Method == "POST" && !string.IsNullOrEmpty(httpContext.Request.Form["actionKey"]))
            {
                if (httpContext.Request.Form["actionKey"].Equals("startBuild"))
                {
                    var fcnvc = httpContext.Request.Form;
                    Dictionary<string, StringValues> dictValues = new Dictionary<string, StringValues>();
                    foreach (var key in fcnvc.Keys)
                    {
                        if (key == "arg")
                        {
                            string value = WebUtility.UrlDecode(fcnvc[key]);
                            string valueBeReplace = "", newValue = "";
                            var jobj = JObject.Parse(value);
                            if (jobj["parameters"] != null)
                            {
                                for (int i = 0; i < jobj["parameters"].Count(); i++)
                                {
                                    if (jobj["parameters"][i]["Key"].ToString() == "tanentid")
                                    {
                                        valueBeReplace = jobj["parameters"][i]["Value"].ToString();
                                        var val = JObject.Parse(httpContext.Request.Headers["X-userinfo"].ToString());
                                        newValue = val["tenantId"].ToString();
                                        if (!string.IsNullOrEmpty(fcnvc[key]) && !string.IsNullOrEmpty(valueBeReplace))
                                        {
                                            value = fcnvc[key].ToString().Replace(valueBeReplace, newValue);
                                        }
                                    }
                                    //else if (jobj["parameters"][i]["Key"].ToString() == "manageremailid")
                                    //{
                                    //    valueBeReplace = jobj["parameters"][i]["Value"].ToString();
                                    //    var val = JObject.Parse(httpContext.Request.Headers["X-User-Info"].ToString());
                                    //    newValue = val["manageremailId"].ToString();
                                    //    if (!string.IsNullOrEmpty(fcnvc[key]) && !string.IsNullOrEmpty(valueBeReplace))
                                    //    {
                                    //        value = fcnvc[key].ToString().Replace(valueBeReplace, newValue);
                                    //    }
                                    //}

                                }

                            }
                            dictValues.Add(key, value);
                        }
                        else
                        {
                            dictValues.Add(key, fcnvc[key]);
                        }
                    }

                    var fc = new FormCollection(dictValues);
                    httpContext.Request.Form = fc;
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SetParamsMiddlewareExtensions
    {
        public static IApplicationBuilder UseSetParamsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetParamsMiddleware>();
        }
    }
}
