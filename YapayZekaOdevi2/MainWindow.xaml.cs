using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace YapayZekaOdevi2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker solverWorker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            solverWorker.WorkerSupportsCancellation = true;
            solverWorker.DoWork += new DoWorkEventHandler(solverWorker_DoWork);
            solverWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(solverWorker_RunWorkerCompleted);
        }

        private void solverWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtBox_k.IsEnabled = true;
            btn_start.IsEnabled = true;
            btn_cancel.IsEnabled = false;

            List<Row> finalBoards = e.Result as List<Row>;
            
            listView_steps.ItemsSource = finalBoards;
        }

        private void solverWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            HillClimbing hillClimbing = new HillClimbing();
            List<Row> finalBoards = hillClimbing.FindLocalMaximum(worker, (byte)e.Argument);

            e.Result = finalBoards;
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            txtBox_k.IsEnabled = false;
            btn_start.IsEnabled = false;
            btn_cancel.IsEnabled = true;
            listView_steps.ItemsSource = null;

            if (solverWorker.IsBusy != true)
            {
                if (!String.IsNullOrWhiteSpace(txtBox_k.Text))
                {
                    solverWorker.RunWorkerAsync(Byte.Parse(txtBox_k.Text));
                }
                else
                {
                    MessageBox.Show("Lütfen K değerini giriniz.");
                }
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            solverWorker.CancelAsync();
        }
    }
}
