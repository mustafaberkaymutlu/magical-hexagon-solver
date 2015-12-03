using System;
using System.ComponentModel;
using System.Windows;
using MagicalHexagonSolver.Models;

namespace MagicalHexagonSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _solverWorker = new BackgroundWorker();
        private readonly BackgroundWorker _testWorker = new BackgroundWorker();
        private readonly ElapsedTimer _elapsedTimer = new ElapsedTimer();

        
        public MainWindow()
        {
            InitializeComponent();

            _solverWorker.WorkerSupportsCancellation = true;
            _solverWorker.DoWork += new DoWorkEventHandler(solverWorker_DoWork);
            _solverWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(solverWorker_RunWorkerCompleted);

            _testWorker.DoWork += new DoWorkEventHandler(testWorker_DoWork);
            _testWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(testWorker_RunWorkerCompleted);

            LabelElapsedTimer.DataContext = _elapsedTimer;
        }

        private void testWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SmallResult result = e.Result as SmallResult;
            
            Console.WriteLine("Ortalama iterasyon sayisi: {0:G}", result.IterationCount / Config.TEST_MODE_ITERATION_COUNT);
            Console.WriteLine("Bulma yuzdesi " + (double)result.FoundCount / Config.TEST_MODE_ITERATION_COUNT);

            LabelWorking.Content = "No";
            TxtBoxK.IsEnabled = true;
            BtnStart.IsEnabled = true;
            BtnCancel.IsEnabled = false;
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
                iterationCount += result.FoundIterationNumber;

                if (result.SolutionIsFound)
                    foundCount++;
            }

            e.Result = new SmallResult(iterationCount, foundCount);
        }

        private void solverWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result result = e.Result as Result;

            TxtBoxK.IsEnabled = true;
            BtnStart.IsEnabled = true;
            BtnCancel.IsEnabled = false;

            _elapsedTimer.StopTimer();

            LabelWorking.Content = "No";

            if (result != null && result.SolverIsCancelled)
            {
                LabelCancelled.Content = "Yes";
            }
            else
            {
                LabelCancelled.Content = "No";
            }

            if (result != null && result.SolutionIsFound)
            {
                LabelFoundSolution.Content = "Yes";
                LabelFoundIterationNumber.Content = result.FoundIterationNumber.ToString();
                LabelFoundKNumber.Content = result.FoundKNumber.ToString();
            }
            else
            {
                LabelFoundSolution.Content = "No";
                LabelFoundIterationNumber.Content = "N/A";
                LabelFoundKNumber.Content = "N/A";
            }

            if (result != null) ListViewSteps.ItemsSource = result.Rows;
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
                LabelWorking.Content = "TEST";
                TxtBoxK.IsEnabled = false;
                BtnStart.IsEnabled = false;
                BtnCancel.IsEnabled = false;

                _testWorker.RunWorkerAsync(ushort.Parse(TxtBoxK.Text));
            }
            else
            {
                TxtBoxK.IsEnabled = false;
                BtnStart.IsEnabled = false;
                BtnCancel.IsEnabled = true;
                ListViewSteps.ItemsSource = null;
                LabelWorking.Content = "Yes";
                LabelCancelled.Content = "N/A";
                LabelFoundSolution.Content = "N/A";

                if (_solverWorker.IsBusy != true)
                {
                    if (!string.IsNullOrWhiteSpace(TxtBoxK.Text))
                    {
                        _solverWorker.RunWorkerAsync(ushort.Parse(TxtBoxK.Text));
                        _elapsedTimer.StartTimer();
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
            if(_solverWorker.IsBusy)
                _solverWorker.CancelAsync();
        }

        private class SmallResult{
            public readonly uint IterationCount;
            public readonly uint FoundCount;

            public SmallResult(uint iterationCount, uint foundCount)
            {
                IterationCount = iterationCount;
                FoundCount = foundCount;
            }
        }
    }
    
}
