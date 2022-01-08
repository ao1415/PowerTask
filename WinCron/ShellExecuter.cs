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

                Logger.Verbose($"[WinCron]コマンド：{string.Join(",", pscmd.Commands)}");

                try
                {
                    var results = ps.Invoke();

                    foreach (var stream in ps.Streams.Information)
                    {
                        Logger.Debug($"[WinCron]実行情報：{stream}");
                    }
                    foreach (var stream in ps.Streams.Error)
                    {
                        Logger.Warning($"[WinCron]実行エラー：{stream}");
                    }

                    if (results.Count > 0)
                    {
                        Logger.Information("[WinCron]実行スクリプト：" + file);
                        foreach (var res in results)
                        {
                            Logger.Information("[WinCron]実行結果：" + res);
                        }
                    }
                }
                catch (PSSecurityException)
                {
                    Logger.Error("[WinCron]スクリプト実行を許可してください");
                    Logger.Error("[WinCron]Set-ExecutionPolicy RemoteSigned -Scope CurrentUser");
                    return false;
                }
            }
            catch (Exception e)
            {
                Logger.Error("[WinCron]PowerShellの実行に失敗しました", e);
                return false;
            }

            return true;
        }
    }
}
