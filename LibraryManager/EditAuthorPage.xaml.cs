using Microsoft.Win32;
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
using Zadatak.Utils;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for EditWorkplacePage.xaml
    /// </summary>
    public partial class EditAuthorPage : FramedPage<AuthorViewModel>
    {
        private readonly Author author;
        public EditAuthorPage(AuthorViewModel authorViewModel, Author author = null) : base(authorViewModel)
        {
            InitializeComponent();
            this.author = author ?? new Author();
            DataContext = author;
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                author.Name = TbName.Text.Trim();

                if (author.Id == 0)
                {
                    ViewModel.Authors.Add(author);
                }
                else
                {
                    ViewModel.Update(author);
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
