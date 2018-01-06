using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for DodajIspitanika.xaml
    /// </summary>
    public partial class DodajIspitanika : Window
    {

        private SqlConnection conn = Connection.getConnection();

        private string query = "";

        public DodajIspitanika()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbIspitanikIme.Text) || String.IsNullOrEmpty(this.tbIspitanikPrezime.Text) || String.IsNullOrEmpty(this.tbIspitanikUsername.Text) || String.IsNullOrEmpty(this.tbIspitanikPassword.Text))
            {
                MessageBox.Show("Morate popuniti sva polja!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    this.conn.Open();
                    this.query = @"insert into tblIspitanik(IspitanikIme, IspitanikPrezime, Username, Password) values('" + this.tbIspitanikIme.Text + "', '" + this.tbIspitanikPrezime.Text + "', '"
                                                                                                                                                     + this.tbIspitanikUsername.Text + "', '" + this.tbIspitanikPassword.Text + "')";
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
    }
}
