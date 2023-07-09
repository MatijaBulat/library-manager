using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Zadatak.Dal;
using Zadatak.Models;

namespace Zadatak.ViewModels
{
    public class PublisherViewModel
    {
        private Publisher selectedPublisher;
        public ObservableCollection<Publisher> Publishers { get; }

        public Publisher SelectedPublisher
        {
            get => selectedPublisher;
            set
            {
                selectedPublisher = value ?? Publishers.FirstOrDefault();
            }
        }
        public PublisherViewModel()
        {
            Publishers = new ObservableCollection<Publisher>(RepositoryFactory.GetRepository<Publisher>().GetAll());
            Publishers.CollectionChanged += Publishers_CollectionChanged;
        }

        private void Publishers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository<Publisher>().Add(Publishers[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository<Publisher>().Delete(e.OldItems.OfType<Publisher>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository<Publisher>().Update(e.NewItems.OfType<Publisher>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Publisher publisher) => Publishers[Publishers.IndexOf(publisher)] = publisher;
    }
}
