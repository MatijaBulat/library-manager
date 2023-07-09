using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak.Dal;
using Zadatak.Models;

namespace Zadatak.ViewModels
{
    public class BookViewModel
    {
        public ObservableCollection<Book> Books { get; }
        public BookViewModel()
        {
            Books = new ObservableCollection<Book>(RepositoryFactory.GetRepository<Book>().GetAll());
            Books.CollectionChanged += Book_CollectionChanged;
        }

        private void Book_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository<Book>().Add(Books[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository<Book>().Delete(e.OldItems.OfType<Book>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository<Book>().Update(e.NewItems.OfType<Book>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Book book) => Books[Books.IndexOf(book)] = book;
    }
}
