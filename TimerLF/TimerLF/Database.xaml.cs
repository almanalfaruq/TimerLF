using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace TimerLF
{
    /// <summary>
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class Database : Window
    {
        private string stDate;
        private DBHelper dbHelper;
        private DataTable dt;

        public Database()
        {
            InitializeComponent();
            dbHelper = new DBHelper();
        }

        private void dpLF_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;

            DateTime? date = picker.SelectedDate;
            if (date != null)
            {
                stDate = date.Value.ToString("ddMMyyyy");
                dt = dbHelper.loadFromDB(stDate);
                dgLF.ItemsSource = dt.DefaultView;
            }
        }
    }
}
