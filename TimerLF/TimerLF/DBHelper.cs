using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimerLF
{
    class DBHelper
    {
        public static string dataSource = "Data Source=LF_TC2K17.db";
        public static string ID = "_id";
        public static string TIM1 = "tim_1";
        public static string TIM2 = "tim_2";
        public static string TIM3 = "tim_3";
        public static string TIM4 = "tim_4";
        public static string WAKTU_TIM1 = "waktu_tim_1";
        public static string WAKTU_TIM2 = "waktu_tim_2";
        public static string WAKTU_TIM3 = "waktu_tim_3";
        public static string WAKTU_TIM4 = "waktu_tim_4";

        private string today;

        private static SQLiteConnection conn;
        

        public DBHelper()
        {
            today = DateTime.Today.ToString("ddMMyyyy");
            SQLConnection();
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS " + "DATE_" + today
                + "(_id INTEGER PRIMARY KEY AUTOINCREMENT, "
                + "tim_1 TEXT, "
                + "waktu_tim_1 TEXT, "
                + "tim_2 TEXT, "
                + "waktu_tim_2 TEXT, "
                + "tim_3 TEXT, "
                + "waktu_tim_3 TEXT, "
                + "tim_4 TEXT, "
                + "waktu_tim_4 TEXT)";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void insertIntoDB(string[] tim, string[] waktu)
        {
            try
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "INSERT INTO DATE_" + today
                    + "(tim_1, "
                    + "waktu_tim_1, "
                    + "tim_2, "
                    + "waktu_tim_2, "
                    + "tim_3, "
                    + "waktu_tim_3, "
                    + "tim_4, "
                    + "waktu_tim_4) "
                    + "VALUES "
                    + " ('" + tim[0] + "','" + waktu[0] + "','" + tim[1] + "','" + waktu[1] + "','" + tim[2] + "','" + waktu[2] + "','" + tim[3] + "','" + waktu[3] + "')";
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
        }

        public DataTable loadFromDB(string date)
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM DATE_" + date;
                var command = conn.CreateCommand();
                command.CommandText = query;
                command.ExecuteNonQuery();
                SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(command);
                DataTable dt = new DataTable(date);
                dataAdp.Fill(dt);
                dataAdp.Update(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DataTable dt = null;
                return dt;
            }
        }

        public static SQLiteConnection SQLConnection()
        {
            return conn = new SQLiteConnection(dataSource);
        }
    }
}
