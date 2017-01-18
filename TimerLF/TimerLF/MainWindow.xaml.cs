using Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Windows.Threading;

namespace TimerLF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        string strTimer;
        DispatcherTimer timer;
        int intTimer = 0;
        DataSet result;
        DataTable dataTable;
        string[] recordWaktu, teamName;

        public MainWindow()
        {
            InitializeComponent();
            lblJudul.Content = "TECHNOCORNER 2017";
            recordWaktu = new string[4];
            teamName = new string[4];
        }

        private void btnRace_Click(object sender, RoutedEventArgs e)
        {
            lblJudul.Content = "RACE!";
            txtJudul.Text = "Mulai Perlombaan";
            txtJam.Text = "00";
            txtMenit.Text = "04";
            txtDetik.Text = "00";
            strTimer = txtJam.Text + txtMenit.Text + txtDetik.Text;
            int s = Int32.Parse(txtDetik.Text);
            int m = Int32.Parse(txtMenit.Text) * 60;
            int h = Int32.Parse(txtJam.Text) * 60 * 60;
            intTimer = s + m + h;
        }

        private void btnWarmup_Click(object sender, RoutedEventArgs e)
        {
            lblJudul.Content = "WARMING UP ";
            txtJudul.Text = "Warming Up";
            txtJam.Text = "00";
            txtMenit.Text = "00";
            txtDetik.Text = "30";
            strTimer = txtJam.Text + txtMenit.Text + txtDetik.Text;
            int s = Int32.Parse(txtDetik.Text);
            int m = Int32.Parse(txtMenit.Text) * 60;
            int h = Int32.Parse(txtJam.Text) * 60 * 60;
            intTimer = s + m + h;
        }

        private void btnCall_Click(object sender, RoutedEventArgs e)
        {
            lblJudul.Content = "CALLING TEAMS";
            txtJudul.Text = "Pemanggilan Team";
            txtJam.Text = "00";
            txtMenit.Text = "00";
            txtDetik.Text = "30";
            strTimer = txtJam.Text + txtMenit.Text + txtDetik.Text;
            int s = Int32.Parse(txtDetik.Text);
            int m = Int32.Parse(txtMenit.Text) * 60;
            int h = Int32.Parse(txtJam.Text) * 60 * 60;
            intTimer = s + m + h;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (txtDetik.Text == "00" && txtMenit.Text == "00" && txtJam.Text == "00")
            {
                MessageBox.Show("Set your timer first!");
            } else
            {
                if (intTimer == 0)
                {
                    strTimer = txtJam.Text + ":" + txtMenit.Text + ":" + txtDetik.Text;
                    lblTimer.Content = strTimer;
                    int s = Int32.Parse(txtDetik.Text);
                    int m = Int32.Parse(txtMenit.Text) * 60;
                    int h = Int32.Parse(txtJam.Text) * 60 * 60;
                    intTimer = s + m + h;
                }
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
                btnStart.IsEnabled = false;
                btnPause.IsEnabled = true;
                btnStop.IsEnabled = true;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            intTimer--;
            int s = intTimer % 60;
            int m = intTimer / 60 % 60;
            int h = intTimer / (60 * 60);
            lblTimer.Content = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            btnStart.Content = "RESUME";
            btnStart.IsEnabled = true;
            btnPause.IsEnabled = false;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            intTimer = 0;
            btnStart.Content = "START";
            btnStart.IsEnabled = true;
            btnPause.IsEnabled = false;
            btnStop.IsEnabled = false;
            lblTimer.Content = "00:00:00";
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to reset this timer?", "Reset Timer", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    if (timer != null)
                    {
                        if (timer.IsEnabled == true)
                        {
                            timer.Stop();
                        }
                    }
                    intTimer = 0;
                    btnStart.Content = "START";
                    btnStart.IsEnabled = true;
                    btnPause.IsEnabled = false;
                    btnStop.IsEnabled = false;
                    lblJudul.Content = "TECHNOCORNER 2017";
                    lblTimer.Content = "00:00:00";
                    lblTimSatu.Content = "00:00:00";
                    lblTimDua.Content = "00:00:00";
                    lblTimTiga.Content = "00:00:00";
                    lblTimEmpat.Content = "00:00:00";
                    txtJudul.Text = "";
                    txtJam.Text = "00";
                    txtMenit.Text = "00";
                    txtDetik.Text = "00";
                    break;
                case MessageBoxResult.No:
                    break;
            }
            
        }

        private void btnRecSatu_Click(object sender, RoutedEventArgs e)
        {
            int s = intTimer % 60;
            int m = intTimer / 60 % 60;
            int h = intTimer / (60 * 60);
            recordWaktu[0] = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0')); ;
            lblTimSatu.Content = recordWaktu[0];
        }

        private void btnRecDua_Click(object sender, RoutedEventArgs e)
        {
            int s = intTimer % 60;
            int m = intTimer / 60 % 60;
            int h = intTimer / (60 * 60);
            recordWaktu[1] = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0')); ;
            lblTimSatu.Content = recordWaktu[1];
        }

        private void btnRecTiga_Click(object sender, RoutedEventArgs e)
        {
            int s = intTimer % 60;
            int m = intTimer / 60 % 60;
            int h = intTimer / (60 * 60);
            recordWaktu[2] = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0')); ;
            lblTimSatu.Content = recordWaktu[2];
        }

        private void btnRecEmpat_Click(object sender, RoutedEventArgs e)
        {
            int s = intTimer % 60;
            int m = intTimer / 60 % 60;
            int h = intTimer / (60 * 60);
            recordWaktu[3] = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0')); ;
            lblTimSatu.Content = recordWaktu[3];
        }

        private void cmbSatu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            teamName[0] = cmbSatu.Text;
        }

        private void cmbDua_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            teamName[1] = cmbDua.Text;
        }

        private void cmbTiga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            teamName[2] = cmbTiga.Text;
        }

        private void cmbEmpat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            teamName[3] = cmbTiga.Text;
        }

        private void openExcel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true } ;

            if (openFile.ShowDialog() == true)
            {
                FileStream fs = File.Open(openFile.FileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                reader.IsFirstRowAsColumnNames = true;
                result = reader.AsDataSet();
                dataTable = result.Tables[0];
                List<string> tim = new List<string>(dataTable.Rows.Count);
                foreach(DataRow row in dataTable.Rows)
                {
                    tim.Add((string)row[1]);
                }
                foreach(string namaTim in tim)
                {
                    cmbSatu.Items.Add(namaTim);
                    cmbDua.Items.Add(namaTim);
                    cmbTiga.Items.Add(namaTim);
                    cmbEmpat.Items.Add(namaTim);
                }
            }
        }
    }
}
