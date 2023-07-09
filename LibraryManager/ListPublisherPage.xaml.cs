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
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for ListWorkplacePage.xaml
    /// </summary>
    public partial class ListPublisherPage : FramedPage<PublisherViewModel>
    {
        public BookViewModel BookViewModel { get; }
        public ListPublisherPage(PublisherViewModel publisherViewModel, BookViewModel bookViewModel) : base(publisherViewModel)
        {
            InitializeComponent();
            LvPublishers.ItemsSource = publisherViewModel.Publishers;
            BookViewModel = bookViewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new EditPublisherPage(ViewModel) { Frame = Frame });
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvPublishers.SelectedItem != null)
            {
                Frame.Navigate(new EditPublisherPage(ViewModel, LvPublishers.SelectedItem as Publisher) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvPublishers.SelectedItem != null)
            {
                BookViewModel.Books
                    .Where(b => b.Publisher.Id == (LvPublishers.SelectedItem as Publisher).Id).ToList()
                    .ForEach(p => BookViewModel.Books.Remove(p));
                ViewModel.Publishers.Remove(LvPublishers.SelectedItem as Publisher);
            }
        }
    }
}
