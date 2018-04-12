using System;
using System.IO;
using System.Reflection;

namespace Core.Bootstrap
{
    /// <summary>
    /// Simple txt file logger.
    /// Add bootstraplog.log next to the Core.dll
    /// </summary>
    public static class BootstrapLogger
    {
        public static void Log(
            string message,
            [System.Runtime.CompilerServices.CallerMemberName]string objectName = "", 
            [System.Runtime.CompilerServices.CallerLineNumber]int lineNumber = 0)
        {
            var assemblyPath = typeof(BootstrapLogger).GetTypeInfo().Assembly.Location;
            var path = Path.GetDirectoryName(assemblyPath) + "\\bootstraplog.log";

            // Need to create the file manually
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                // Only write if the log file is manually created
                using (var fileStream = new FileStream(path, FileMode.Append))
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine($"{DateTimeOffset.Now} - Caller: {objectName} : Line: {lineNumber} -- {message}");
                }
            }
            catch (Exception)
            {
                // Swallow the exception 
            }
        }
    }
}
