using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace MuzickaSkola.Forms
{
    public partial class DodajPitanje : Window
    {

        private SqlConnection conn = Connection.getConnection();

        private string query = "";

        public DodajPitanje()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbPitanjeNaslov.Text) || String.IsNullOrEmpty(this.tbPitanjeText.Text) || String.IsNullOrEmpty(this.tbPitanjeTacanOdgovor.Text))
            {
                MessageBox.Show("Morate popuniti sva polja!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    this.conn.Open();

                    if (MainWindow.edit)
                    {
                        DataRowView row = (DataRowView)MainWindow.selectedRow;
                        this.query = @"update tblPitanje set PitanjeNaslov = '" + this.tbPitanjeNaslov.Text + "', PitanjeText = '" + this.tbPitanjeText.Text + "', PitanjeTacanOdgovor = '" + this.tbPitanjeTacanOdgovor.Text + "' where PitanjeID = " + row["ID"];
                        MainWindow.selectedRow = null;
                    }
                    else
                    {

                        this.query = @"insert into tblPitanje(PitanjeNaslov, PitanjeText, PitanjeTacanOdgovor) values('" + this.tbPitanjeNaslov.Text + "', '" + this.tbPitanjeText.Text + "', '" + this.tbPitanjeTacanOdgovor.Text + "')";
                    }
                    SqlCommand cmd = new SqlCommand(this.query, this.conn);
                    cmd.ExecuteNonQuery();
                    this.Close();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Error with statement!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    this.conn.Close();
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
