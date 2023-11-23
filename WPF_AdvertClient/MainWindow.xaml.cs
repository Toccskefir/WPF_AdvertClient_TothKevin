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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_AdvertClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //source database: https://retoolapi.dev/D2hiYM/ad_creator
        AdvertService service = new AdvertService();

        public MainWindow()
        {
            InitializeComponent();
            dataGridAdvert.ItemsSource = service.GetAll();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AdvertCreatorForm advertCreatorForm = new AdvertCreatorForm();
            advertCreatorForm.Closed += (_, _) =>
            {
                dataGridAdvert.ItemsSource = service.GetAll();
            };
            advertCreatorForm.ShowDialog();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Advert selected = dataGridAdvert.SelectedItem as Advert;
            if (selected != null)
            {
                MessageBoxResult choose = MessageBox.Show($"Biztos törölni szeretné ezt a hirdetést: {selected.Title}?", "Törlés", MessageBoxButton.YesNo);
                if (choose == MessageBoxResult.Yes)
                {
                    if (service.DeleteAdvert(selected))
                    {
                        MessageBox.Show("Sikeresen törölte!");
                    }
                    else
                    {
                        MessageBox.Show("A törlés sikertelen!");
                    }
                    dataGridAdvert.ItemsSource = service.GetAll();
                }
            }
            else
            {
                MessageBox.Show("Válasszon ki egy hirdetést!");
                return;
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Advert selected = dataGridAdvert.SelectedItem as Advert;
            if (selected != null)
            {
                AdvertCreatorForm advertCreatorForm = new AdvertCreatorForm(selected);
                advertCreatorForm.Closed += (_, _) =>
                {
                    dataGridAdvert.ItemsSource = service.GetAll();
                };
                advertCreatorForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Válasszon ki egy hirdetést!");
                return;
            }
        }
    }
}
