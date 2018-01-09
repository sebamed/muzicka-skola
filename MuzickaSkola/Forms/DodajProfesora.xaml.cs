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
    /// <summary>
    /// Interaction logic for DodajProfesora.xaml
    /// </summary>
    public partial class DodajProfesora : Window
    {

        private SqlConnection conn = Connection.getConnection();
        private string query = "";

        public DodajProfesora()
        {
            InitializeComponent();

            // combo box fetching data
            try
            {
                this.conn.Open();
                this.query = @"select InstrumentID, InstrumentNaziv from tblInstrument";
                DataTable dtInstrumenti = new DataTable();
                SqlDataAdapter daInstrumenti = new SqlDataAdapter(this.query, this.conn);
                daInstrumenti.Fill(dtInstrumenti);

                this.cbInstrumenti.ItemsSource = dtInstrumenti.DefaultView;
                this.cbInstrumenti.SelectedIndex = 0;
            }
            catch (SqlException)
            {
                MessageBox.Show("Error with database!");
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbProfesorIme.Text) || String.IsNullOrEmpty(this.tbProfesorPrezime.Text))
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
                        this.query = @"update tblProfesor set ProfesorIme = '" + this.tbProfesorIme.Text + "', ProfesorPrezime = '" + this.tbProfesorPrezime.Text + 
                                                                "', InstrumentID = " + this.cbInstrumenti.SelectedValue + " where ProfesorID = " + row["ID"]; 
                        MainWindow.selectedRow = null;
                    }
                    else
                    {
                        this.query = @"insert into tblProfesor(ProfesorIme, ProfesorPrezime, InstrumentID) values('" + this.tbProfesorIme.Text + "', '" + this.tbProfesorPrezime.Text + "', " +
                                                                                                                                        this.cbInstrumenti.SelectedValue + ")";
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
    }
}
