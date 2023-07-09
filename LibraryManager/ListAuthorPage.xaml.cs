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
    public partial class ListAuthorPage : FramedPage<AuthorViewModel>
    {
        public BookViewModel BookViewModel { get; }
        public ListAuthorPage(AuthorViewModel authorViewModel, BookViewModel bookViewModel) : base(authorViewModel)
        {
            InitializeComponent();
            LvAuthors.ItemsSource = authorViewModel.Authors;
            BookViewModel = bookViewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new EditAuthorPage(ViewModel) { Frame = Frame });
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvAuthors.SelectedItem != null)
            {
                Frame.Navigate(new EditAuthorPage(ViewModel, LvAuthors.SelectedItem as Author) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvAuthors.SelectedItem != null)
            {
                BookViewModel.Books
                    .Where(b => b.Author.Id == (LvAuthors.SelectedItem as Author).Id).ToList()
                    .ForEach(a => BookViewModel.Books.Remove(a));
                ViewModel.Authors.Remove(LvAuthors.SelectedItem as Author);
            }
        }
    }
}
