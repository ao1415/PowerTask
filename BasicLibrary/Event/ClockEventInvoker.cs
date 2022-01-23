namespace BasicLibrary.Event
{
    /// <summary>1秒周期でイベントを発生させる</summary>
    public class ClockEventInvoker
    {
        /// <summary>インスタンス</summary>
        public static ClockEventInvoker Instance { get; } = new();

        private readonly System.Timers.Timer _timer;

        /// <summary>イベント</summary>
        private event EventHandler? Clock = null;

        /// <summary>イベント重複ガード</summary>
        private readonly Dictionary<string, bool> _raiseLock = new();

        /// <summary>タイマーインターバル取得</summary>
        /// <returns></returns>
        private static double GetInterval()
        {
            return 1001 - DateTime.Now.Millisecond;
        }

        private ClockEventInvoker()
        {
            _timer = new System.Timers.Timer
            {
                Interval = GetInterval(),
                AutoReset = true
            };
            _timer.Elapsed += (sender, e) =>
            {
                Instance.RaiseEvent();
                _timer.Interval = GetInterval();
            };

            _timer.Start();
        }

        ~ClockEventInvoker()
        {
            _timer?.Stop();
        }

        /// <summary>
        /// 定期イベント追加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="event"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddEvent(string name, EventHandler @event)
        {
            if (_raiseLock.ContainsKey(name))
            {
                throw new ArgumentException($"名前が重複しています:{name}");
            }

            _raiseLock.Add(name, false);
            Clock += async (sender, e) =>
            {
                if (!_raiseLock[name])
                {
                    _raiseLock[name] = true;
                    await Task.Run(() => { @event(sender, e); });
                    _raiseLock[name] = false;
                }
            };

            Logger.Logger.Log.Information($"定期イベント登録:{name}");

        }

        /// <summary>
        /// イベント実行
        /// </summary>
        public void RaiseEvent()
        {
            Clock?.Invoke(this, EventArgs.Empty);
        }
    }
}
