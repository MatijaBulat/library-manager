using System.Windows;
using System.Windows.Controls;
using Zadatak.Dal;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Frame.Navigate(new ListBookPage(new ViewModels.BookViewModel(), new ViewModels.AuthorViewModel(), new ViewModels.PublisherViewModel()) { Frame = Frame });
        }
    }
}
