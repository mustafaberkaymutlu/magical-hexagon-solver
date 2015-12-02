using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;

namespace YapayZekaOdevi2.Models
{
    public class ElapsedTimer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TimeElapsed { get; set; }

        private DispatcherTimer timer;
        private Stopwatch stopWatch;

        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += dispatcherTimerTick_;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            stopWatch = new Stopwatch();
            stopWatch.Start();
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        private void dispatcherTimerTick_(object sender, EventArgs e)
        {
            TimeElapsed = string.Format("{0:0.00}", stopWatch.Elapsed.TotalSeconds) + " sec.";
            PropertyChanged(this, new PropertyChangedEventArgs("TimeElapsed"));
        }
    }
}
