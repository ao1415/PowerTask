using BasicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WinCapt
{
    internal class PathUtils
    {
        private const string TagVideos = "%videos%";
        private const string TagPictures = "%pictures%";

        private const string TagTitle = "%title%";
        private const string TagDate = "%date.*%";

        private readonly string _folderPath;
        private readonly string _fileFormat;

        public PathUtils(string folderPathTemplate, string fileNameTemplate)
        {
            _folderPath = GetFolderPath(folderPathTemplate);
            _fileFormat = GetFileFormat(fileNameTemplate);
        }

        /// <summary>
        /// 保存ファイルパス取得
        /// </summary>
        /// <param name="windowTitle">ウィンドウタイトル</param>
        /// <returns></returns>
        public string? GetSaveFilePath(StringBuilder title)
        {
            char[] invalidch = Path.GetInvalidFileNameChars();

            foreach (char ch in invalidch)
            {
                title.Replace(ch.ToString(), string.Empty);
            }
            string fileName = string.Format(_fileFormat, title.ToString(), DateTime.Now);

            try
            {
                Path.GetFullPath(fileName);
            }
            catch (Exception ex)
            {
                Logger.Warning(ex, $"[WinCpat]使用できないファイル名です:{fileName}");
                return null;
            }

            return Path.Combine(_folderPath, fileName);
        }


        /// <summary>
        /// 文字列変換
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        private static string Replace(string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// フォルダ名変換
        /// </summary>
        /// <returns></returns>
        private static string GetFolderPath(string folderPathTemplate)
        {
            string path = folderPathTemplate;
            path = Replace(path, TagVideos, System.Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            path = Replace(path, TagPictures, System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

            return path;
        }
        /// <summary>
        /// ファイル名変換
        /// </summary>
        /// <returns></returns>
        private string GetFileFormat(string fileNameTemplate)
        {
            string format = fileNameTemplate;
            format = Replace(format, TagTitle, "{0}");
            Match dateMatch = Regex.Match(format, TagDate, RegexOptions.IgnoreCase);
            if (dateMatch.Success)
            {
                string dateFormat;

                string value = dateMatch.Value;
                int index = value.IndexOf(':');
                if (0 <= index)
                {
                    dateFormat = $"{{1{value[index..(value.Length - 1)]}}}";
                }
                else
                {
                    dateFormat = "{1}";
                }
                format = Replace(format, TagDate, dateFormat);
            }

            return format;
        }

    }
}
