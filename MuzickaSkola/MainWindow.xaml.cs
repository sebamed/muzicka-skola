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

namespace MuzickaSkola
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool SidebarOpened = false;
        private int currentlyActive = 0;
        private int previouslyActive = 0;

        public MainWindow()
        {       
            InitializeComponent();
        }

        private void btnCollapseMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.SidebarOpened)
            {
                ShowHideMenu("sbHideSidenav", this.spSideNav);
            } else
            {
                ShowHideMenu("sbShowSidenav", this.spSideNav);
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
                                    b.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BAC0")); // pozadina dugmeta
                                    current = VisualTreeHelper.GetChild(current, 0);
                                    Image img = (Image)current;
                                    if (this.currentlyActive == 0) // pocetna
                                    {
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Heating_Room_50px.png"));
                                    }
                                    else if (this.currentlyActive == 1)
                                    {
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Graduation_Cap_48px_1.png"));
                                    }
                                    else if (this.currentlyActive == 2)
                                    {
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Training_50px_1.png"));
                                    }
                                    else if (this.currentlyActive == 3)
                                    {
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Drums_50px_1.png"));
                                    }
                                    else if (this.currentlyActive == 4)
                                    {
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_User_Typing_Using_Typewriter_64px.png"));
                                    }
                                    else if (this.currentlyActive == 5)
                                    {
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Test_Passed_50px_2.png"));
                                    }
                                    else if (this.currentlyActive == 6)
                                    {
                                        img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/icons8_Questionnaire_48px_1.png"));
                                    }

                                    this.setInactive(this.previouslyActive);

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
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 0;
            setActive();
        }

        private void btnSNUcenik_Click(object sender, RoutedEventArgs e)
        {
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 1;
            setActive();
        }    

        private void btnSNProfesor_Click(object sender, RoutedEventArgs e)
        {
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 2;
            setActive();
        }

        private void btnSNInstrument_Click(object sender, RoutedEventArgs e)
        {
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 3;
            setActive();
        }

        private void btnSNIspitanik_Click(object sender, RoutedEventArgs e)
        {
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 4;
            setActive();
        }

        private void btnSNIspit_Click(object sender, RoutedEventArgs e)
        {
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 5;
            setActive();
        }

        private void btnSNPitanja_Click(object sender, RoutedEventArgs e)
        {
            this.previouslyActive = this.currentlyActive;
            this.currentlyActive = 6;
            setActive();
        }
    }
}
