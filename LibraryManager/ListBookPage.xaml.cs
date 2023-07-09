using System.Windows;
using System.Windows.Controls;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for ListPersonsPage.xaml
    /// </summary>
    public partial class ListBookPage : FramedPage<BookViewModel>
    {
        public AuthorViewModel AuthorViewModel { get; }
        public PublisherViewModel PublisherViewModel { get; }
        public ListBookPage(BookViewModel bookViewModel) : base(bookViewModel)
        {
            InitializeComponent();
            LvBooks.ItemsSource = bookViewModel.Books;
            DataContext = bookViewModel;
        }

        public ListBookPage(BookViewModel bookViewModel, AuthorViewModel authorViewModel, PublisherViewModel publisherViewModel) : base(bookViewModel)
        {
            InitializeComponent();
            LvBooks.ItemsSource = bookViewModel.Books;
            DataContext = bookViewModel;
            AuthorViewModel = authorViewModel;
            PublisherViewModel = publisherViewModel;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditBookPage(ViewModel, AuthorViewModel, PublisherViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvBooks.SelectedItem != null)
            {
                Frame.Navigate(new EditBookPage(ViewModel, AuthorViewModel, PublisherViewModel, LvBooks.SelectedItem as Book) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvBooks.SelectedItem != null)
            {
                ViewModel.Books.Remove(LvBooks.SelectedItem as Book);
            }
        }

        private void BtnAuthors_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new ListAuthorPage(AuthorViewModel, ViewModel) { Frame = Frame });

        private void BtnPublishers_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new ListPublisherPage(PublisherViewModel, ViewModel) { Frame = Frame });
    }
}
