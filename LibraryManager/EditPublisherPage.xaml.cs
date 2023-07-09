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
    /// Interaction logic for EditPublisherPage.xaml
    /// </summary>
    public partial class EditPublisherPage : FramedPage<PublisherViewModel>
    {
        private readonly Publisher publisher;
        public EditPublisherPage(PublisherViewModel publisherViewModel, Publisher publisher = null) : base(publisherViewModel)
        {
            InitializeComponent();
            this.publisher = publisher ?? new Publisher();
            DataContext = publisher;
        }
        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                publisher.Name = TbName.Text.Trim();

                if (publisher.Id == 0)
                {
                    ViewModel.Publishers.Add(publisher);
                }
                else
                {
                    ViewModel.Update(publisher);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool valid = true;

            GridContainer.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim()))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });

            return valid;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
    }
}
