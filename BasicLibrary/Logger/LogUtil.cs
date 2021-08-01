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
        /// �����ݒ�
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
        /// �f�o�b�O���O
        /// </summary>
        /// <param name="message">���O���b�Z�[�W</param>
        public static void Debug(string message)
        {
            Log.Debug(message);
        }
        /// <summary>
        /// �g���[�X���O
        /// </summary>
        /// <param name="message">���O���b�Z�[�W</param>
        public static void Trace(string message)
        {
            Log.Verbose(message);
        }
        /// <summary>
        /// ��񃍃O
        /// </summary>
        /// <param name="message">���O���b�Z�[�W</param>
        public static void Information(string message)
        {
            Log.Information(message);
        }
        /// <summary>
        /// �x�����O
        /// </summary>
        /// <param name="message">���O���b�Z�[�W</param>
        public static void Warning(string message)
        {
            Log.Warning(message);
        }
        /// <summary>
        /// �G���[���O
        /// </summary>
        /// <param name="message">���O���b�Z�[�W</param>
        public static void Error(string message)
        {
            Log.Error(message);
        }
        /// <summary>
        /// �G���[���O
        /// </summary>
        /// <param name="e">��O</param>
        /// <param name="message">���O���b�Z�[�W</param>
        public static void Error(Exception e, string message)
        {
            Log.Error(e, message);
        }
    }
}
