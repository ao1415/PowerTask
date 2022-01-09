using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.WindowsAPI
{
    /// <summary>
    /// <see href="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/ns-winuser-kbdllhookstruct?redirectedfrom=MSDN">
    /// 低レベルのキーボード入力イベント情報
    /// </see>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class KBDLLHOOKSTRUCT
    {
        /// <summary>仮想キーコード</summary>
        public uint vkCode;
        /// <summary>キーのハードウェアスキャンコード</summary>
        public uint scanCode;
        /// <summary>キーフラグ</summary>
        public uint flags;
        /// <summary>タイムスタンプ</summary>
        public uint time;
        /// <summary>メッセージに関連する追加情報</summary>
        public UIntPtr dwExtraInfo;
    }

}
