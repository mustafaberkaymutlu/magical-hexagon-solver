using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;

namespace MagicalHexagonSolver.Models
{
    public class ElapsedTimer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TimeElapsed { get; set; } = "N/A";

        private DispatcherTimer _timer;
        private Stopwatch _stopWatch;

        public void StartTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += dispatcherTimerTick_;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        private void dispatcherTimerTick_(object sender, EventArgs e)
        {
            TimeElapsed = $"{_stopWatch.Elapsed.TotalSeconds:0.00}" + " sec.";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TimeElapsed"));
        }
    }
}
