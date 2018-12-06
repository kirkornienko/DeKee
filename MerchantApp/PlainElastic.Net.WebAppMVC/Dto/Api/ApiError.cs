using System;

namespace PlainElastic.Net.WebAppMVC.Dto.Api
{
    public class ApiError
    {
        public ApiError()
        { }
        public ApiError(Exception e)
        {
            Code = e.GetType().Name;
            Description = GetMessage(e);
        }

        private static string GetMessage(Exception e)
        {
            return e != null ? FormatParticularException(e) + "\n" + GetMessage(e.InnerException) : "";
        }

        private static string FormatParticularException(Exception e)
        {
            return e.Message;
        }

        public static class Codes {
            public static readonly string B4UAuthorize = "B4UAuthorize";
        }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}