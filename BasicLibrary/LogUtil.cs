using System;
using Microsoft.Extensions.Configuration;
using System.IO;

using Serilog;
using Serilog.Exceptions;

namespace BasicLibrary
{
    public static class LogUtil
    {
        /// <summary>
        /// 初期設定
        /// </summary>
        static LogUtil()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithThreadName().Enrich.WithProperty("ThreadName", "__")
                .CreateLogger();

            /*
            string logFilePath = @"./Logs/PowerService.log";
            string template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u4}][{ThreadId:00}:{ThreadName}] {Message:lj}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                            .Enrich.WithThreadId()
                            .Enrich.WithThreadName().Enrich.WithProperty("ThreadName", "__")
                            .Enrich.WithExceptionDetails()
                            .MinimumLevel.Verbose()
                            .WriteTo.File(logFilePath, outputTemplate: template, rollOnFileSizeLimit: true, fileSizeLimitBytes: 104857600)
                            .CreateLogger();
            */
            System.Threading.Thread.CurrentThread.Name = "MN";
        }

        /// <summary>
        /// デバッグログ
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Debug(string message)
        {
            Log.Debug(message);
        }
        /// <summary>
        /// トレースログ
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Trace(string message)
        {
            Log.Verbose(message);
        }
        /// <summary>
        /// 情報ログ
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Information(string message)
        {
            Log.Information(message);
        }
        /// <summary>
        /// 警告ログ
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Warning(string message)
        {
            Log.Warning(message);
        }
        /// <summary>
        /// エラーログ
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Error(string message)
        {
            Log.Error(message);
        }
        /// <summary>
        /// エラーログ
        /// </summary>
        /// <param name="e">例外</param>
        /// <param name="message">ログメッセージ</param>
        public static void Error(Exception e, string message)
        {
            Log.Error(e, message);
        }
    }
}
