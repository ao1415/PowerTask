using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace BasicLibrary.Logger
{
    public class BasicLogger
    {
        /// <summary>ログフォルダ</summary>
        public static string FolderPath { get; } = @".\logs";

        /// <summary>ログファイル</summary>
        public static string FileName { get; } = @"PowerTask.log";

        /// <summary>
        /// 初期化
        /// </summary>
        public static void Initialize()
        {
            string template = "{Timestamp:yyyy/MM/dd HH:mm:ss.fff} [{Level:u4}][{ThreadId:00}] {Message:j}{NewLine}{Exception}";
            string logFilePath = Path.Combine(FolderPath, FileName);
            long fileSizeLimitBytes = 1024 * 1024 * 10; // 10MB

            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .Enrich.WithThreadId()
                            .Enrich.WithExceptionDetails()
                            .WriteTo.Debug(outputTemplate: template)
                            .WriteTo.File(logFilePath, outputTemplate: template, rollOnFileSizeLimit: true, fileSizeLimitBytes: fileSizeLimitBytes, retainedFileCountLimit: null)
                            .CreateLogger();
        }

        private readonly string _tag;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">ライブラリ名</param>
        public BasicLogger(string name)
        {
            _tag = $"[{name}]";
        }

        /// <summary>Log Level 1</summary>
        public void Verbose(string messageTemplate)
        {
            Log.Verbose($"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 1</summary>
        public void Verbose<T>(string messageTemplate, T propertyValue)
        {
            Log.Verbose($"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 1</summary>
        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Verbose($"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 1</summary>
        public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Verbose($"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 1</summary>
        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose($"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 1</summary>
        public void Verbose(Exception exception, string messageTemplate)
        {
            Log.Verbose(exception, $"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 1</summary>
        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Verbose(exception, $"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 1</summary>
        public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Verbose(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 1</summary>
        public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Verbose(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 1</summary>
        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(exception, $"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 2</summary>
        public void Debug(string messageTemplate)
        {
            Log.Debug($"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 2</summary>
        public void Debug<T>(string messageTemplate, T propertyValue)
        {
            Log.Debug($"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 2</summary>
        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Debug($"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 2</summary>
        public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Debug($"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 2</summary>
        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            Log.Debug($"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 2</summary>
        public void Debug(Exception exception, string messageTemplate)
        {
            Log.Debug(exception, $"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 2</summary>
        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Debug(exception, $"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 2</summary>
        public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Debug(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 2</summary>
        public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Debug(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 2</summary>
        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Debug(exception, $"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 3</summary>
        public void Information(string messageTemplate)
        {
            Log.Information($"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 3</summary>
        public void Information<T>(string messageTemplate, T propertyValue)
        {
            Log.Information($"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 3</summary>
        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Information($"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 3</summary>
        public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Information($"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 3</summary>
        public void Information(string messageTemplate, params object[] propertyValues)
        {
            Log.Information($"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 3</summary>
        public void Information(Exception exception, string messageTemplate)
        {
            Log.Information(exception, $"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 3</summary>
        public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Information(exception, $"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 3</summary>
        public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Information(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 3</summary>
        public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Information(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 3</summary>
        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Information(exception, $"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 4</summary>
        public void Warning(string messageTemplate)
        {
            Log.Warning($"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 4</summary>
        public void Warning<T>(string messageTemplate, T propertyValue)
        {
            Log.Warning($"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 4</summary>
        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Warning($"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 4</summary>
        public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Warning($"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 4</summary>
        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            Log.Warning($"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 4</summary>
        public void Warning(Exception exception, string messageTemplate)
        {
            Log.Warning(exception, $"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 4</summary>
        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Warning(exception, $"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 4</summary>
        public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Warning(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 4</summary>
        public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Warning(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 4</summary>
        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Warning(exception, $"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 5</summary>
        public void Error(string messageTemplate)
        {
            Log.Error($"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 5</summary>
        public void Error<T>(string messageTemplate, T propertyValue)
        {
            Log.Error($"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 5</summary>
        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Error($"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 5</summary>
        public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Error($"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 5</summary>
        public void Error(string messageTemplate, params object[] propertyValues)
        {
            Log.Error($"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 5</summary>
        public void Error(Exception exception, string messageTemplate)
        {
            Log.Error(exception, $"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 5</summary>
        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Error(exception, $"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 5</summary>
        public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Error(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 5</summary>
        public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Error(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 5</summary>
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Error(exception, $"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 6</summary>
        public void Fatal(string messageTemplate)
        {
            Log.Fatal($"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 6</summary>
        public void Fatal<T>(string messageTemplate, T propertyValue)
        {
            Log.Fatal($"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 6</summary>
        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Fatal($"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 6</summary>
        public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Fatal($"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 6</summary>
        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Log.Fatal($"{_tag}{messageTemplate}", propertyValues);
        }

        /// <summary>Log Level 6</summary>
        public void Fatal(Exception exception, string messageTemplate)
        {
            Log.Fatal(exception, $"{_tag}{messageTemplate}");
        }

        /// <summary>Log Level 6</summary>
        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Fatal(exception, $"{_tag}{messageTemplate}", propertyValue);
        }

        /// <summary>Log Level 6</summary>
        public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Log.Fatal(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1);
        }

        /// <summary>Log Level 6</summary>
        public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Fatal(exception, $"{_tag}{messageTemplate}", propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Log Level 6</summary>
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Fatal(exception, $"{_tag}{messageTemplate}", propertyValues);
        }
    }
}
