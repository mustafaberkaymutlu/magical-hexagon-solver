using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            List<Row> finalBoards = e.Result as List<Row>;

            if (finalBoards != null)
            {
                listView_steps.ItemsSource = finalBoards;

                MessageBox.Show("Result found!");
            }
            else
            {
                MessageBox.Show("Can not found the result.");
            }


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
            else
            {
                MessageBox.Show("Solver already working.");
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            solverWorker.CancelAsync();
        }
    }
}
