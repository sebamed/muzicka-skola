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
    public partial class Add : Window
    {

        private SqlConnection conn = Connection.getConnection();

        private string query = "";

        public Add()
        {
            InitializeComponent();


            // combo box fetch data
            try
            {
                this.conn.Open();

                this.query = @"select ProfesorID, ProfesorIme + ProfesorPrezime + InstrumentNaziv as 'Profesor' from tblProfesor inner join tblInstrument on tblProfesor.InstrumentID = tblInstrument.InstrumentID";
                DataTable dtProfesori = new DataTable();
                SqlDataAdapter daProfesori = new SqlDataAdapter(this.query, this.conn);
                daProfesori.Fill(dtProfesori);

                this.cbProfesori.ItemsSource = dtProfesori.DefaultView;
                this.cbProfesori.SelectedIndex = 0;
            }
            catch (SqlException)
            {
                MessageBox.Show("Error with database!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (String.IsNullOrEmpty(this.tbUcenikIme.Text) || String.IsNullOrEmpty(this.tbUcenikPrezime.Text) || String.IsNullOrEmpty(this.tbUcenikDatumRodjenja.Text) || String.IsNullOrEmpty(this.tbUcenikJMBG.Text))
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
                        this.query = @"update tblUcenik set UcenikIme = '" + this.tbUcenikIme.Text + 
                                                        "', UcenikPrezime = '" + this.tbUcenikPrezime.Text + 
                                                        "', UcenikJMBG = '" + this.tbUcenikJMBG.Text + 
                                                        "', UcenikDatumRodjenja = '" + this.tbUcenikDatumRodjenja.Text + 
                                                        "', ProfesorID = " + this.cbProfesori.SelectedValue + 
                                                        " where UcenikID = " + row["ID"];
                        MainWindow.selectedRow = null;
                    }
                    else
                    {

                        this.query = @"insert into tblUcenik(UcenikIme, UcenikPrezime, UcenikDatumRodjenja, UcenikJMBG, ProfesorID) values('" + this.tbUcenikIme.Text + "', '" +
                                                                                                                                            this.tbUcenikPrezime.Text + "', '" +
                                                                                                                                            this.tbUcenikDatumRodjenja.Text + "', '" +
                                                                                                                                            this.tbUcenikJMBG.Text + "', " +
                                                                                                                                            this.cbProfesori.SelectedValue + ")";
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
