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
    public class AuthorViewModel
    {
        private Author selectedAuthor;
        public ObservableCollection<Author> Authors { get; }

        public Author SelectedAuthor    
        {
            get => selectedAuthor;
            set
            {
                selectedAuthor = value ?? Authors.FirstOrDefault();
            }
        }
        public AuthorViewModel()
        {
            Authors = new ObservableCollection<Author>(RepositoryFactory.GetRepository<Author>().GetAll());
            Authors.CollectionChanged += Authors_CollectionChanged;
        }

        private void Authors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository<Author>().Add(Authors[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository<Author>().Delete(e.OldItems.OfType<Author>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository<Author>().Update(e.NewItems.OfType<Author>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Author author) => Authors[Authors.IndexOf(author)] = author;
    }
}
