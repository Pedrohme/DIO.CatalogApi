using System;
namespace DIO.CatalogApi.ViewModel {
    public class SongViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
        public string Author { get; set; }
    }
}
