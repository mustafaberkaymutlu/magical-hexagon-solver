using System;
using System.Collections.Generic;
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
        private ElapsedTimer elapsedTimer = new ElapsedTimer();

        public MainWindow()
        {
            InitializeComponent();

            solverWorker.WorkerSupportsCancellation = true;
            solverWorker.DoWork += new DoWorkEventHandler(solverWorker_DoWork);
            solverWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(solverWorker_RunWorkerCompleted);
            
            label_elapsedTimer.DataContext = elapsedTimer;
        }

        private void solverWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result result = e.Result as Result;

            txtBox_k.IsEnabled = true;
            btn_start.IsEnabled = true;
            btn_cancel.IsEnabled = false;

            elapsedTimer.StopTimer();

            label_working.Content = "No";
            
            if (result.isCancelled)
            {
                label_foundSolution.Content = "No";
                label_cancelled.Content = "Yes";
            }
            else
            {
                label_foundSolution.Content = "Yes";
                label_cancelled.Content = "No";
                label_foundIterationNumber.Content = result.foundIterationNumber.ToString();
                label_foundKNumber.Content = result.foundKNumber.ToString();
            }
            
            listView_steps.ItemsSource = result.rows;
        }

        private void solverWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            HillClimbing hillClimbing = new HillClimbing();
            Result result = hillClimbing.FindLocalMaximum(worker, (ushort)e.Argument);

            e.Result = result;
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
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

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            solverWorker.CancelAsync();
        }
    }
}
