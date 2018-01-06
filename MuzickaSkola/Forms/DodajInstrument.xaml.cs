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
    /// Interaction logic for DodajInstrument.xaml
    /// </summary>
    public partial class DodajInstrument : Window
    {

        private SqlConnection conn = Connection.getConnection();

        private string query = "";

        public DodajInstrument()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.conn.Open();
                this.query = @"insert into tblInstrument(InstrumentNaziv) values ('" + this.tbInstrumentNaziv.Text + "')";
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
