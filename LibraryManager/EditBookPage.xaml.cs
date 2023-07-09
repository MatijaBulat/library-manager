using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zadatak.Models;
using Zadatak.Utils;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary 
    public partial class EditBookPage : FramedPage<BookViewModel>
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly AuthorViewModel authorViewModel;
        private readonly PublisherViewModel publisherViewModel;
        private readonly Book book;
        public EditBookPage(BookViewModel bookViewModel, AuthorViewModel authorViewModel, PublisherViewModel publisherViewModel, Book book = null) : base(bookViewModel)
        {
            InitializeComponent();
            this.authorViewModel = authorViewModel;
            this.publisherViewModel = publisherViewModel;
            this.book = book ?? new Book();
            this.book.Author = this.book.Author ?? new Author();
            this.book.Publisher = this.book.Publisher ?? new Publisher();

            this.authorViewModel.SelectedAuthor = this.authorViewModel.Authors.FirstOrDefault(a => a.Id == this.book.Author.Id);
            this.publisherViewModel.SelectedPublisher = this.publisherViewModel.Publishers.FirstOrDefault(p => p.Id == this.book.Publisher.Id);

            DataContext = new
            {
                book = this.book,
                authors = authorViewModel,
                publishers = publisherViewModel
            };
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new ListBookPage(ViewModel, authorViewModel, publisherViewModel) { Frame = Frame });

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                book.Name = TbTitle.Text.Trim();
                book.YearPublished = int.Parse(TbYearPublished.Text.Trim());
                book.ISBN = TbISBN.Text.Trim();
                book.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                book.Author = CbAuthors.SelectedItem as Author;
                book.Publisher = CbPublishers.SelectedItem as Publisher;
                if (book.Id == 0)
                {
                    ViewModel.Books.Add(book);
                }
                else
                {
                    ViewModel.Update(book);
                }
                Frame.Navigate(new ListBookPage(ViewModel, authorViewModel, publisherViewModel) { Frame = Frame });
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim()) 
                    || ("Int".Equals(e.Tag) && !int.TryParse(e.Text, out int age)))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                } else
                {
                    e.Background = Brushes.White;
                }
            });
            if (Picture.Source == null)
            {
                PictureBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            } 
            else
            {
                PictureBorder.BorderBrush = Brushes.WhiteSmoke;
            }
            return valid;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            book.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
            if (authorViewModel.Authors.Count == 0)
            {
                MessageBox.Show("No authors yet! Add a author to add a book.");
                Frame.Navigate(new ListAuthorPage(authorViewModel, ViewModel) { Frame = Frame });
            }
            if (publisherViewModel.Publishers.Count == 0)
            {
                MessageBox.Show("No publisherss yet! Add a publisher to add a book.");
                Frame.Navigate(new ListPublisherPage(publisherViewModel, ViewModel) { Frame = Frame });
            }
        }
    }
}
