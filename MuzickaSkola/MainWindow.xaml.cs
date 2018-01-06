﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media.Effects;
using MuzickaSkola.Forms;

namespace MuzickaSkola
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlurEffect blurEffect = new BlurEffect();

        private SqlConnection conn = Connection.getConnection();

        private string query = "";

        public static object selectedRow = null;

        private bool SidebarOpened = false;
        public static bool edit = false;

        private int currentlyActive = 0;
        private int previouslyActive = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.setCrudMenuVisible(false);
            this.dgMain.Visibility = Visibility.Hidden;

            this.RegisterName("blurEffect", blurEffect);

            this.fillInHomeInfo();
        }

        private void fillInHomeInfo()
        {
            try
            {
                this.conn.Open();

                this.query = "select count(UcenikID) from tblUcenik";

                // broj ucenika
                SqlCommand cmd = new SqlCommand(this.query, this.conn);
                Int32 countUcenika = (Int32)cmd.ExecuteScalar();

                // broj profesora
                cmd.CommandText = "select count(ProfesorID) from tblProfesor";
                Int32 countProfesora = (Int32)cmd.ExecuteScalar();

                // broj instrumenata
                cmd.CommandText = "select count(InstrumentID) from tblInstrument";
                Int32 countInstrumenata = (Int32)cmd.ExecuteScalar();

                // broj ispita
                cmd.CommandText = "select count(IspitID) from tblIspit";
                Int32 countIspita = (Int32)cmd.ExecuteScalar();

                // broj polozenih ispita
                cmd.CommandText = "select count(IspitID) from tblIspit where IspitPolozen > 0";
                Int32 countPolozenih = (Int32)cmd.ExecuteScalar();

                // broj ispitanika
                cmd.CommandText = "select count(IspitanikID) from tblIspitanik";
                Int32 countnIspitanika = (Int32)cmd.ExecuteScalar();

                // setovanje labelova
                this.lblUcenika.Content = countUcenika + "";
                this.lblProfesora.Content = countProfesora + "";
                this.lblInstrumenata.Content = "uz " + countInstrumenata;
                this.lblIspita.Content = countIspita + "";
                this.lblPolozenihIspita.Content = countPolozenih + " polozenih";
                this.lblIspitanika.Content = countnIspitanika;
            }
            catch (SqlException)
            {

            }
            finally
            {
                this.conn.Close();
            }
        }

        private void btnCollapseMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.SidebarOpened)
            {
                ShowHideMenu("sbHideSidenav", this.spSideNav);
                this.dgMain.Effect = null;
            } else
            {
                ShowHideMenu("sbShowSidenav", this.spSideNav);
                this.blurEffect.Radius = 5;
                this.dgMain.Effect = this.blurEffect;
            }
        }

        private void ShowHideMenu(string Storyboard, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            this.SidebarOpened = !this.SidebarOpened;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.blurEffect.Radius = 5;
            this.Effect = this.blurEffect;
            Exit exit = new Exit();
            exit.ShowDialog();
            this.Effect = null;
        }

        private void setInactive(int inAc)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this.spSideNav); i++) // usao u sidenav
            {
                DependencyObject current = VisualTreeHelper.GetChild(this.spSideNav, inAc + 1);
                if (current.GetType().Equals(typeof(Button)))
                {
                    Button btn = (Button)current;
                    btn.Background = Brushes.DarkBlue;
                    for (int j = 0; j < VisualTreeHelper.GetChildrenCount(current); j++) // usao u dugme
                    {
                        current = VisualTreeHelper.GetChild(current, j);
                        if (current.GetType().Equals(typeof(Border)))
                        {
                            for (int k = 0; k < VisualTreeHelper.GetChildrenCount(current); k++) // usao u border
                            {

                                current = VisualTreeHelper.GetChild(current, k);
                                Border b = (Border)current;
                                b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#242631")); // pozadina dugmeta
                                current = VisualTreeHelper.GetChild(current, 0);
                                Image img = (Image)current;
                                if (this.previouslyActive == 0) // pocetna
                                {
                                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Heating_Room_50px_1.png"));
                                }
                                else if (this.previouslyActive == 1)
                                {
                                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Graduation_Cap_48px.png"));
                                }
                                else if (this.previouslyActive == 2)
                                {
                                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Training_50px.png"));
                                }
                                else if (this.previouslyActive == 3)
                                {
                                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Drums_50px.png"));
                                }
                                else if (this.previouslyActive == 4)
                                {
                                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_User_Typing_Using_Typewriter_50px_1.png"));
                                }
                                else if (this.previouslyActive == 5)
                                {
                                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Test_Passed_50px_1.png"));
                                }
                                else if (this.previouslyActive == 6)
                                {
                                    img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Questionnaire_48px.png"));
                                }

                                return;
                            }
                        }
                        else if (current.GetType().Equals(typeof(DockPanel)))
                        {
                            DockPanel dp = (DockPanel)current;
                            dp.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#2B303D"));
                        }

                    }

                }
            }
        }

        private void setActive()
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this.spSideNav); i++) // usao u sidenav
            {
                DependencyObject current = VisualTreeHelper.GetChild(this.spSideNav, this.currentlyActive + 1);
                if (current.GetType().Equals(typeof(Button)))
                {
                    Button btn = (Button)current;
                    btn.Background = Brushes.DarkBlue;
                    for (int j = 0; j < VisualTreeHelper.GetChildrenCount(current); j++) // usao u dugme 
                    {
                        current = VisualTreeHelper.GetChild(current, j);
                        if (current.GetType().Equals(typeof(Border)))
                        {
                            for (int k = 0; k < VisualTreeHelper.GetChildrenCount(current); k++) // usao u border
                            {
                                current = VisualTreeHelper.GetChild(current, k);
                                Border b = (Border)current;
                                switch (this.currentlyActive)
                                {
                                    case 0:
                                        b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ecc71")); // zelen
                                        break;
                                    case 1:
                                        b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1c40f")); // zut
                                        break;
                                    case 2:
                                        b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#e74c3c")); // crven
                                        break;
                                    case 3:
                                        b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498db")); // plav
                                        break;
                                    case 4:
                                        b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ecc71")); // zelen
                                        break;
                                    case 5:
                                        b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1c40f")); // zut
                                        break;
                                    case 6:
                                        b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#e74c3c")); // crven
                                        break;
                                }

                                current = VisualTreeHelper.GetChild(current, 0);
                                Image img = (Image)current;
                                switch (this.currentlyActive)
                                {
                                    case 0:
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Heating_Room_50px.png"));
                                        break;
                                    case 1:
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Graduation_Cap_48px_1.png"));
                                        break;
                                    case 2:
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Training_50px_1.png"));
                                        break;
                                    case 3:
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Drums_50px_1.png"));
                                        break;
                                    case 4:
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_User_Typing_Using_Typewriter_64px.png"));
                                        break;
                                    case 5:
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Test_Passed_50px_2.png"));
                                        break;
                                    case 6:
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Questionnaire_48px_1.png"));
                                        break;
                                }

                                if (!this.isSame())
                                {
                                    this.setInactive(this.previouslyActive);
                                }

                                return;
                            }
                        }
                        else if (current.GetType().Equals(typeof(DockPanel)))
                        {
                            DockPanel dp = (DockPanel)current;
                            dp.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#242631"));
                        }

                    }

                }
            }
        }

        private bool isSame()
        {
            if (this.currentlyActive == this.previouslyActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void setHomeScreenVisible()
        {
            if(this.grdHome.Visibility == Visibility.Visible)
            {
                this.grdHome.Visibility = Visibility.Hidden;
            }
        }

        private void btnSNPocetna_Click(object sender, RoutedEventArgs e)
        {
            // Glavni meni
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 0;
            setActive();

            // CRUD meni
            this.setCrudMenuVisible(false);

            // tabela
            this.dgMain.Visibility = Visibility.Hidden;

            this.grdHome.Visibility = Visibility.Visible;
            this.fillInHomeInfo();
        }

        private void btnSNUcenik_Click(object sender, RoutedEventArgs e)
        {
            this.setHomeScreenVisible();
            // Glavni meni
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 1;
            setActive();

            // CRUD meni
            this.setCrudMenuVisible(true);

            // tabela
            this.setDataGridModel();
            this.dgMain.SelectedIndex = 0;
        }

        private void btnSNProfesor_Click(object sender, RoutedEventArgs e)
        {
            this.setHomeScreenVisible();
            // Glavni meni
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 2;
            setActive();

            // CRUD meni
            this.setCrudMenuVisible(true);

            // tabela
            this.setDataGridModel();
            this.dgMain.SelectedIndex = 0;

        }

        private void btnSNInstrument_Click(object sender, RoutedEventArgs e)
        {
            this.setHomeScreenVisible();
            // Glavni meni
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 3;
            setActive();

            // CRUD meni
            this.setCrudMenuVisible(true);

            // tabela
            this.setDataGridModel();
            this.dgMain.SelectedIndex = 0;

        }

        private void btnSNIspitanik_Click(object sender, RoutedEventArgs e)
        {
            this.setHomeScreenVisible();
            // Glavni meni
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 4;
            setActive();

            // CRUD meni
            this.setCrudMenuVisible(true);

            // tabela
            this.setDataGridModel();
            this.dgMain.SelectedIndex = 0;
        }

        private void btnSNIspit_Click(object sender, RoutedEventArgs e)
        {
            this.setHomeScreenVisible();
            // Glavni meni
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 5;
            setActive();

            // CRUD meni
            this.setCrudMenuVisible(true);

            // tabela
            this.setDataGridModel();
            this.dgMain.SelectedIndex = 0;
        }

        private void btnSNPitanja_Click(object sender, RoutedEventArgs e)
        {
            this.setHomeScreenVisible();
            // Glavni meni
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 6;
            setActive();

            // CRUD meni
            this.setCrudMenuVisible(true);

            // tabela
            this.setDataGridModel();
            this.dgMain.SelectedIndex = 0;
        }

        private void btnRNRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.setDataGridModel();
        }

        private void setCrudMenuVisible(bool enabled)
        {
            if (enabled) // vidljivi
            {
                this.btnRNAdd.Visibility = Visibility.Visible;
                this.btnRNEdit.Visibility = Visibility.Visible;
                this.btnRNRemove.Visibility = Visibility.Visible;
                this.btnRNRefresh.Visibility = Visibility.Visible;

            }
            else
            {
                this.btnRNAdd.Visibility = Visibility.Hidden;
                this.btnRNEdit.Visibility = Visibility.Hidden;
                this.btnRNRemove.Visibility = Visibility.Hidden;
                this.btnRNRefresh.Visibility = Visibility.Hidden;
            }
        }

        private void setDataGridModel()
        {
            switch (this.currentlyActive)
            {
                case 1: this.query = @"select UcenikID as 'ID', UcenikIme as 'IME', UcenikPrezime as 'PREZIME', UcenikJMBG as 'JMBG', UcenikDatumRodjenja as 'DATUM RODJENJA', ProfesorIme + ProfesorPrezime as 'PROFESOR'
                            from tblUcenik inner join tblProfesor on tblUcenik.ProfesorID = tblProfesor.ProfesorID";
                    break;
                case 2: this.query = @"select ProfesorID as 'ID', ProfesorIme as 'IME', ProfesorPrezime as 'PREZIME', InstrumentNaziv as 'INSTRUMENT'  
                            from tblProfesor inner join tblInstrument on tblProfesor.InstrumentID = tblInstrument.InstrumentID";
                    break;
                case 3: this.query = @"select InstrumentID as 'ID', InstrumentNaziv as 'INSTRUMENT' from tblInstrument";
                    break;
                case 4: this.query = @"select IspitanikID as 'ID', IspitanikIme as 'IME', IspitanikPrezime as 'PREZIME', Username as 'USERNAME', Password as 'PASSWORD' from tblIspitanik";
                    break;
                case 5: this.query = @"select IspitID as 'ID',
                                    UcenikIme + UcenikPrezime as 'UCENIK', 
                                    IspitanikIme + IspitanikPrezime as 'ISPITANIK',
                                    PitanjeNaslov as 'PITANJE',
                                    IspitOsvojenihBodova as 'BODOVI',
                                    IspitPolozen as 'POLOZEN'
                                    from tblIspit inner join tblUcenik on tblIspit.UcenikID = tblUcenik.UcenikID
                                    inner join tblIspitanik on tblIspit.IspitanikID = tblIspitanik.IspitanikID
                                    inner join tblPitanje on tblIspit.PitanjeID = tblPitanje.PitanjeID";
                    break;
                case 6: this.query = @"select PitanjeID as 'ID', PitanjeNaslov as 'NASLOV', PitanjeText as 'PITANJE', PitanjeTacanOdgovor as 'TACAN ODGOVOR' from tblPitanje";
                    break;
            }

            try
            {
                this.conn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(this.query, this.conn);

                DataTable dt = new DataTable("Test");
                sqlDA.Fill(dt);
                this.dgMain.ItemsSource = dt.DefaultView;
                this.dgMain.Visibility = Visibility.Visible;
            } catch (Exception)
            {
                MessageBox.Show("Error with connection or query!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.conn.Close();
            }
        }

        private void btnRNAdd_Click(object sender, RoutedEventArgs e)
        {
            this.chooseToAdd();
            this.btnRNRefresh_Click(sender, e);
            this.dgMain.SelectedIndex = this.dgMain.Items.Count - 1;
            this.dgMain.ScrollIntoView(this.dgMain.SelectedItem);
        }

        private void chooseToAdd()
        {
            this.blurEffect.Radius = 5;
            this.Effect = this.blurEffect;
            switch (this.currentlyActive)
            {
                case 1: // otvori dodaj ucenika
                    Add addNew = new Add();
                    addNew.ShowDialog();          
                    break;
                case 2: // otvori dodaj profesora
                    DodajProfesora dodajProfesora = new DodajProfesora();
                    dodajProfesora.ShowDialog();
                    break;
                case 3: // otvori dodaj instrument
                    DodajInstrument dodajInstrument = new DodajInstrument();
                    dodajInstrument.ShowDialog();
                    break;
                case 4: // otvori dodaj ispitanika
                    DodajIspitanika dodajIspitanika = new DodajIspitanika();
                    dodajIspitanika.ShowDialog();
                    break;
                case 5: // otvori dodaj ispit
                    DodajIspit dodajIspit = new DodajIspit();
                    dodajIspit.ShowDialog();
                    break;
                case 6: // otvori dodaj ispit
                    DodajPitanje dodajPitanje = new DodajPitanje();
                    dodajPitanje.ShowDialog();
                    break;
            }
            this.Effect = null;
        }

        private void deleteFromDb(object sender, RoutedEventArgs e)
        {
            string type = "";
            string rowType = "";
            try
            {
                this.conn.Open();
                DataRowView row = (DataRowView)this.dgMain.SelectedItems[0];

                switch (this.currentlyActive)
                {
                    case 1: // ucenik
                        this.query = "delete from tblUcenik where UcenikID = " + row["ID"];
                        type = "ucenika";
                        rowType = "IME";
                        break;
                    case 2: // profesor
                        this.query = "delete from tblProfesor where ProfesorID = " + row["ID"];
                        type = "profesora";
                        rowType = "IME";
                        break;
                    case 3: // instrument
                        this.query = "delete from tblInstrument where InstrumentID  = " + row["ID"];
                        type = "instrument";
                        rowType = "INSTRUMENT";
                        break;
                    case 4: // ispitanik
                        this.query = "delete from tblIspitanik where IspitanikID  = " + row["ID"];
                        type = "ispitanika";
                        rowType = "IME";
                        break;
                    case 5: // ispit
                        this.query = "delete from tblispit where IspitID  = " + row["ID"];
                        type = "ispit sa ID-em";
                        rowType = "ID";
                        break;
                    case 6: // pitanje
                        this.query = "delete from tblPitanje where PitanjeID  = " + row["ID"];
                        type = "pitanje sa ID-em";
                        rowType = "ID";
                        break;
                }

                MessageBoxResult res = MessageBox.Show("Da li ste sigurni da zelite da obrisete " + type + " '" + row[rowType] + "'?", "Upozorenje!", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if(res == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand(this.query, this.conn);
                    cmd.ExecuteNonQuery();
                } 

            } catch (SqlException)
            {
                MessageBox.Show("Podaci koje brisete su povezani sa drugim tabelama!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.conn.Close();
            }

            this.btnRNRefresh_Click(sender, e); // refreshuje tabelu
            this.dgMain.SelectedIndex = 0;
        }
    
        private void btnRNRemove_Click(object sender, RoutedEventArgs e)
        {
            this.deleteFromDb(sender, e);
        }

        private void btnRNEdit_Click(object sender, RoutedEventArgs e)
        {
            this.chooseToEdit(sender, e);
        }

        private void editUcenik(object sender, RoutedEventArgs e)
        {
            try
            {
                edit = true;
                Add dodajUcenika = new Add();

                this.conn.Open();
                DataRowView row = (DataRowView)this.dgMain.SelectedItems[0];
                selectedRow = row;
                this.query = @"select * from tblUcenik where UcenikID = " + row["ID"];

                SqlCommand cmd = new SqlCommand(this.query, this.conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dodajUcenika.tbUcenikIme.Text = reader["UcenikIme"].ToString();
                    dodajUcenika.tbUcenikPrezime.Text = reader["UcenikPrezime"].ToString();
                    dodajUcenika.tbUcenikDatumRodjenja.Text = reader["UcenikDatumRodjenja"].ToString();
                    dodajUcenika.tbUcenikJMBG.Text = reader["UcenikJMBG"].ToString();
                    dodajUcenika.cbProfesori.SelectedValue = reader["ProfesorID"].ToString();
                }

                dodajUcenika.ShowDialog();
            } catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Morate selektovati red!");
            }
            finally
            {
                this.conn.Close();
                this.btnRNRefresh_Click(sender, e);
                this.dgMain.SelectedIndex = this.dgMain.Items.Count - 1;
                this.dgMain.ScrollIntoView(this.dgMain.SelectedItem);
                edit = false;
            }
        }

        private void editProfesor(object sender, RoutedEventArgs e)
        {
            try
            {
                edit = true;
                DodajProfesora dodajProfesora = new DodajProfesora();

                this.conn.Open();
                DataRowView row = (DataRowView)this.dgMain.SelectedItems[0];
                selectedRow = row;
                this.query = @"select * from tblProfesor where ProfesorID = " + row["ID"];

                SqlCommand cmd = new SqlCommand(this.query, this.conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dodajProfesora.tbProfesorIme.Text = reader["ProfesorIme"].ToString();
                    dodajProfesora.tbProfesorPrezime.Text = reader["ProfesorPrezime"].ToString();
                    dodajProfesora.cbInstrumenti.SelectedValue = reader["InstrumentID"].ToString();
                }

                dodajProfesora.ShowDialog();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Morate selektovati red!");
            }
            finally
            {
                this.conn.Close();
                this.btnRNRefresh_Click(sender, e);
                this.dgMain.SelectedIndex = this.dgMain.Items.Count - 1;
                this.dgMain.ScrollIntoView(this.dgMain.SelectedItem);
                edit = false;
            }
        }

        private void editInstrument(object sender, RoutedEventArgs e)
        {
            try
            {
                edit = true;
                DodajInstrument dodajInstrument = new DodajInstrument();

                this.conn.Open();
                DataRowView row = (DataRowView)this.dgMain.SelectedItems[0];
                selectedRow = row;
                this.query = @"select * from tblInstrument where InstrumentID = " + row["ID"];

                SqlCommand cmd = new SqlCommand(this.query, this.conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dodajInstrument.tbInstrumentNaziv.Text = reader["InstrumentNaziv"].ToString();
                }

                dodajInstrument.ShowDialog();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Morate selektovati red!");
            }
            finally
            {
                this.conn.Close();
                this.btnRNRefresh_Click(sender, e);
                this.dgMain.SelectedIndex = this.dgMain.Items.Count - 1;
                this.dgMain.ScrollIntoView(this.dgMain.SelectedItem);
                edit = false;
            }
        }

        private void editIspitanik(object sender, RoutedEventArgs e)
        {
            try
            {
                edit = true;
                DodajIspitanika dodajIspitanika = new DodajIspitanika();

                this.conn.Open();
                DataRowView row = (DataRowView)this.dgMain.SelectedItems[0];
                selectedRow = row;
                this.query = @"select * from tblIspitanik where IspitanikID = " + row["ID"];

                SqlCommand cmd = new SqlCommand(this.query, this.conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dodajIspitanika.tbIspitanikIme.Text = reader["IspitanikIme"].ToString();
                    dodajIspitanika.tbIspitanikPrezime.Text = reader["IspitanikPrezime"].ToString();
                    dodajIspitanika.tbIspitanikUsername.Text = reader["Username"].ToString();
                    dodajIspitanika.tbIspitanikPassword.Text = reader["Password"].ToString();
                }

                dodajIspitanika.ShowDialog();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Morate selektovati red!");
            }
            finally
            {
                this.conn.Close();
                this.btnRNRefresh_Click(sender, e);
                this.dgMain.SelectedIndex = this.dgMain.Items.Count - 1;
                this.dgMain.ScrollIntoView(this.dgMain.SelectedItem);
                edit = false;
            }
        }

        private void editIspit(object sender, RoutedEventArgs e)
        {
            try
            {
                edit = true;
                DodajIspit dodajIspit = new DodajIspit();

                this.conn.Open();
                DataRowView row = (DataRowView)this.dgMain.SelectedItems[0];
                selectedRow = row;
                this.query = @"select * from tblIspit where IspitID = " + row["ID"];

                SqlCommand cmd = new SqlCommand(this.query, this.conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dodajIspit.cbUcenici.SelectedValue = reader["UcenikID"].ToString();
                    dodajIspit.cbIspitanici.SelectedValue = reader["IspitanikID"].ToString();
                    dodajIspit.cbPitanja.SelectedValue = reader["PitanjeID"].ToString();
                    dodajIspit.tbOsvojenihBodova.Text = reader["IspitOsvojenihBodova"].ToString();
                }

                dodajIspit.ShowDialog();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Morate selektovati red!");
            }
            finally
            {
                this.conn.Close();
                this.btnRNRefresh_Click(sender, e);
                this.dgMain.SelectedIndex = this.dgMain.Items.Count - 1;
                this.dgMain.ScrollIntoView(this.dgMain.SelectedItem);
                edit = false;
            }
        }

        private void editPitanje(object sender, RoutedEventArgs e)
        {
            try
            {
                edit = true;
                DodajPitanje dodajPitanje = new DodajPitanje();

                this.conn.Open();
                DataRowView row = (DataRowView)this.dgMain.SelectedItems[0];
                selectedRow = row;
                this.query = @"select * from tblPitanje where PitanjeID = " + row["ID"];

                SqlCommand cmd = new SqlCommand(this.query, this.conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dodajPitanje.tbPitanjeNaslov.Text = reader["PitanjeNaslov"].ToString();
                    dodajPitanje.tbPitanjeText.Text = reader["PitanjeText"].ToString();
                    dodajPitanje.tbPitanjeTacanOdgovor.Text = reader["PitanjeTacanOdgovor"].ToString();
                }

                dodajPitanje.ShowDialog();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Morate selektovati red!");
            }
            finally
            {
                this.conn.Close();
                this.btnRNRefresh_Click(sender, e);
                this.dgMain.SelectedIndex = this.dgMain.Items.Count - 1;
                this.dgMain.ScrollIntoView(this.dgMain.SelectedItem);
                edit = false;
            }
        }

        private void chooseToEdit(object sender, RoutedEventArgs e)
        {
            this.blurEffect.Radius = 5;
            this.Effect = this.blurEffect;
            switch (this.currentlyActive)
            {
                case 1:
                    this.editUcenik(sender, e);
                    break;
                case 2:
                    this.editProfesor(sender, e);
                    break;
                case 3:
                    this.editInstrument(sender, e);
                    break;
                case 4:
                    this.editIspitanik(sender, e);
                    break;
                case 5:
                    this.editIspit(sender, e);
                    break;
                case 6:
                    this.editPitanje(sender, e);
                    break;
            }
            this.Effect = null;
        }
    }
}
