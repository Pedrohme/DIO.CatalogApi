using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIO.CatalogApi.InputModel;
using DIO.CatalogApi.ViewModel;

namespace DIO.CatalogApi.Services {
    public interface ISongService : IDisposable {

        Task<List<SongViewModel>> Get(int page, int numItems);

        Task<SongViewModel> Get(Guid id);

        Task<SongViewModel> Insert(SongInputModel song);

        Task Update(Guid id, SongInputModel song);

        Task Update(Guid id, string album);

        Task Delete(Guid id);
    }
}
