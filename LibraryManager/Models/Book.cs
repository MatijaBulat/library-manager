using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Zadatak.Utils;

namespace Zadatak.Models
{
    public class Book : BaseEntity 
    {
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public int YearPublished { get; set; }
        public string ISBN { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage Image
        {
            get => ImageUtils.ByteArrayToBitmapImage(Picture);
        }
    }
}
