namespace BasicLibrary.Event
{
    /// <summary>1秒周期でイベントを発生させる</summary>
    public class ClockEventInvoker
    {
        /// <summary>インスタンス</summary>
        public static ClockEventInvoker Instance { get; } = new();

        private readonly System.Timers.Timer _timer;

        public EventManager<object, EventArgs> Clock { get; } = new("定期イベント");

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
                Clock.Invoke(this, EventArgs.Empty);
                _timer.Interval = GetInterval();
            };

            _timer.Start();
        }

        ~ClockEventInvoker()
        {
            _timer?.Stop();
        }
    }
}
