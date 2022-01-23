using BasicLibrary;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace WinCron
{
    internal static class ShellExecuter
    {
        public static bool Exec(string path, string param = "")
        {
            try
            {
                string fullPath = Path.GetFullPath(path);
                string workspace = Path.GetDirectoryName(fullPath) ?? string.Empty;
                string file = Path.GetFileName(path);

                using Runspace rs = RunspaceFactory.CreateRunspace();
                using PowerShell ps = PowerShell.Create();

                PSCommand pscmd = new();
                pscmd.AddScript($"cd {workspace}");
                pscmd.AddScript($".\\{file} {param}");

                rs.Open();
                ps.Commands = pscmd;
                ps.Runspace = rs;

                Logger.Log.Verbose($"コマンド：{string.Join(",", pscmd.Commands)}");

                try
                {
                    var results = ps.Invoke();

                    foreach (var stream in ps.Streams.Information)
                    {
                        Logger.Log.Debug($"実行情報：{stream}");
                    }
                    foreach (var stream in ps.Streams.Error)
                    {
                        Logger.Log.Warning($"実行エラー：{stream}");
                    }

                    if (results.Count > 0)
                    {
                        Logger.Log.Information("実行スクリプト：" + file);
                        foreach (var res in results)
                        {
                            Logger.Log.Information("実行結果：" + res);
                        }
                    }
                }
                catch (PSSecurityException)
                {
                    Logger.Log.Error("スクリプト実行を許可してください");
                    Logger.Log.Error("Set-ExecutionPolicy RemoteSigned -Scope CurrentUser");
                    return false;
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("PowerShellの実行に失敗しました", e);
                return false;
            }

            return true;
        }
    }
}
