using System;
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

        private bool SidebarOpened = false;
        private int currentlyActive = 0;
        private int previouslyActive = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.setCrudMenuVisible(false);
            this.dgMain.Visibility = Visibility.Hidden;

            this.RegisterName("blurEffect", blurEffect);


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
            // Dodati diskonektovanje 
            this.Close();
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
        }

        private void btnSNUcenik_Click(object sender, RoutedEventArgs e)
        {
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
            } catch (Exception e)
            {
                MessageBox.Show("Error with connection or query!", "Error!", MessageBoxButton.OK);
            }
            finally
            {
                this.conn.Close();
            }
        }

        private void btnRNAdd_Click(object sender, RoutedEventArgs e)
        {
            this.chooseToAdd();
        }

        private void chooseToAdd()
        {
            switch (this.currentlyActive)
            {
                case 1: // otvori dodaj ucenika
                    Add addNew = new Add();
                    addNew.ShowDialog();
                    break;
            }
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

                MessageBoxResult res = MessageBox.Show("Da li ste sigurni da zelite da obrisete " + type + " '" + row[rowType] + "'?", "Upozorenje!", MessageBoxButton.YesNo);

                if(res == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand(this.query, this.conn);
                    cmd.ExecuteNonQuery();
                } 

            } catch (SqlException)
            {
                MessageBox.Show("Podaci koje brisete su povezani sa drugim tabelama!", "Error!", MessageBoxButton.OK);
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
    }
}
