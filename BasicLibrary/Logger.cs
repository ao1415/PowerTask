using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace BasicLibrary
{
    public static class Logger
    {
        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize()
        {
            string template = "{Timestamp:yyyy/MM/dd HH:mm:ss.fff} [{Level:u4}][{ThreadId:00}] {Message:j}{NewLine}{Exception}";
            string logFilePathHead = @"logs\log";
            long fileSizeLimitBytes = 1024 * 1024 * 10; // 10MB

            Log.Logger = new LoggerConfiguration()
                            .Enrich.WithThreadId()
                            .Enrich.WithExceptionDetails()
                            .MinimumLevel.Verbose()
                            .WriteTo.Debug(outputTemplate: template)
                            .WriteTo.File($"{logFilePathHead}.log", LogEventLevel.Verbose, outputTemplate: template, rollOnFileSizeLimit: true, fileSizeLimitBytes: fileSizeLimitBytes, retainedFileCountLimit: null)
                            .CreateLogger();
        }


        /// <summary>Log Level 1</summary>
        public static void Verbose(string messageTemplate)
        {
            Log.Verbose(messageTemplate);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose<T>(string messageTemplate, T propertyValue)
        {
            Log.Verbose(messageTemplate, propertyValue);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Verbose(messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(messageTemplate, propertyValues);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose(Exception exception, string messageTemplate)
        {
            Log.Verbose(exception, messageTemplate);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Verbose(exception, messageTemplate, propertyValue);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 1</summary>
        public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(exception, messageTemplate, propertyValues);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug(string messageTemplate)
        {
            Log.Debug(messageTemplate);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug<T>(string messageTemplate, T propertyValue)
        {
            Log.Debug(messageTemplate, propertyValue);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Debug(messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug(string messageTemplate, params object[] propertyValues)
        {
            Log.Debug(messageTemplate, propertyValues);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug(Exception exception, string messageTemplate)
        {
            Log.Debug(exception, messageTemplate);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Debug(exception, messageTemplate, propertyValue);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Debug(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 2</summary>
        public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Debug(exception, messageTemplate, propertyValues);
        }

        /// <summary>Log Level 3</summary>
        public static void Information(string messageTemplate)
        {
            Log.Information(messageTemplate);
        }

        /// <summary>Log Level 3</summary>
        public static void Information<T>(string messageTemplate, T propertyValue)
        {
            Log.Information(messageTemplate, propertyValue);
        }

        /// <summary>Log Level 3</summary>
        public static void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Information(messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 3</summary>
        public static void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 3</summary>
        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            Log.Information(messageTemplate, propertyValues);
        }

        /// <summary>Log Level 3</summary>
        public static void Information(Exception exception, string messageTemplate)
        {
            Log.Information(exception, messageTemplate);
        }

        /// <summary>Log Level 3</summary>
        public static void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Information(exception, messageTemplate, propertyValue);
        }

        /// <summary>Log Level 3</summary>
        public static void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Information(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 3</summary>
        public static void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 3</summary>
        public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Information(exception, messageTemplate, propertyValues);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning(string messageTemplate)
        {
            Log.Warning(messageTemplate);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning<T>(string messageTemplate, T propertyValue)
        {
            Log.Warning(messageTemplate, propertyValue);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Warning(messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning(string messageTemplate, params object[] propertyValues)
        {
            Log.Warning(messageTemplate, propertyValues);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning(Exception exception, string messageTemplate)
        {
            Log.Warning(exception, messageTemplate);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Warning(exception, messageTemplate, propertyValue);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 4</summary>
        public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Warning(exception, messageTemplate, propertyValues);
        }

        /// <summary>Log Level 5</summary>
        public static void Error(string messageTemplate)
        {
            Log.Error(messageTemplate);
        }

        /// <summary>Log Level 5</summary>
        public static void Error<T>(string messageTemplate, T propertyValue)
        {
            Log.Error(messageTemplate, propertyValue);
        }

        /// <summary>Log Level 5</summary>
        public static void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Error(messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 5</summary>
        public static void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 5</summary>
        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Log.Error(messageTemplate, propertyValues);
        }

        /// <summary>Log Level 5</summary>
        public static void Error(Exception exception, string messageTemplate)
        {
            Log.Error(exception, messageTemplate);
        }

        /// <summary>Log Level 5</summary>
        public static void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Error(exception, messageTemplate, propertyValue);
        }

        /// <summary>Log Level 5</summary>
        public static void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Error(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 5</summary>
        public static void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 5</summary>
        public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Error(exception, messageTemplate, propertyValues);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal(string messageTemplate)
        {
            Log.Fatal(messageTemplate);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal<T>(string messageTemplate, T propertyValue)
        {
            Log.Fatal(messageTemplate, propertyValue);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Fatal(messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Log.Fatal(messageTemplate, propertyValues);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal(Exception exception, string messageTemplate)
        {
            Log.Fatal(exception, messageTemplate);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Fatal(exception, messageTemplate, propertyValue);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 6</summary>
        public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}
