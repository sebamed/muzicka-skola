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

        public Add()
        {
            InitializeComponent();


            // combo box fetch data
            try
            {
                this.conn.Open();

                string profesori = "select ProfesorID, ProfesorIme + ProfesorPrezime + InstrumentNaziv as 'Profesor' from tblProfesor inner join tblInstrument on tblProfesor.InstrumentID = tblInstrument.InstrumentID";
                DataTable dtVozila = new DataTable();
                SqlDataAdapter daVozila = new SqlDataAdapter(profesori, this.conn);
                daVozila.Fill(dtVozila);

                this.cbProfesori.ItemsSource = dtVozila.DefaultView;
            }
            catch (SqlException)
            {
                MessageBox.Show("Error with database!");
            } finally
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

        }
    }
}
