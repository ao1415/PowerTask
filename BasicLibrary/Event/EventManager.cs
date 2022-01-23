using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Event
{
    public class EventManager<Sender, Args>
    {
        private readonly string _name;

        public delegate void EventHandler<T, U>(Sender sender, Args e);
        private event EventHandler<Sender, Args>? Event;

        private readonly Dictionary<EventHandler<Sender, Args>, bool> _lock = new();

        public EventManager(string name)
        {
            _name = name;
        }

        /// <summary>
        /// キー押下イベント追加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="event"></param>
        /// <exception cref="ArgumentException"></exception>
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
        public void Invoke(Sender sender, Args e)
        {
            Event?.Invoke(sender, e);
        }
    }
}
