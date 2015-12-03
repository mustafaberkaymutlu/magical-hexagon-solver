using System;
using System.ComponentModel;
using System.Windows;
using YapayZekaOdevi2.Models;

namespace YapayZekaOdevi2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker solverWorker = new BackgroundWorker();
        private BackgroundWorker testWorker = new BackgroundWorker();
        private ElapsedTimer elapsedTimer = new ElapsedTimer();

        
        public MainWindow()
        {
            InitializeComponent();

            solverWorker.WorkerSupportsCancellation = true;
            solverWorker.DoWork += new DoWorkEventHandler(solverWorker_DoWork);
            solverWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(solverWorker_RunWorkerCompleted);

            testWorker.DoWork += new DoWorkEventHandler(testWorker_DoWork);
            testWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(testWorker_RunWorkerCompleted);

            label_elapsedTimer.DataContext = elapsedTimer;
        }

        private void testWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SmallResult result = e.Result as SmallResult;
            
            Console.WriteLine("Ortalama iterasyon sayisi: {0:G}", result.iterationCount / Config.TEST_MODE_ITERATION_COUNT);
            Console.WriteLine("Bulma yuzdesi " + (double)result.foundCount / Config.TEST_MODE_ITERATION_COUNT);

            label_working.Content = "No";
            txtBox_k.IsEnabled = true;
            btn_start.IsEnabled = true;
            btn_cancel.IsEnabled = false;
        }

        private void testWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            HillClimbing hillClimbing = new HillClimbing();
            uint iterationCount = 0;
            uint foundCount = 0;
            for (int i = 0; i < Config.TEST_MODE_ITERATION_COUNT; i++)
            {
                Console.WriteLine("{0:G}. deneme yapiliyor..", i);
                Result result = hillClimbing.FindMaximum(worker, (ushort)e.Argument);
                iterationCount += result.foundIterationNumber;

                if (result.solutionIsFound)
                    foundCount++;
            }

            e.Result = new SmallResult(iterationCount, foundCount);
        }

        private void solverWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result result = e.Result as Result;

            txtBox_k.IsEnabled = true;
            btn_start.IsEnabled = true;
            btn_cancel.IsEnabled = false;

            elapsedTimer.StopTimer();

            label_working.Content = "No";

            if (result.solverIsCancelled)
            {
                label_cancelled.Content = "Yes";
            }
            else
            {
                label_cancelled.Content = "No";
            }

            if (result.solutionIsFound)
            {
                label_foundSolution.Content = "Yes";
                label_foundIterationNumber.Content = result.foundIterationNumber.ToString();
                label_foundKNumber.Content = result.foundKNumber.ToString();
            }
            else
            {
                label_foundSolution.Content = "No";
                label_foundIterationNumber.Content = "N/A";
                label_foundKNumber.Content = "N/A";
            }

            listView_steps.ItemsSource = result.rows;
        }

        private void solverWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            HillClimbing hillClimbing = new HillClimbing();
            Result result = hillClimbing.FindMaximum(worker, (ushort)e.Argument);

            e.Result = result;
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (Config.TEST_MODE)
            {
                label_working.Content = "TEST";
                txtBox_k.IsEnabled = false;
                btn_start.IsEnabled = false;
                btn_cancel.IsEnabled = false;

                testWorker.RunWorkerAsync(ushort.Parse(txtBox_k.Text));
            }
            else
            {
                txtBox_k.IsEnabled = false;
                btn_start.IsEnabled = false;
                btn_cancel.IsEnabled = true;
                listView_steps.ItemsSource = null;
                label_working.Content = "Yes";
                label_cancelled.Content = "N/A";
                label_foundSolution.Content = "N/A";

                if (solverWorker.IsBusy != true)
                {
                    if (!String.IsNullOrWhiteSpace(txtBox_k.Text))
                    {
                        solverWorker.RunWorkerAsync(ushort.Parse(txtBox_k.Text));
                        elapsedTimer.StartTimer();
                    }
                    else
                    {
                        MessageBox.Show("Please enter the K value.");
                    }
                }
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if(solverWorker.IsBusy)
                solverWorker.CancelAsync();
        }

        private class SmallResult{
            public uint iterationCount;
            public uint foundCount;

            public SmallResult(uint iterationCount, uint foundCount)
            {
                this.iterationCount = iterationCount;
                this.foundCount = foundCount;
            }
        }
    }
    
}
