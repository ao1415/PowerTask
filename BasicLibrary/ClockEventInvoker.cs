namespace BasicLibrary
{
    /// <summary>1秒周期でイベントを発生させる</summary>
    public class ClockEventInvoker
    {
        /// <summary>インスタンス</summary>
        public static ClockEventInvoker Instance { get; } = new();

        /// <summary>イベント</summary>
        private event EventHandler? Clock = null;

        /// <summary>イベント重複ガード</summary>
        private readonly Dictionary<string, bool> _raiseLock = new();

        private ClockEventInvoker()
        {

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
