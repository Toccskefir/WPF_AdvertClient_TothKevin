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

namespace WPF_AdvertClient
{
    /// <summary>
    /// Interaction logic for AdvertCreatorForm.xaml
    /// </summary>
    public partial class AdvertCreatorForm : Window
    {
        AdvertService service = new AdvertService();
        Advert advert;
        public AdvertCreatorForm()
        {
            InitializeComponent();
        }

        public AdvertCreatorForm(Advert advert)
        {
            InitializeComponent();
            this.advert = advert;
            LoadAdvert();
            buttonCreateAdvert.Visibility = Visibility.Hidden;
            buttonUpdateAdvert.Visibility = Visibility.Visible;
        }

        public void LoadAdvert()
        {
            textBoxTitle.Text = advert.Title;
            textBoxImage.Text = advert.Image;
            textBoxDescription.Text = advert.Description;
            textBoxPrice.Text = advert.Price.ToString();
            textBoxSeller.Text = advert.Seller;
            checkBoxTerms.IsChecked = advert.TermsAndConditions;
        }

        private Advert CreateAdvert()
        {
            string title = textBoxTitle.Text.Trim();
            string image = textBoxImage.Text.Trim();
            string description = textBoxDescription.Text.Trim();
            string priceText = textBoxPrice.Text.Trim();
            string seller = textBoxSeller.Text.Trim();
            bool termsAndConditions = (bool)checkBoxTerms.IsChecked;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(image) || string.IsNullOrEmpty(description)
                || string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(seller))
            {
                throw new Exception("Kitöltetlen adatmezők!");
            }

            if (!int.TryParse(priceText, out int price))
            {
                throw new Exception("Az ár csak szám lehet!");
            }

            if (price < 1)
            {
                throw new Exception("Hibás ár!");
            }

            Advert advert = new Advert();
            advert.Title = title;
            advert.Image = image;
            advert.Description = description;
            advert.Price = price;
            advert.Seller = seller;
            advert.TermsAndConditions = termsAndConditions;
            return advert;
        }

        private void OnClick(int argument)
        {
            try
            {
                Advert advert = CreateAdvert();
                Advert newAdvert;
                if (argument == 0)
                {
                    newAdvert = service.AddAdvert(advert);
                }
                else
                {
                    newAdvert = service.UpdateAdvert(this.advert.Id, advert);
                }
                
                if (newAdvert.Id != 0)
                {
                    if (argument == 0)
                    {
                        MessageBox.Show("Hirdetése sikeresen megjelent!");
                    }
                    else
                    {
                        MessageBox.Show("A hirdetést sikeresen módosította!");
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCreateAdvert_Click(object sender, RoutedEventArgs e)
        {
            OnClick(0);
        }

        private void buttonUpdateAdvert_Click(object sender, RoutedEventArgs e)
        {
            OnClick(1);
        }
    }
}
