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

    public partial class DodajIspit : Window
    {
        private int minZaProlaz = 50;

        private SqlConnection conn = Connection.getConnection();

        private string query = "";

        public DodajIspit()
        {
            InitializeComponent();
            this.tbUkupnoBodova.Text = "100";
            this.chbPolozen.IsChecked = false;
            this.initializeComboBoxes();
        }

        private void initializeComboBoxes()
        {
            // Ucenik
            try
            {
                this.conn.Open();

                // Ucenici
                this.query = @"select UcenikID, UcenikIme + ' ' + UcenikPrezime as 'Ucenik' from tblUcenik";
                DataTable dtUcenici = new DataTable();
                SqlDataAdapter daUcenici = new SqlDataAdapter(this.query, this.conn);
                daUcenici.Fill(dtUcenici);

                this.cbUcenici.ItemsSource = dtUcenici.DefaultView;
                this.cbUcenici.SelectedIndex = 0;

                // Ispitanici
                this.query = @"select IspitanikID, IspitanikIme + ' ' + IspitanikPrezime as 'Ispitanik' from tblIspitanik";
                DataTable dtIspitanici = new DataTable();
                SqlDataAdapter daIspitanici = new SqlDataAdapter(this.query, this.conn);
                daIspitanici.Fill(dtIspitanici);

                this.cbIspitanici.ItemsSource = dtIspitanici.DefaultView;
                this.cbIspitanici.SelectedIndex = 0;

                // Pitanja
                this.query = @"select PitanjeID, PitanjeNaslov as 'Pitanje' from tblPitanje";
                DataTable dtPitanja = new DataTable();
                SqlDataAdapter daPitanja = new SqlDataAdapter(this.query, this.conn);
                daPitanja.Fill(dtPitanja);

                this.cbPitanja.ItemsSource = dtPitanja.DefaultView;
                this.cbPitanja.SelectedIndex = 0;
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
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbOsvojenihBodova.Text))
            {
                MessageBox.Show("Morate popuniti sva polja!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else
            {
                try
                {
                    this.conn.Open();
                    if (MainWindow.edit)
                    {
                        DataRowView row = (DataRowView)MainWindow.selectedRow;
                        this.query = @"update tblIspit set UcenikID = " + this.cbUcenici.SelectedValue +
                                                                        ", IspitanikID = " + this.cbIspitanici.SelectedValue +
                                                                        ", PitanjeID  = " + this.cbPitanja.SelectedValue +
                                                                        ", IspitUkupnoBodova = " + this.tbUkupnoBodova.Text +
                                                                        ", IspitOsvojenihBodova = " + this.tbOsvojenihBodova.Text +
                                                                        ", IspitPolozen = " + Convert.ToInt32(this.chbPolozen.IsChecked) +
                                                                        " where IspitID = " + row["ID"];

                        MainWindow.selectedRow = null;
                    }
                    else
                    {
                        this.query = @"insert into tblIspit(UcenikID, IspitanikID, PitanjeID, IspitUkupnoBodova, IspitOsvojenihBodova, IspitPolozen) values (" + this.cbUcenici.SelectedValue +
                                                                                                                                                        ", " + this.cbIspitanici.SelectedValue +
                                                                                                                                                        ", " + this.cbPitanja.SelectedValue +
                                                                                                                                                        ", " + int.Parse(this.tbUkupnoBodova.Text) +
                                                                                                                                                        ", " + int.Parse(this.tbOsvojenihBodova.Text) +
                                                                                                                                                        ", " + Convert.ToInt32(this.chbPolozen.IsChecked) +
                                                                                                                                                        ")";
                    }
                    SqlCommand sql = new SqlCommand(this.query, this.conn);
                    sql.ExecuteNonQuery();
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

        private void tbOsvojenihBodova_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isInteger = true;
            try
            {
                string enteredCharacter = e.Source.ToString().Split(':')[1].Substring(1); // jako doktorska stvar jer sam ja doktor jedan veliki sto se rodio
                isInteger = int.TryParse(enteredCharacter, out int num);
            } catch
            {
                this.tbOsvojenihBodova.Text = "";
            }
            if (!isInteger)
            {
                if (this.tbOsvojenihBodova.Text.Length == 1)
                {
                    
                    this.tbOsvojenihBodova.Text = this.tbOsvojenihBodova.Text.Substring(0, 0);
                    return;
                }
                else if (this.tbOsvojenihBodova.Text.Length == 2)
                {
                    this.tbOsvojenihBodova.Text = this.tbOsvojenihBodova.Text.Substring(0, 1);
                    return;
                }
                else if (this.tbOsvojenihBodova.Text.Length == 3)
                {
                    this.tbOsvojenihBodova.Text = this.tbOsvojenihBodova.Text.Substring(0, 2);
                    return;
                }
            }
            if(this.tbOsvojenihBodova.Text.Length == 3)
            {
                if (int.Parse(this.tbOsvojenihBodova.Text) == 100)
                {
                    this.chbPolozen.IsChecked = true;
                }
                else if (int.Parse(this.tbOsvojenihBodova.Text) > 100)
                {
                    this.chbPolozen.IsChecked = true;
                    this.tbOsvojenihBodova.Text = "100";
                }
            }
            else if (this.tbOsvojenihBodova.Text.Length == 2)
            {
                if (int.Parse(this.tbOsvojenihBodova.Text) > this.minZaProlaz)
                {
                    this.chbPolozen.IsChecked = true;
                } else if (int.Parse(this.tbOsvojenihBodova.Text) <= this.minZaProlaz)
                {
                    this.chbPolozen.IsChecked = false;
                }
            }
            else if(this.tbOsvojenihBodova.Text.Length == 1 || this.tbOsvojenihBodova.Text.Length == 0)
            {
                this.chbPolozen.IsChecked = false;
            }
        }
    }
}
