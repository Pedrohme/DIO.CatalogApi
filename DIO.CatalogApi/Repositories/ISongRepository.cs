using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.CatalogApi.Entities;

namespace DIO.CatalogApi.Repositories {
    public interface ISongRepository : IDisposable {
        Task<List<Song>> Get(int page, int numItems);

        Task<Song> Get(Guid id);

        Task<List<Song>> Get(string name, string author);

        Task Insert(Song song);

        Task Update(Song song);

        Task Delete(Guid id);
    }
}
