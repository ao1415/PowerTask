using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Toast
{
    /// <summary>トースト表示</summary>
    public class Toast
    {
        private static DateTime ExpirationTime { get; } = DateTime.Now.AddSeconds(3);

        /// <summary>
        /// トースト表示
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="message">本文</param>
        public static void Show(string title, string message)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(message)
                .Show(toast =>
                {
                    toast.ExpirationTime = ExpirationTime;
                });
        }

        /// <summary>
        /// トースト表示
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="path">画像パス</param>
        public static void Show(string title, Uri path)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddInlineImage(path)
                .Show(toast =>
                {
                    toast.ExpirationTime = ExpirationTime;
                });
        }
    }
}
