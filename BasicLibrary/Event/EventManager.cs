using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Event
{
    /// <summary>
    /// <para>イベントマネージャー</para>
    /// <para>イベントの重複発生を行わない</para>
    /// </summary>
    /// <typeparam name="Sender"></typeparam>
    /// <typeparam name="Args"></typeparam>
    public class EventManager<Sender, Args>
    {
        private readonly string _name;

        public delegate void EventHandler<T, U>(Sender sender, Args e);
        private event EventHandler<Sender, Args>? Event;

        private readonly Dictionary<EventHandler<Sender, Args>, bool> _lock = new();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">イベント名</param>
        public EventManager(string name)
        {
            _name = name;
        }

        /// <summary>
        /// イベント追加
        /// </summary>
        /// <param name="event">デリゲート</param>
        public void AddEvent(EventHandler<Sender, Args> @event)
        {
            string registName = $"{@event.Method?.DeclaringType?.FullName}.{@event.Method?.Name}";

            _lock.Add(@event, false);
            Event += async (sender, e) =>
            {
                if (!_lock[@event])
                {
                    _lock[@event] = true;
                    await Task.Run(() => { @event(sender, e); });
                    _lock[@event] = false;
                }
            };

            Logger.Logger.Log.Information($"[{_name}]イベント登録:{registName}");
        }

        /// <summary>
        /// イベント実行
        /// </summary>
        /// <param name="sender">呼び出しオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void Invoke(Sender sender, Args e)
        {
            Event?.Invoke(sender, e);
        }
    }
}
