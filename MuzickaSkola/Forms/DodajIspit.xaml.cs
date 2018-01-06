using System;
using System.Collections.Generic;
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
        public DodajIspit()
        {
            InitializeComponent();
            this.tbUkupnoBodova.Text = "100";
            this.chbPolozen.IsChecked = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

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
            finally
            {

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
                if (int.Parse(this.tbOsvojenihBodova.Text) > 50)
                {
                    this.chbPolozen.IsChecked = true;
                } else if (int.Parse(this.tbOsvojenihBodova.Text) <= 50){
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
