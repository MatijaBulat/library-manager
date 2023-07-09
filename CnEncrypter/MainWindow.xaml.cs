using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Zadatak.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace CnEncrypter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e) => TbEncrypted.Text = EncryptionUtils.Encrypt(TbConnectionString.Text);

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e) => TbDecrypted.Text = EncryptionUtils.Decrypt(TbEncrypted.Text);
    }
}
