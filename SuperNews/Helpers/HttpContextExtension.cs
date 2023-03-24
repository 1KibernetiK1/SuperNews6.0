using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SuperNews.Helpers
{
    public static class HttpContextExtension
    {
        public static void SaveToSession<T>(this HttpContext httpContext, T subject)
           where T : class, new()
        {
            string json = JsonConvert.SerializeObject(subject);
            httpContext.Session.SetString(typeof(T).Name, json);
        }

        public static T LoadFromSession<T>(this HttpContext httpContext)
            where T : class, new()
        {
            T subject = null;
            string json = httpContext.Session.GetString(typeof(T).Name);
            if (string.IsNullOrEmpty(json))
            {
                subject = new T();
            }
            else
            {
                subject = JsonConvert.DeserializeObject<T>(json);
            }
            return subject;
        }
    }
}
