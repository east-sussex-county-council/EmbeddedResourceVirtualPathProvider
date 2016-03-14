using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exceptionless;

namespace EmbeddedResourceVirtualPathProvider
{
    public static class Logger
    {
        static Logger()
        {
            LogWarning = (message, ex) =>
            {
                ex.Data.Add("Message", message);
                ex.ToExceptionless().Submit();
            };
        }

        public static Action<string, Exception> LogWarning { get; set; }
    }
}
