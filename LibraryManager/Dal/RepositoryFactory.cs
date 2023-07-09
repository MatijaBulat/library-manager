using System;
using System.Collections.Generic;
using Zadatak.Models;

namespace Zadatak.Dal
{
    static class RepositoryFactory
    {
        private static readonly Lazy<Dictionary<string, Object>> repositories = new Lazy<Dictionary<string, Object>>(() =>
        {
            Dictionary<string, Object> repositoryDictionary = new Dictionary<string, Object>();

            repositoryDictionary.Add(nameof(Book), Activator.CreateInstance(typeof(BookRepository)) as IRepository<Book>);
            repositoryDictionary.Add(nameof(Author), Activator.CreateInstance(typeof(AuthorRepository)) as IRepository<Author>);
            repositoryDictionary.Add(nameof(Publisher), Activator.CreateInstance(typeof(PublisherRepository)) as IRepository<Publisher>);

            return repositoryDictionary;
        });

        public static IRepository<T> GetRepository<T>() => repositories.Value[typeof(T).Name] as IRepository<T>;
    }
}
