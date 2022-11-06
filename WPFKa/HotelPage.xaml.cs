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

namespace WPFKa
{
    /// <summary>
    /// Логика взаимодействия для HotelPage.xaml
    /// </summary>
    public partial class HotelPage : Page
    {
        public HotelPage()
        {
            InitializeComponent();
            //DGridHotel.ItemsSource = ToursBaseEntities.GetContext().Hotels.ToList();

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Hotel ));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelForRemoving = DGridHotel.SelectedItems.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ToursBaseEntities.GetContext().Hotels.RemoveRange(hotelForRemoving);
                    ToursBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridHotel.ItemsSource = ToursBaseEntities.GetContext().Hotels.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToursBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotel.ItemsSource = ToursBaseEntities.GetContext().Hotels.ToList();
            }
        }

        private void BtnTour_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ToursPage());
        }
    }
}
